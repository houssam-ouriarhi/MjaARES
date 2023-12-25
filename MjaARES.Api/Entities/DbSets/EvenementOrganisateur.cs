namespace MjaARES.Api.Entities.DbSets
{
    public class EvenementOrganisateur
    {
        public Guid EvenementId { get; set; }
        public Guid OrganisateurId { get; set; }


        public Evenement Evenement { get; set; }
        public Organisateur Organisateur { get; set; }
    }
}
