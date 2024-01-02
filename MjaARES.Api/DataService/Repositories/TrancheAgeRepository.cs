using Microsoft.EntityFrameworkCore;
using MjaARES.Api.DataService.Data;
using MjaARES.Api.DataService.Repositories.Interfaces;
using MjaARES.Api.Entities.DbSets;

namespace MjaARES.Api.DataService.Repositories
{
    public class TrancheAgeRepository : GeniricRepository<TrancheAge>, ITrancheAgeRepository
    {
        public TrancheAgeRepository(ILogger logger, AppDbContext context) : base(logger, context)
        {
        }

        public override async Task<IEnumerable<TrancheAge?>> GetAllAsync()
        {
            try
            {
                return await _dbSet.Where(x => x.IsDeleted == false)
                     .AsNoTracking()
                     .AsSplitQuery()
                     .OrderBy(x => x.created_at)
                     .ToListAsync();
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} GetAll() function error", typeof(TrancheAgeRepository));
                throw;
            }
        }

        public override async Task<bool> Update(TrancheAge entity)
        {
            try
            {
                // Get my entity
                TrancheAge? trancheAge = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (trancheAge != null) return false;

                trancheAge.Libelle = entity.Libelle;
                trancheAge.updated_at = DateTime.UtcNow;

                return true;
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} Update() function error", typeof(TrancheAgeRepository));
                throw;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                // Get my entity
                TrancheAge? trancheAge = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (trancheAge != null) return false;

                trancheAge.IsDeleted = true;
                trancheAge.Deleted_at = DateTime.UtcNow;

                return true;
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} Delete() function error", typeof(TrancheAgeRepository));
                throw;
            }
        }

        
    }
}
