using GeekShopping.MessageBus;

namespace GeekShopping.PaymentApi.RabbitMQSender
{
    public interface IRabbitMQSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
