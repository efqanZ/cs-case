using System;
using System.Collections.Generic;

namespace CiSeCase.Core.Dtos
{
    public class BasketDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddDate { get; set; }
    }
}