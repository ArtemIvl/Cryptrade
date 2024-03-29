﻿using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PortfolioManagement.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PortfolioManagement.Services
{
    public class RabbitMQConsumer : IDisposable
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;
        private readonly object _lock = new object();
        private bool _isConsuming = false;

        private TotalValueModel storedTotalValue;
        private TaskCompletionSource<bool> _consumptionCompleted = new TaskCompletionSource<bool>();

        public RabbitMQConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Declare Exchange and Queue
            _channel.ExchangeDeclare(_configuration["RabbitMQ:ExchangeName"], ExchangeType.Fanout);
        }

        public void StartConsuming(int portfolioId)
        {
            if (_isConsuming)
            {
                return; // Already consuming, prevent starting again
            }

            var queueName = $"{_configuration["RabbitMQ:QueueName"]}_{portfolioId}";

            // Declare Queue (if not already declared)
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            _channel.QueueBind(queue: queueName, exchange: _configuration["RabbitMQ:ExchangeName"], routingKey: "");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var totalValue = JsonConvert.DeserializeObject<TotalValueModel>(message);

                    lock (_lock)
                    {
                        storedTotalValue = totalValue;
                    }

                    // Update total value
                    _consumptionCompleted.TrySetResult(true);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Exception in RabbitMQConsumer: {ex}");
                }
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            _isConsuming = true;
        }

        public async Task<double> GetTotalValueByPortfolioId(int portfolioId)
        {
            // Wait for the completion of the current consumption cycle
            await _consumptionCompleted.Task;

            lock (_lock)
            {
                if (storedTotalValue.portfolioId == portfolioId)
                {
                    return storedTotalValue.totalValue;
                }
                else return 0;
            }
        }

        public async Task<double> GetProfitLossByPortfolioId(int portfolioId)
        {
            // Wait for the completion of the current consumption cycle
            await _consumptionCompleted.Task;

            lock (_lock)
            {
                if (storedTotalValue.portfolioId == portfolioId)
                {
                    return storedTotalValue.profitLoss;
                }
                else return 0;
            }
        }

        public async Task<Performer> GetBestPerformerByPortfolioId(int portfolioId)
        {
            // Wait for the completion of the current consumption cycle
            await _consumptionCompleted.Task;

            lock (_lock)
            {
                if (storedTotalValue.portfolioId == portfolioId)
                {
                    return storedTotalValue.bestPerformer;
                }
                else return null;
            }
        }

        public async Task<Performer> GetWorstPerformerByPortfolioId(int portfolioId)
        {
            // Wait for the completion of the current consumption cycle
            await _consumptionCompleted.Task;

            lock (_lock)
            {
                if (storedTotalValue.portfolioId == portfolioId)
                {
                    return storedTotalValue.worstPerformer;
                }
                else return null;
            }
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}