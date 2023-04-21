﻿using GeekShoopping.ProductApi.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShoopping.ProductApi.Model
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        [Column("Name")]
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        [Column("Price")]
        [Required]
        [Range(1, 10000)]
        public decimal Price { get; set; }
        [Column("Description")]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        public string CategoryName { get; set; }

        [Column("ImageUrl")]
        [StringLength(300)]
        public string ImageURL { get; set; } = string.Empty;
    }
}