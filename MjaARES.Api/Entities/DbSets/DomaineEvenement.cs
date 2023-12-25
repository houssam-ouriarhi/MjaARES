namespace MjaARES.Api.Entities.DbSets
{
    public class DomaineEvenement
    {
        public Guid DomaineId { get; set; }
        public Guid EvenementId { get; set; }

        public Domaine Domaine { get; set; }
        public Evenement Evenement { get; set; }
    }
}
