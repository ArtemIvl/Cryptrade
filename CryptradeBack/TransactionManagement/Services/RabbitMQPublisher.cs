using RabbitMQ.Client;
using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using TransactionManagement.Models;

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

            // Declare Exchange
            _channel.ExchangeDeclare(_configuration["RabbitMQPublish:ExchangeName"], ExchangeType.Fanout);
        }

        public void PublishMessage(double totalValue, double profitLoss, int portfolioId, Performer bestPerformer, Performer worstPerformer)
        {
            // Dynamically generate a unique queue name based on the portfolio ID
            var queueName = $"{_configuration["RabbitMQPublish:QueueName"]}_{portfolioId}";

            // Declare the Queue (if not already declared)
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var message = new
            {
                totalValue = totalValue,
                profitLoss = profitLoss,
                portfolioId = portfolioId,
                bestPerformer = bestPerformer,
                worstPerformer = worstPerformer
            };

            // Convert the message to a JSON string
            var messageJson = Newtonsoft.Json.JsonConvert.SerializeObject(message);

            // Convert the message to bytes
            var body = Encoding.UTF8.GetBytes(messageJson);

            // Publish to the specific queue
            _channel.BasicPublish(exchange: _configuration["RabbitMQPublish:ExchangeName"], routingKey: queueName, basicProperties: null, body: body);
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
