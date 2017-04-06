using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Aalstprojecten2_groep4DOTNET.Models;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aalstprojecten2_groep4DOTNET.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<JobCoach> JobCoaches { get; set; }
        public DbSet<Analyse> Analyses { get; set; }
        public DbSet<Werkgever> Werkgevers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Persoon>(MapPersoon);
            builder.Entity<JobCoach>(MapJobCoach);
            builder.Entity<Analyse>(MapAnalyse);
            builder.Entity<Werkgever>(MapWerkgever);
            builder.Entity<KostOfBaat>(MapKostOfBaat);
            builder.Entity<KOBRij>(MapKOBRij);
            builder.Entity<KOBVak>(MapKOBVak);
        }

        private void MapKOBVak(EntityTypeBuilder<KOBVak> v)
        {
            v.ToTable("KostOfBaatVak");
            v.HasKey(t => new { t.KOBVakId, t.KOBRijId, t.KostOfBaatId, t.KostOfBaatEnum, t.AnalyseId, t.JobCoachEmail});

            v.Property(t => t.Data).IsRequired();
        }

        private void MapKOBRij(EntityTypeBuilder<KOBRij> r)
        {
            r.ToTable("KostOfBaatRij");
            r.HasKey(t => new { t.KOBRijId, t.KostOfBaatId, t.KostOfBaatEnum, t.AnalyseId, t.JobCoachEmail});

            r.HasMany(t => t.Vakken).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);
        }

        private void MapKostOfBaat(EntityTypeBuilder<KostOfBaat> k)
        {
            k.ToTable("KostOfBaat");
            k.HasKey(t => new {t.KostOfBaatId, t.KostOfBaatEnum, t.AnalyseId, t.JobCoachEmail});

            k.Property(t => t.Formule).IsRequired();
            k.HasMany(t => t.Rijen).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);
        }

        private void MapWerkgever(EntityTypeBuilder<Werkgever> w)
        {
            w.ToTable("Werkgever");
            w.HasKey(t => t.WerkgeverId);

            w.Property(t => t.AantalWerkuren).IsRequired();
            w.Property(t => t.Bus).HasMaxLength(1).IsRequired(false);
            w.Property(t => t.Gemeente).IsRequired();
            w.Property(t => t.LinkNaarLogoPrent).IsRequired(false);
            w.Property(t => t.Naam).IsRequired();
            w.Property(t => t.NaamAfdeling).IsRequired();
            w.Property(t => t.Nummer).IsRequired();
            w.Property(t => t.PatronaleBijdrage).HasMaxLength(3).IsRequired();
            w.Property(t => t.Postcode).HasMaxLength(4).IsRequired();
            w.Property(t => t.Straat).IsRequired(false);
            w.Property(t => t.WerkgeverId).ValueGeneratedOnAdd();

            w.HasOne(t => t.ContactPersoon).WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
        }

        private void MapAnalyse(EntityTypeBuilder<Analyse> a)
        {
            a.ToTable("Analyse");
            a.HasKey(t => new { t.AnalyseId, t.JobCoachEmail});

            a.Property(t => t.LaatsteAanpasDatum).IsRequired();
            a.Property(t => t.IsGearchiveerd).IsRequired();

            a.HasMany(t => t.KostenEnBaten).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);
            a.HasOne(t => t.Werkgever).WithMany().IsRequired(false).OnDelete(DeleteBehavior.Restrict);
        }

        private void MapJobCoach(EntityTypeBuilder<JobCoach> j)
        {
            j.Property(t => t.GemeenteBedrijf).IsRequired();
            j.Property(t => t.NaamBedrijf).IsRequired();
            j.Property(t => t.NummerBedrijf).IsRequired();
            j.Property(t => t.BusBedrijf).HasMaxLength(1).IsRequired(false);
            j.Property(t => t.PostcodeBedrijf).HasMaxLength(4).IsRequired();
            j.Property(t => t.StraatBedrijf).IsRequired();
            j.Property(t => t.MoetWachtwoordVeranderen).IsRequired();
            j.Property(t => t.Wachtwoord).IsRequired();

            j.HasMany(t => t.Analyses).WithOne().IsRequired().OnDelete(DeleteBehavior.Restrict);
        }


        private static void MapPersoon(EntityTypeBuilder<Persoon> p)
        {
            p.ToTable("Persoon");

            p.HasKey(t => t.Email);

            p.HasDiscriminator<string>("Type").HasValue<JobCoach>("JobCoach").HasValue<ContactPersoon>("ContactPersoon");

            p.Property(t => t.Email).IsRequired();
            p.Property(t => t.Naam).IsRequired();
            p.Property(t => t.Voornaam).IsRequired();
        }
    }
}
