﻿namespace Geekshopping.CartApi.Data.ValueObjects
{
    public class CuponVO
    {
        public long Id { get; set; }
        public string CuponCode { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
