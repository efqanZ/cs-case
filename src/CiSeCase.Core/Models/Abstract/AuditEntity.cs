using System;

namespace CiSeCase.Core.Models.Abstract
{
    public class AuditEntity<T> : IdEntity<T>, IAuditEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}