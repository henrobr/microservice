using GeekShopping.MessageBus;
using GeekShopping.OrderApi.Messages;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderApi.RabbitMQSender
{
    public class RabbitMQSender : IRabbitMQSender
    {
        private readonly string hostName;
        private readonly string password;
        private readonly string username;
        private IConnection connection;

        public RabbitMQSender()
        {
            hostName = "localhost";
            password = "guest";
            username = "guest";
        }

        public void SendMessage(BaseMessage baseMessage, string queueName)
        {

            if (ConnectionExists())
            {
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
                byte[] body = GetMessageAsByteArray(baseMessage);
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
        }

        private byte[] GetMessageAsByteArray(object message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize<PaymentVO>((PaymentVO)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = hostName,
                    UserName = username,
                    Password = password
                };

                connection = factory.CreateConnection();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool ConnectionExists()
        {
            if (connection != null)
                return true;

            CreateConnection();

            return connection != null;
        }
    }
}
