using GeekShopping.MessageBus;

namespace GeekShopping.OrderApi.RabbitMQSender
{
    public interface IRabbitMQSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
