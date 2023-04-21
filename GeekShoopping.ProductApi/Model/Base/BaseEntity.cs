using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShoopping.ProductApi.Model.Base
{
    public class BaseEntity
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }
    }
}
