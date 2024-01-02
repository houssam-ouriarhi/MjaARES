using Microsoft.EntityFrameworkCore;
using MjaARES.Api.DataService.Data;
using MjaARES.Api.DataService.Repositories.Interfaces;
using MjaARES.Api.Entities.DbSets;

namespace MjaARES.Api.DataService.Repositories
{
    public class DomaineRepository : GeniricRepository<Domaine>, IDomaineRepository
    {
        public DomaineRepository(ILogger logger, AppDbContext context) : base(logger, context) { }

        public override async Task<IEnumerable<Domaine?>> GetAllAsync()
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
                _logger.LogError(e, "{Repo} GetAllAsync() function error", typeof(DomaineRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Domaine entity)
        {
            try
            {
                // Get my entity
                Domaine? domaine = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (domaine != null) return false;

                domaine.Libelle = entity.Libelle;
                domaine.updated_at = DateTime.UtcNow;

                return true;
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} Update() function error", typeof(DomaineRepository));
                throw;
            }
        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                // Get my entity
                Domaine? domaine = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (domaine != null) return false;

                domaine.IsDeleted = true;
                domaine.Deleted_at = DateTime.UtcNow;

                return true;
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} Delete() function error", typeof(DomaineRepository));
                throw;
            }
        }
        public async Task<IEnumerable<Evenement>> GetEvenementsDeDomaineAsync(Guid domaineId)
        {
            try
            {
                // test it
                ICollection<Evenement> evenements = await _context.Evenements
                     .Include(e => e.DomaineEvenements
                                .Where(de => de.DomaineId == domaineId))
                     .Where(e => !e.IsDeleted)
                     .ToListAsync();


                return evenements;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} GetEvenementsDeCategorieAsync() function error", typeof(DomaineRepository));
                throw;
            }
        }
    }
}
