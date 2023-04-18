using GeekShooping.ProductApi.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShooping.ProductApi.Model
{
    [Table("Product")]
    public class Product:BaseEntity
    {
        [Column("Name")]
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        [Column("Price")]
        [Required]
        [Range(1,10000)]
        public decimal Price{ get; set; }
        [Column("Description")]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Column("Category_Id")]
        public long Category_Id { get; set; }
        [ForeignKey(nameof(Category_Id))]
        public Category Category { get; set; }

        [Column("Image_Url")]
        [StringLength(300)]
        public string ImageURL { get; set; } = string.Empty;
    }
}
