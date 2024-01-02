namespace MjaARES.Api.Entities.DbSets
{
    public class TrancheAge : BaseEntity
    {
       
        public ICollection<SexeTrancheAgeEvenement> SexeTrancheAgeEvenements { get; set; }


    }
}
