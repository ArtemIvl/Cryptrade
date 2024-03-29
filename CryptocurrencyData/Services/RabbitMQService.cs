﻿using RabbitMQ.Client;
using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CryptocurrencyData.Services
{
    public class RabbitMQService
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQService(IConfiguration configuration)
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
            _channel.ExchangeDeclare(_configuration["RabbitMQ:ExchangeName"], type: ExchangeType.Fanout);
            _channel.QueueDeclare(_configuration["RabbitMQ:QueueName"], durable: true, exclusive: false, autoDelete: false, arguments: new Dictionary<string, object>{{ "x-max-length", 1 }});
            _channel.QueueBind(_configuration["RabbitMQ:QueueName"], _configuration["RabbitMQ:ExchangeName"], "");
        }

        public void PublishMessage(string jsonData)
        {
            var body = Encoding.UTF8.GetBytes(jsonData);
            _channel.BasicPublish(exchange: _configuration["RabbitMQ:ExchangeName"], "", basicProperties: null, body: body);
        }

        //public void SubscribeToTopic(string topic)
        //{
        //    _channel.QueueBind(_configuration["RabbitMQ:QueueName"], _configuration["RabbitMQ:ExchangeName"], topic);
        //}

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
