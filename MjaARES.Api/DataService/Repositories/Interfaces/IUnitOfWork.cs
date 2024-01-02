namespace MjaARES.Api.DataService.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ICategorieRepository Categorie { get; }
        IDomaineRepository Domaine { get; }
        IEvenementRepository Evenement{ get; }
        IOrganisateurRepository Organisateur{ get; }
        ISexeRepository Sexe{ get; }
        ITrancheAgeRepository TrancheAge{ get; }
        Task<bool> CompleteAsync();
    }
}
