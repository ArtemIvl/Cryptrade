using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TransactionManagement.Models;

public class RabbitMQConsumer : IDisposable
{
    private readonly IConfiguration _configuration;
    private IConnection _connection;
    private IModel _channel;
    private List<Cryptocurrency> _cachedCryptocurrencies = new List<Cryptocurrency>();
    private readonly object _lock = new object();
    private bool _isConsuming = false;

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

        _channel.ExchangeDeclare(_configuration["RabbitMQ:ExchangeName"], ExchangeType.Fanout);
        _channel.QueueDeclare(_configuration["RabbitMQ:QueueName"], durable: true, exclusive: false, autoDelete: false, arguments: new Dictionary<string, object> { { "x-max-length", 1 } });
        _channel.QueueBind(_configuration["RabbitMQ:QueueName"], _configuration["RabbitMQ:ExchangeName"], "");
    }

    public void StartConsuming()
    {
        if (_isConsuming)
        {
            return; // Already consuming, prevent starting again
        }

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Deserialize the JSON data back into a list of Cryptocurrency
                var cryptocurrencies = JsonConvert.DeserializeObject<List<Cryptocurrency>>(message);

                // Store the cryptocurrencies for later retrieval
                lock (_lock)
                {
                    _cachedCryptocurrencies = cryptocurrencies;
                }

                // Process the list of Cryptocurrency as needed

                // Signal that the consumption for this cycle is complete
                _consumptionCompleted.TrySetResult(true);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception in RabbitMQConsumer: {ex}");
            }
        };

        _channel.BasicConsume(queue: _configuration["RabbitMQ:QueueName"], autoAck: false, consumer: consumer);
        _isConsuming = true;
    }

    public async Task<Cryptocurrency> GetCachedCryptocurrenciesAsync(string cryptoName)
    {
        // Wait for the completion of the current consumption cycle
        await _consumptionCompleted.Task;

        lock (_lock)
        {
            var list = new List<Cryptocurrency>(_cachedCryptocurrencies);
            Cryptocurrency requestedCrypto = null;
            foreach (Cryptocurrency item in list)
            {
                if (item.name == cryptoName)
                {
                    requestedCrypto = item;
                }
            }
            return requestedCrypto;
        }
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }
}
