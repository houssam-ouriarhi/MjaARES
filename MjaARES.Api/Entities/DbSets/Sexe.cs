namespace MjaARES.Api.Entities.DbSets
{
    public class Sexe : BaseEntity
    {
        public ICollection<SexeTrancheAgeEvenement> SexeTrancheAgeEvenements { get; set; }
    }
}
