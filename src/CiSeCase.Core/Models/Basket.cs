using System.Collections.Generic;
using CiSeCase.Core.Models.Abstract;

namespace CiSeCase.Core.Models
{
    public class Basket : AuditEntity<long>
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }


    }
}