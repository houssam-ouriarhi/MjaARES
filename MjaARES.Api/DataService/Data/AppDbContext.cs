using Microsoft.EntityFrameworkCore;
using MjaARES.Api.Entities.DbSets;

namespace MjaARES.Api.DataService.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { }

        // Tables
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<Domaine> Domaines { get; set; }
        public DbSet<Organisateur> Organisateurs { get; set; }
        public DbSet<Sexe> Sexes { get; set; }
        public DbSet<TrancheAge> TrancheAges { get; set; }

        // Join tables 
        public DbSet<DomaineEvenement> DomaineEvenements { get; set; }
        public DbSet<EvenementOrganisateur> EvenementOrganisateurs { get; set; }
        public DbSet<SexeTrancheAgeEvenement> SexeTrancheAgeEvenements { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Categorie Table
            modelBuilder.Entity<Categorie>()
                .HasMany(e => e.Evenements)
                .WithOne(e => e.Categorie)
                .HasForeignKey(e => e.Id)
                .IsRequired();

            // Evenement Table
            modelBuilder.Entity<Evenement>()
                .HasOne(e => e.Categorie)
                .WithMany(e => e.Evenements)
                .HasForeignKey(e => e.CategorieId)
                .IsRequired();

            // DomaineEvenement Tabele
            modelBuilder.Entity<DomaineEvenement>()
                .HasKey(de => new { de.DomaineId, de.EvenementId });

            modelBuilder.Entity<DomaineEvenement>()
                .HasOne(d => d.Domaine)
                .WithMany(de => de.DomaineEvenements)
                .HasForeignKey(d => d.DomaineId);

            modelBuilder.Entity<DomaineEvenement>()
                .HasOne(e => e.Evenement)
                .WithMany(de => de.DomaineEvenements)
                .HasForeignKey(e => e.EvenementId);

            // EvenementOrganisateur Tabele
            modelBuilder.Entity<EvenementOrganisateur>()
                .HasKey(eo => new { eo.EvenementId, eo.OrganisateurId });

            modelBuilder.Entity<EvenementOrganisateur>()
                .HasOne(e => e.Evenement)
                .WithMany(eo => eo.EvenementOrganisateurs)
                .HasForeignKey(e => e.EvenementId);

            modelBuilder.Entity<EvenementOrganisateur>()
                .HasOne(o => o.Organisateur)
                .WithMany(eo => eo.EvenementOrganisateurs)
                .HasForeignKey(o => o.OrganisateurId);


            //SexeTrancheAgeEvenement
            modelBuilder.Entity<SexeTrancheAgeEvenement>()
                .HasKey(x => new { x.SexeId, x.TrancheAgeId, x.EvenementId });

            modelBuilder.Entity<SexeTrancheAgeEvenement>()
                .HasOne(s => s.Sexe)
                .WithMany(x => x.SexeTrancheAgeEvenements)
                .HasForeignKey(s => s.SexeId);

            modelBuilder.Entity<SexeTrancheAgeEvenement>()
                .HasOne(t => t.TrancheAge)
                .WithMany(x => x.SexeTrancheAgeEvenements)
                .HasForeignKey(t => t.TrancheAgeId);

            modelBuilder.Entity<SexeTrancheAgeEvenement>()
                .HasOne(e => e.Evenement)
                .WithMany(x => x.SexeTrancheAgeEvenements)
                .HasForeignKey(e => e.EvenementId);
        }
    }
}
