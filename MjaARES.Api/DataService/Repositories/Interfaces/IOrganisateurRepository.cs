using MjaARES.Api.Entities.DbSets;

namespace MjaARES.Api.DataService.Repositories.Interfaces
{
    public interface IOrganisateurRepository : IGeniricRepository<Organisateur>
    {
        // Retourner tous les événements liés à l'organisateur 
        Task<IEnumerable<Evenement>> GetEvenementsDOrganisateurAsync(Guid OrganisateurId);

    }
}
