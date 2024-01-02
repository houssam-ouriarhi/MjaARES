using MjaARES.Api.Entities.DbSets;

namespace MjaARES.Api.DataService.Repositories.Interfaces
{
    public interface IDomaineRepository :IGeniricRepository<Domaine>
    {
        // retourner tous les événements  liés à ce domaine
        Task<IEnumerable<Evenement>> GetEvenementsDeDomaineAsync(Guid domaineId);

    }
}
