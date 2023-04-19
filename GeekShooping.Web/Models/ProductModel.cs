namespace GeekShooping.Web.Models
{
    public class ProductModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public long Category_Id { get; set; }
        public string Image_Url { get; set; } = string.Empty;
    }
}
