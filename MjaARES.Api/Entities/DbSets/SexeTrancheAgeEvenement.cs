namespace MjaARES.Api.Entities.DbSets
{
    public class SexeTrancheAgeEvenement
    {
        public Guid SexeId { get; set; }
        public Guid TrancheAgeId { get; set; }

        public Guid EvenementId { get; set; }

        public int Somme { get; set; }

        public Sexe Sexe { get; set; }
        public TrancheAge TrancheAge { get; set; }

        public Evenement Evenement { get; set; }
    }
}
