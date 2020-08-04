using System;
using System.Collections.Generic;
using CiSeCase.Core.Models.Abstract;

namespace CiSeCase.Core.Models
{
    public class Product : AuditEntity<int>
    {
        public int StockQuantity { get; set; }

        public ICollection<Basket> BasketItems { get; set; }
    }
}