using Microsoft.EntityFrameworkCore;
using MjaARES.Api.DataService.Data;
using MjaARES.Api.DataService.Repositories.Interfaces;
using MjaARES.Api.Entities.DbSets;
using MjaARES.Api.Entities.Dtos.Responses;

namespace MjaARES.Api.DataService.Repositories
{
    public class EvenementsRepository : GeniricRepository<Evenement>, IEvenementRepository
    {
        public EvenementsRepository(ILogger logger, AppDbContext context) : base(logger, context)
        {
        }

        public override async Task<IEnumerable<Evenement?>> GetAllAsync()
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

                _logger.LogError(e, "{Repo} GetAll() function error", typeof(EvenementsRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Evenement entity)
        {

            try
            {
                // Get my entity
                Evenement? evenement = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (evenement != null) return false;

                evenement.Libelle = entity.Libelle;
                evenement.Sujet = entity.Sujet;
                evenement.Remarques = entity.Remarques;

                evenement.CategorieId = entity.CategorieId;

                evenement.DateDebut = entity.DateDebut;
                evenement.DateFin = entity.DateFin;

                evenement.updated_at = DateTime.UtcNow;

                return true;
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} Update() function error", typeof(EvenementsRepository));
                throw;
            }
        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                Evenement? evenement = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (evenement != null) return false;

                evenement.IsDeleted = true;
                evenement.Deleted_at = DateTime.UtcNow;

                return true;
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} Delete() function error", typeof(EvenementsRepository));
                throw;
            }
        }



        public async Task<Categorie> GetCategorieDEvenementAsync(Guid evenementId)
        {
            try
            {

                Categorie? categorie = await _context.Categories
                        .Include(c => c.Evenements)
                            .ThenInclude(e => e.Where(e => e.Id == evenementId))
                        .Where(c => !c.IsDeleted)
                        .FirstOrDefaultAsync();


                return categorie;
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} GetCategorieDEvenementAsync() function error", typeof(EvenementsRepository));
                throw;
            }
        }


        public async Task<IEnumerable<Domaine>> GetDomainesDEvenementAsync(Guid evenementId)
        {
            try
            {

                IEnumerable<Domaine> domaines = await _context.Domaines
                    .Include(d => d.DomaineEvenements)
                        .ThenInclude(de => de.Where(de => de.EvenementId == evenementId))
                    .Where(d => !d.IsDeleted)
                    .ToListAsync();
                return domaines;

            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} GetDomainesDEvenementAsync() function error", typeof(EvenementsRepository));
                throw;
            }
        }

        public async Task<IEnumerable<Organisateur>> GetOrganisateursDEvenementAsync(Guid evenementId)
        {
            try
            {
                IEnumerable<Organisateur> organisateurs = await _context.Organisateurs
                    .Include(e => e.EvenementOrganisateurs)
                        .ThenInclude(eo => eo.Where(eo => eo.EvenementId == evenementId))
                    .Where(o => !o.IsDeleted)
                    .ToListAsync();

                return organisateurs;

            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} GetOrganisateursDEvenementAsync() function error", typeof(EvenementsRepository));
                throw;
            }
        }

        public async Task<IEnumerable<GetSexeTrancheAgesDEvenementResponse>> GetSexeTrancheAgesDEvenementAsync(Guid evenementId)
        {
            try
            {

                IEnumerable<GetSexeTrancheAgesDEvenementResponse> sexeTrancheAgeEvenements = await _context.Sexes
                        .AsNoTracking()
                        .Include(s => s.SexeTrancheAgeEvenements)
                            .ThenInclude(stae => stae.TrancheAge)
                        .Where(s => !s.IsDeleted && s.SexeTrancheAgeEvenements.Any(stae => !stae.Evenement.IsDeleted && stae.Evenement.Id == evenementId))
                        .OrderBy(s => s.SexeTrancheAgeEvenements
                                        .FirstOrDefault().TrancheAge.Libelle)
                        .Select(s => new GetSexeTrancheAgesDEvenementResponse
                        {
                            SexeLibelle = s.Libelle,
                            TrancheAgeLibelle = s.SexeTrancheAgeEvenements.FirstOrDefault().TrancheAge.Libelle,
                            Somme = s.SexeTrancheAgeEvenements.FirstOrDefault().Somme
                        })
                        .ToListAsync();



                return sexeTrancheAgeEvenements;

            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} GetSexeTrancheAgesDEvenementAsync() function error", typeof(EvenementsRepository));
                throw;
            }
        }

        public async Task<IEnumerable<GetSexeTrancheAgesDEvenementResponse>> GetSexeTrancheAgesDEvenementBySexAsync(Guid evenementId, string sexe)
        {
            try
            {

                IEnumerable<GetSexeTrancheAgesDEvenementResponse> sexeTrancheAgeEvenementsBySexe = await _context.Sexes
                        .AsNoTracking()
                        .Include(s => s.SexeTrancheAgeEvenements)
                            .ThenInclude(stae => stae.TrancheAge)
                        .Where(s => !s.IsDeleted && s.SexeTrancheAgeEvenements
                             .Any(stae =>
                                        !stae.Evenement.IsDeleted
                                        && stae.Evenement.Id == evenementId
                                        && stae.Sexe.Libelle == sexe))
                        .OrderBy(s => s.SexeTrancheAgeEvenements
                                        .FirstOrDefault().TrancheAge.Libelle)
                        .Select(s => new GetSexeTrancheAgesDEvenementResponse
                        {
                            SexeLibelle = s.Libelle,
                            TrancheAgeLibelle = s.SexeTrancheAgeEvenements.FirstOrDefault().TrancheAge.Libelle,
                            Somme = s.SexeTrancheAgeEvenements.FirstOrDefault().Somme
                        })
                        .ToListAsync();



                return sexeTrancheAgeEvenementsBySexe;

            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} GetSexeTrancheAgesDEvenementBySexAsync() function error", typeof(EvenementsRepository));
                throw;
            }
        }

        public async Task<IEnumerable<GetSexeTrancheAgesDEvenementResponse>> GetSexeTrancheAgesDEvenementByTrancheAgeAsync(Guid evenementId, string TrancheAge)
        {
            try
            {

                IEnumerable<GetSexeTrancheAgesDEvenementResponse> sexeTrancheAgeEvenementsByTrancheAge = await _context.Sexes
                        .AsNoTracking()
                        .Include(s => s.SexeTrancheAgeEvenements)
                            .ThenInclude(stae => stae.TrancheAge)
                        .Where(s => !s.IsDeleted && s.SexeTrancheAgeEvenements
                             .Any(stae =>
                                        !stae.Evenement.IsDeleted
                                        && stae.Evenement.Id == evenementId
                                        && stae.TrancheAge.Libelle == TrancheAge))
                        .OrderBy(s => s.SexeTrancheAgeEvenements
                                        .FirstOrDefault().TrancheAge.Libelle)
                        .Select(s => new GetSexeTrancheAgesDEvenementResponse
                        {
                            SexeLibelle = s.Libelle,
                            TrancheAgeLibelle = s.SexeTrancheAgeEvenements.FirstOrDefault().TrancheAge.Libelle,
                            Somme = s.SexeTrancheAgeEvenements.FirstOrDefault().Somme
                        })
                        .ToListAsync();



                return sexeTrancheAgeEvenementsByTrancheAge;

            }
            catch (Exception e)
            {

                _logger.LogError(e, "{Repo} GetSexeTrancheAgesDEvenementByTrancheAgeAsync() function error", typeof(EvenementsRepository));
                throw;
            }
        }


    }
}
