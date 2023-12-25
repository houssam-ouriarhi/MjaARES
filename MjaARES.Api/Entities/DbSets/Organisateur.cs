namespace MjaARES.Api.Entities.DbSets
{
    public class Organisateur:BaseEntity
    {
        public ICollection<EvenementOrganisateur> EvenementOrganisateurs { get; set; }

    }
}
