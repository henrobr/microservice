using GeekShopping.PaymentApi.Messages;
using GeekShopping.PaymentApi.RabbitMQSender;
using GeekShopping.PaymentProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.PaymentApi.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private IConnection connection;
        private IModel channel;
        private IRabbitMQSender rabbitMQSender;
        private readonly IProcessPayment processPayment;

        public RabbitMQPaymentConsumer(IProcessPayment processPayment, IRabbitMQSender rabbitMQSender)
        {
            this.processPayment = processPayment;
            this.rabbitMQSender = rabbitMQSender;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            this.connection = factory.CreateConnection();

            this.channel = this.connection.CreateModel();
            channel.QueueDeclare(queue: "orderpaymentproccessqueue", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                PaymentMessage vo = JsonSerializer.Deserialize<PaymentMessage>(content);
                ProcessorPayment(vo).GetAwaiter().GetResult();
                channel.BasicAck(evt.DeliveryTag, false);
            };
            channel.BasicConsume("orderpaymentproccessqueue", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessorPayment(PaymentMessage vo)
        {
            var result = processPayment.PaymentProcessor();

            UpdatePaymentResultMessage paymentResult = new UpdatePaymentResultMessage()
            {
                Status = result,
                OrderId = vo.OrderId,
                Email = vo.Email
            };

            try
            {
                rabbitMQSender.SendMessage(paymentResult, "orderpaymentresultqueue");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
