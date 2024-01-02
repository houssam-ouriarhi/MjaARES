using Microsoft.EntityFrameworkCore;
using MjaARES.Api.DataService.Data;
using MjaARES.Api.DataService.Repositories.Interfaces;
using MjaARES.Api.Entities.DbSets;

namespace MjaARES.Api.DataService.Repositories
{
    public class SexeRepository : GeniricRepository<Sexe>, ISexeRepository
    {
        public SexeRepository(ILogger logger, AppDbContext context) : base(logger, context)
        {
        }
       
        public override async Task<IEnumerable<Sexe?>> GetAllAsync()
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

                _logger.LogError(e, "{Repo} GetAll() function error", typeof(SexeRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Sexe entity)
        {
            try
            {
                // Get my entity
                Sexe? sexe = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (sexe != null) return false;

                sexe.Libelle = entity.Libelle;
                sexe.updated_at = DateTime.UtcNow;

                return true;
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} Update() function error", typeof(SexeRepository));
                throw;
            }
        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                // Get my entity
                Sexe? sexe = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (sexe != null) return false;

                sexe.IsDeleted = true;
                sexe.Deleted_at = DateTime.UtcNow;

                return true;
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} Delete() function error", typeof(SexeRepository));
                throw;
            }
        }

    }
}
