using GeekShopping.MessageBus;

namespace Geekshopping.CartApi.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
