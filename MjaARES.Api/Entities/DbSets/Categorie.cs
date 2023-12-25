namespace MjaARES.Api.Entities.DbSets
{
    public class Categorie : BaseEntity
    {
        public ICollection<Evenement> Evenements { get;  } = new List<Evenement>();
    }
}
