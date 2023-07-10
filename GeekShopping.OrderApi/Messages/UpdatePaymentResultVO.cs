namespace GeekShopping.OrderApi.Messages
{
    public class UpdatePaymentResultVO
    {
        public long OrderId { get; set; }
        public bool Status { get; set; }
        public decimal Email { get; set; }
    }
}
