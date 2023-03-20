using GeekShopping.MessageBus;

namespace GeekShopping.CartApi.RabbitMQSender
{
    public interface IRabbitMQSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
