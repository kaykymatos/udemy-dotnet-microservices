using GeekShopping.OrderApi.Messages;
using GeekShopping.OrderApi.Model;
using GeekShopping.OrderApi.RabbitMQSender;
using GeekShopping.OrderApi.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderApi.MessageConsumer
{
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private readonly OrderRepository _repository;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IRabbitMQMessageSender _rabbitMQMessageSender;

        public RabbitMQCheckoutConsumer(OrderRepository repoistory, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _repository = repoistory;
            _rabbitMQMessageSender = rabbitMQMessageSender;
            ConnectionFactory factory = new()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "checkoutqueue", false, false, false, arguments: null);

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            EventingBasicConsumer consumer = new(_channel);
            consumer.Received += (channel, evt) =>
            {
                string content = Encoding.UTF8.GetString(evt.Body.ToArray());
                CheckoutHeaderVO vo = JsonSerializer.Deserialize<CheckoutHeaderVO>(content);
                ProcessOrder(vo).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };

            _channel.BasicConsume("checkoutqueue", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessOrder(CheckoutHeaderVO vo)
        {
            OrderHeader order = new()
            {
                UserId = vo.UserId,
                FirstName = vo.FirstName,
                LastName = vo.LastName,
                OrderDetails = new List<OrderDetail>(),
                CardNumber = vo.CardNumber,
                CuponCode = vo.CuponCode,
                CVV = vo.CVV,
                DiscountAmount = vo.DiscountAmount,
                Email = vo.Email,
                ExpiryMothYear = vo.ExpiryMothYear,
                OrderTime = DateTime.Now,
                PurchaseAmount = vo.PurchaseAmount,
                PaymentStatus = false,
                Phone = vo.Phone,
                DateTime = vo.DateTime
            };

            foreach (CartDetailVO details in vo.CartDetails)
            {
                OrderDetail detail = new()
                {
                    ProductId = details.ProductId,
                    ProductName = details.Product.Name,
                    Price = details.Product.Price,
                    Count = details.Count,
                };
                order.CartTotalItens += details.Count;
                order.OrderDetails.Add(detail);
            }

            await _repository.AddOrder(order);

            PaymentVO payment = new PaymentVO()
            {
                Name = order.FirstName + " " + order.LastName,
                CardNumber = order.CardNumber,
                CVV = order.CVV,
                ExpiryMothYear = order.ExpiryMothYear,
                OrderId = order.Id,
                PurchaseAmount = order.PurchaseAmount,
                Email = order.Email
            };

            try
            {
                _rabbitMQMessageSender.SendMessage(payment, "orderpaymentprocessqueue");
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
