using System;

namespace CiSeCase.Core.Models.Abstract
{
    public interface IAuditEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}