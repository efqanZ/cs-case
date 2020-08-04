using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CiSeCase.Core.Interfaces.Repository.Base;
using CiSeCase.Core.Models.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CiSeCase.Infrastructure.Data.Repository.Base
{
    public class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
            entity.Deleted = false;
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            entity.Deleted = true;
            await EditAsync(entity);
        }

        public async Task EditAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate = null)
        {
            var query = _context.Set<T>().Where(x => !x.Deleted);

            if (predicate != null)
                query = query.Where(predicate);

            var datas = await query.ToListAsync<T>();

            return datas;
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
        {
            var query = _context.Set<T>().Where(x => !x.Deleted);

            if (predicate != null)
                return await query.FirstOrDefaultAsync<T>(predicate);

            return await query.FirstOrDefaultAsync<T>();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
        {
            var query = _context.Set<T>().Where(p => !p.Deleted);
            if (predicate != null)
                return await query.AnyAsync(predicate);

            return await query.AnyAsync();
        }
    }
}