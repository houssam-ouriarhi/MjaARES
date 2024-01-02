using Microsoft.EntityFrameworkCore;
using MjaARES.Api.DataService.Data;
using MjaARES.Api.DataService.Repositories.Interfaces;
using MjaARES.Api.Entities.DbSets;

namespace MjaARES.Api.DataService.Repositories
{
    public class OrganisateurRepository : GeniricRepository<Organisateur>, IOrganisateurRepository
    {
        public OrganisateurRepository(ILogger logger, AppDbContext context) : base(logger, context)
        {
        }
        public override async Task<IEnumerable<Organisateur?>> GetAllAsync()
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

                _logger.LogError(e, "{Repo} GetAll() function error", typeof(OrganisateurRepository));
                throw;
            }
        }
        public override async Task<bool> Update(Organisateur entity)
        {
            try
            {
                // Get my entity
                Organisateur? organisateur = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (organisateur != null) return false;

                organisateur.Libelle = entity.Libelle;
                organisateur.updated_at = DateTime.UtcNow;

                return true;
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} Update() function error", typeof(OrganisateurRepository));
                throw;
            }
        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                // Get my entity
                Organisateur? organisateur = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (organisateur != null) return false;

                organisateur.IsDeleted = true;
                organisateur.Deleted_at = DateTime.UtcNow;

                return true;
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} Delete() function error", typeof(OrganisateurRepository));
                throw;
            }
        }


        public async Task<IEnumerable<Evenement>> GetEvenementsDOrganisateurAsync(Guid OrganisateurId)
        {
            try
            {

                ICollection<Evenement> evenements = await _context.Evenements
                     .Include(e => e.EvenementOrganisateurs
                                .Where(eo => eo.OrganisateurId == OrganisateurId))
                     .Where(e => !e.IsDeleted)
                     .ToListAsync();


                return evenements;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} GetEvenementsDOrganisateurAsync() function error", typeof(OrganisateurRepository));
                throw;
            }
        }


    }
}
