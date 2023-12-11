﻿using RabbitMQ.Client;
using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace TransactionManagement.Services
{
    public class RabbitMQPublisher
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQPublish:HostName"],
                UserName = _configuration["RabbitMQPublish:UserName"],
                Password = _configuration["RabbitMQPublish:Password"]
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Declare Exchange and Queue
            _channel.ExchangeDeclare(_configuration["RabbitMQPublish:ExchangeName"], ExchangeType.Fanout);
            _channel.QueueDeclare(_configuration["RabbitMQPublish:QueueName"], durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(_configuration["RabbitMQPublish:QueueName"], _configuration["RabbitMQPublish:ExchangeName"], "");
        }

        public void PublishMessage(double totalValue, double profitLoss, int portfolioId)
        {
            var message = new
            {
                totalValue = totalValue,
                profitLoss = profitLoss,
                portfolioId = portfolioId
            };

            // Convert the message to a JSON string
            var messageJson = Newtonsoft.Json.JsonConvert.SerializeObject(message);

            // Convert the message to bytes
            var body = Encoding.UTF8.GetBytes(messageJson);
            _channel.BasicPublish(exchange: _configuration["RabbitMQPublish:ExchangeName"], routingKey: "", basicProperties: null, body: body);
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}

