using Geekshopping.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geekshopping.CartAPI.Model
{
    [Table("cart_detail")]
    public class CartDetail : BaseEntity
    {
        public long CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public long ProductId { get; set; }
        [Column("count")]
        public int Count { get; set; }
    }
}
