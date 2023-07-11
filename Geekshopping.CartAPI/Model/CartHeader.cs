using Geekshopping.CartApi.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geekshopping.CartApi.Model
{
    [Table("cart_header")]
    public class CartHeader : BaseEntity
    {
        [Column("user_id")]
        public string UserId { get; set; }
        [Column("cupon_code")]
        public string CuponCode { get; set; }
    }
}
