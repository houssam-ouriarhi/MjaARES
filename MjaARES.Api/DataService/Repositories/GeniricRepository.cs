using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MjaARES.Api.DataService.Data;
using MjaARES.Api.DataService.Repositories.Interfaces;

namespace MjaARES.Api.DataService.Repositories
{
    public class GeniricRepository<T> : IGeniricRepository<T> where T : class
    {
        public readonly ILogger _logger;
        protected AppDbContext _context;
        internal DbSet<T> _dbSet;

        public GeniricRepository(ILogger logger, AppDbContext context)
        {
            this._logger = logger;
            this._context = context;
            this._dbSet = context.Set<T>();
        }
        public virtual async Task<IEnumerable<T?>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);

            return true;
        }
        public virtual async Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
        public virtual async Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
