using Microsoft.EntityFrameworkCore;
using MjaARES.Api.DataService.Data;
using MjaARES.Api.DataService.Repositories.Interfaces;
using MjaARES.Api.Entities.DbSets;

namespace MjaARES.Api.DataService.Repositories
{
    public class CategorieRepository : GeniricRepository<Categorie>, ICategorieRepository
    {
        public CategorieRepository(ILogger logger, AppDbContext context) : base(logger, context) { }
        public override async Task<IEnumerable<Categorie?>> GetAllAsync()
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
                _logger.LogError(e, "{Repo} GetAllAsync() function error", typeof(CategorieRepository));
                throw;
            }
        }
        public override async Task<bool> Update(Categorie entity)
        {
            try
            {
                // Get my entity
                Categorie? categorie = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (categorie != null) return false;

                categorie.Libelle = entity.Libelle;
                categorie.updated_at = DateTime.UtcNow;

                return true;
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} Update() function error", typeof(CategorieRepository));
                throw;
            }
        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                // Get my entity
                Categorie? categorie = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (categorie != null) return false;

                categorie.IsDeleted = true;
                categorie.Deleted_at = DateTime.UtcNow;

                return true;
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} Delete() function error", typeof(CategorieRepository));
                throw;
            }
        }

        public async Task<IEnumerable<Evenement>> GetEvenementsDeCategorieAsync(Guid categorieId)
        {
            try
            {
                ICollection<Evenement> evenements = await _context.Evenements.Where(x => x.CategorieId == categorieId).ToListAsync();

                return evenements;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} GetEvenementsDeCategorieAsync() function error", typeof(CategorieRepository));
                throw;
            }
        }


    }
}
