using MjaARES.Api.Entities.DbSets;
using MjaARES.Api.Entities.Dtos.Responses;

namespace MjaARES.Api.DataService.Repositories.Interfaces
{
    public interface IEvenementRepository : IGeniricRepository<Evenement>
    {
        // retourner la liste des dommaines de l'évenement
        Task<IEnumerable<Domaine>> GetDomainesDEvenementAsync(Guid evenementId);
        
        // retourner la liste des organisateur de l'évenement
        Task<IEnumerable<Organisateur>> GetOrganisateursDEvenementAsync(Guid evenementId);

        // retourner la categorie de l'évenement
        Task<Categorie> GetCategorieDEvenementAsync(Guid evenementId);

        // retourner la liste des invités hommes et femmes avec les différentes tranches d'âge de cet événement
        Task<IEnumerable<GetSexeTrancheAgesDEvenementResponse>> GetSexeTrancheAgesDEvenementAsync(Guid evenementId);

        // retourner la liste des invités les différents tranches d'âge par sexe
        Task<IEnumerable<GetSexeTrancheAgesDEvenementResponse>> GetSexeTrancheAgesDEvenementBySexAsync(Guid evenementId, string sexe);

        // retourner la liste des invités homme et femme par tranches d'âge
        Task<IEnumerable<GetSexeTrancheAgesDEvenementResponse>> GetSexeTrancheAgesDEvenementByTrancheAgeAsync(Guid evenementId, string TrancheAge);

    }
}
