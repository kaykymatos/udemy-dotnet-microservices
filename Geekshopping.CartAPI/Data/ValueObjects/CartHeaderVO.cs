using Geekshopping.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geekshopping.CartAPI.Data.ValueObjects
{
    public class CartHeaderVO 
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string CuponCode { get; set; }
    }
}
