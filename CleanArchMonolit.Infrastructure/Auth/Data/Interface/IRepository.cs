using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMonolit.Shared.Infrastructure.Data.Interface
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        DbSet<TEntity> GetDbSet();
        IQueryable<TEntity> GetDbSetQuery();
        Task<TEntity> FindAsync(int key);
        Task<IQueryable<TEntity>> Where(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        Task AddAsync(TEntity entity);
        Task AddListAsync(IEnumerable<TEntity> entities);
        Task UpdateListAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        Task<int> SaveChangesAsync();
        //Task AddWithLogAsync(TEntity entity);
        //Task UpdateWithLog(TEntity entity, int id);
        //Task RemoveWithLog(TEntity entity, int id);
        //Task SaveChangesWithAuditAsync();
    }
}
