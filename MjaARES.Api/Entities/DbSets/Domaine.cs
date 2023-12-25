namespace MjaARES.Api.Entities.DbSets
{
    public class Domaine : BaseEntity
    {
        public ICollection<DomaineEvenement> DomaineEvenements { get; set; }

    }
}
