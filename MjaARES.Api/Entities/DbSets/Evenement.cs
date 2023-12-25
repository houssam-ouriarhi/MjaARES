namespace MjaARES.Api.Entities.DbSets
{
    public class Evenement : BaseEntity
    {
        public required DateTime DateDebut { get; set; }
        public required DateTime DateFin { get; set; }
        public required string Sujet { get; set; }
        public string? Remarques { get; set; }

        public Guid CategorieId { get; set; }
        public Categorie Categorie { get; set; }

        public ICollection<SexeTrancheAgeEvenement> SexeTrancheAgeEvenements { get; set; }
        public ICollection<EvenementOrganisateur> EvenementOrganisateurs { get; set; }
        public ICollection<DomaineEvenement> DomaineEvenements { get; set; }


    }
}
