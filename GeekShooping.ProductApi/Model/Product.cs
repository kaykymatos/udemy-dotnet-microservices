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
        [Required]
        public long Category_Id { get; set; }
        [NotMapped]
        [ForeignKey(nameof(Category_Id))]
        public Category Category { get; set; } = new Category();

        [Column("Image_Url")]
        [StringLength(300)]
        public string Image_Url { get; set; } = string.Empty;
    }
}
