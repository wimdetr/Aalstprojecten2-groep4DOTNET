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
        public DbSet<InterneMailJobcoach> InterneMailJobcoaches { get; set; }
        public DbSet<Doelgroep> Doelgroepen { get; set; }
        public DbSet<AdminMail> AdminMails { get; set; }
        public DbSet<Departement> Departementen { get; set; }
        public DbSet<Admin> Admins { get; set; }

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
            builder.Entity<InterneMail>(MapInterneMail);
            builder.Entity<InterneMailJobcoach>(MapInterneMailJobcoach);
            builder.Entity<AdminMail>(MapAdminMail);
            builder.Entity<Doelgroep>(MapDoelgroep);
            builder.Entity<Admin>(MapAdmins);
            builder.Entity<Departement>(MapDepartementen);
        }

        private void MapAdmins(EntityTypeBuilder<Admin> a)
        {
            a.ToTable("Administrator");
            a.HasKey(t => t.Email);

            a.Property(t => t.Naam).IsRequired();
            a.Property(t => t.Voornaam).IsRequired();
            a.Property(t => t.Superadmin).IsRequired();
        }

        private void MapAdminMail(EntityTypeBuilder<AdminMail> a)
        {
            a.ToTable("AdminMail");
            a.HasKey(t => t.AdminMailId);

            a.Property(t => t.Onderwerp).HasMaxLength(100).IsRequired();
            a.Property(t => t.Inhoud).IsRequired();
            a.Property(t => t.IsGelezen).IsRequired();
            a.Property(t => t.VerzendDatum).IsRequired();

            a.HasOne(t => t.Afzender)
                .WithMany()
                .HasForeignKey(t => t.AfzenderMail)
                .IsRequired();
            a.HasOne(t => t.Ontvanger).WithMany().HasForeignKey(t => t.OntvangerMail).IsRequired();
        }

        private void MapDoelgroep(EntityTypeBuilder<Doelgroep> d)
        {
            d.ToTable("Doelgroep");
            d.HasKey(t => t.DoelgroepId);

            d.Property(t => t.DoelgroepText).IsRequired();
            d.Property(t => t.DoelgroepWaarde).IsRequired();
            d.Property(t => t.DoelgroepMaxLoon).IsRequired();
        }

        private void MapInterneMailJobcoach(EntityTypeBuilder<InterneMailJobcoach> i)
        {
            i.HasKey(t => t.Id);

            i.HasOne(t => t.Jobcoach)
                .WithMany(t => t.InterneMailJobcoaches)
                .HasForeignKey(t => t.JobcoachEmail)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            i.HasOne(t => t.InterneMail)
                .WithMany()
                .HasForeignKey(t => t.InterneMailId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void MapInterneMail(EntityTypeBuilder<InterneMail> m)
        {
            m.ToTable("InterneMail");
            m.HasKey(t => t.InterneMailId);

            m.Property(t => t.Onderwerp).HasMaxLength(100).IsRequired();
            m.Property(t => t.Inhoud).IsRequired();
            m.Property(t => t.VerzendDatum).IsRequired();

            m.HasOne(t => t.Afzender).WithMany().IsRequired().OnDelete(DeleteBehavior.Cascade);
        }

        private void MapKOBVak(EntityTypeBuilder<KOBVak> v)
        {
            v.ToTable("KostOfBaatVak");
            v.HasKey(t =>t.id);

            v.Property(t => t.KOBVakId).IsRequired();
            v.Property(t => t.Data).IsRequired();
        }

        private void MapKOBRij(EntityTypeBuilder<KOBRij> r)
        {
            r.ToTable("KostOfBaatRij");
            r.HasKey(t =>t.id);

            r.Property(t => t.KOBRijId).IsRequired();
            r.HasMany(t => t.Vakken).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);
        }

        private void MapKostOfBaat(EntityTypeBuilder<KostOfBaat> k)
        {
            k.ToTable("KostOfBaat");
            k.HasKey(t =>t.id);


            k.Property(t => t.VraagId).IsRequired();
            k.Property(t => t.KostOfBaatEnum).IsRequired();
            k.Property(t => t.Formule).IsRequired();
            k.HasMany(t => t.Rijen).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);
        }

        private void MapDepartementen(EntityTypeBuilder<Departement> d)
        {
            d.ToTable("Departementen");
            d.HasKey(t => t.DepartementId);

            d.Property(t => t.AnalyseId).IsRequired(false);
            d.Property(t => t.AantalWerkuren).IsRequired();
            d.Property(t => t.Bus).HasMaxLength(1).IsRequired(false);
            d.Property(t => t.Gemeente).IsRequired();
            d.Property(t => t.Naam).IsRequired();
            d.Property(t => t.Nummer).IsRequired();
            d.Property(t => t.Postcode).HasMaxLength(4).IsRequired();
            d.Property(t => t.Straat).IsRequired();

            d.HasOne(t => t.Werkgever).WithMany().IsRequired();
        }

        private void MapWerkgever(EntityTypeBuilder<Werkgever> w)
        {
            w.ToTable("Werkgever");
            w.HasKey(t => t.WerkgeverId);

            w.Property(t => t.LinkNaarLogoPrent).IsRequired(false);
            w.Property(t => t.Naam).IsRequired();
            w.Property(t => t.PatronaleBijdrage).HasMaxLength(3).IsRequired();
        }

        private void MapAnalyse(EntityTypeBuilder<Analyse> a)
        {
            a.ToTable("Analyse");
            a.HasKey(t => t.AnalyseId);

            a.Property(t => t.JobCoachEmail).IsRequired();
            a.Property(t => t.LaatsteAanpasDatum).IsRequired();
            a.Property(t => t.IsGearchiveerd).IsRequired();
            a.Property(t => t.IsVerwijderd).IsRequired();

            a.HasMany(t => t.KostenEnBaten).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);
            a.HasOne(t => t.Departement).WithOne().IsRequired().OnDelete(DeleteBehavior.Restrict);
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

            j.HasMany(t => t.Analyses).WithOne().IsRequired().OnDelete(DeleteBehavior.Restrict);
        }


        private static void MapPersoon(EntityTypeBuilder<Persoon> p)
        {
            p.ToTable("Persoon");

            p.HasKey(t => t.Email);

            p.HasDiscriminator<string>("Type").HasValue<JobCoach>("JobCoach");

            p.Property(t => t.Email).IsRequired();
            p.Property(t => t.Naam).IsRequired();
            p.Property(t => t.Voornaam).IsRequired();
        }
    }
}
