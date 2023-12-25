namespace MjaARES.Api.Entities.DbSets
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Libelle { get; set; }
        public DateTime created_at { get; set; } = DateTime.UtcNow; 
        public DateTime updated_at { get; set;} = DateTime.UtcNow;

        public DateTime? Deleted_at { get; set; }
        public bool IsDeleted { get; set; }= false;

    }
}
