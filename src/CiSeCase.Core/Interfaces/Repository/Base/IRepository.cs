using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CiSeCase.Core.Models.Abstract;

namespace CiSeCase.Core.Interfaces.Repository.Base
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task EditAsync(T entity);
        Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);
    }
}