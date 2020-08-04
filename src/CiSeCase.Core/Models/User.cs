using System.Collections.Generic;
using CiSeCase.Core.Models.Abstract;

namespace CiSeCase.Core.Models
{
    public class User : AuditEntity<int>
    {
        public ICollection<Basket> BasketItems { get; set; }
    }
}