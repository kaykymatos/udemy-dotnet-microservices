using GeekShooping.ProductApi.Model.Base;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShooping.ProductApi.Model
{
    [Table("Category")]
    public class Category:BaseEntity
    {
        [Column("Name")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Column("Description")]
        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
        [NotMapped]
        List<Product> Products { get; set; }
    }
}
