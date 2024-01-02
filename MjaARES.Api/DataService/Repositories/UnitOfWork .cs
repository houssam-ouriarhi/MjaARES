using MjaARES.Api.DataService.Data;
using MjaARES.Api.DataService.Repositories.Interfaces;
using MjaARES.Api.Entities.DbSets;

namespace MjaARES.Api.DataService.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");

            Categorie = new CategorieRepository(logger, context);
            Domaine = new DomaineRepository(logger, context);
            Evenement = new EvenementsRepository(logger, context);
            Organisateur = new OrganisateurRepository(logger, context);
            Sexe = new SexeRepository(logger, context);
            TrancheAge = new TrancheAgeRepository(logger, context);
        }

        public ICategorieRepository Categorie { get; }

        public IDomaineRepository Domaine { get; }

        public IEvenementRepository Evenement { get; }

        public IOrganisateurRepository Organisateur { get; }

        public ISexeRepository Sexe { get; }

        public ITrancheAgeRepository TrancheAge { get; }

        public async Task<bool> CompleteAsync()
        {
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public void Dispose() { _context.Dispose(); }
    }
}
