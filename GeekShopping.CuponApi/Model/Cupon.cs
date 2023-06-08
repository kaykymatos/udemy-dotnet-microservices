using GeekShopping.CuponApi.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CuponApi.Model
{
    [Table("cupon")]
    public class Cupon : BaseEntity
    {
        [Column("cupon_code")]
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Column("discount_amount")]
        [Required]
        public decimal DiscountAmount { get; set; }
    }
}
