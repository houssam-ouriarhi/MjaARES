using MjaARES.Api.Entities.DbSets;

namespace MjaARES.Api.DataService.Repositories.Interfaces
{
    public interface ICategorieRepository : IGeniricRepository<Categorie>
    {
        // retourner tous les événements de cette catégorie
        Task<IEnumerable<Evenement>> GetEvenementsDeCategorieAsync(Guid categorieId);
    }
}
