using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Recepti.WebApi.Database
{
    public partial class ReceptiContext : DbContext
    {
        public ReceptiContext()
        {
        }

        public ReceptiContext(DbContextOptions<ReceptiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Favoriti> Favoriti { get; set; }
        public virtual DbSet<Kategorije> Kategorije { get; set; }
        public virtual DbSet<Komentari> Komentari { get; set; }
        public virtual DbSet<Korisnici> Korisnici { get; set; }
        public virtual DbSet<Recepti> Recepti { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=Recepti;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favoriti>(entity =>
            {
                entity.HasKey(e => e.FavoritId);

                entity.Property(e => e.FavoritId).HasColumnName("FavoritID");

                entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

                entity.Property(e => e.ReceptId).HasColumnName("ReceptID");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Favoriti)
                    .HasForeignKey(d => d.KorisnikId)
                    .HasConstraintName("FK__Favoriti__Korisn__2F10007B");

                entity.HasOne(d => d.Recept)
                    .WithMany(p => p.Favoriti)
                    .HasForeignKey(d => d.ReceptId)
                    .HasConstraintName("FK__Favoriti__Recept__300424B4");
            });

            modelBuilder.Entity<Kategorije>(entity =>
            {
                entity.HasKey(e => e.KategorijaId);

                entity.Property(e => e.KategorijaId).HasColumnName("KategorijaID");

                entity.Property(e => e.Naziv).HasMaxLength(200);
            });

            modelBuilder.Entity<Komentari>(entity =>
            {
                entity.HasKey(e => e.KomentarId);

                entity.Property(e => e.KomentarId).HasColumnName("KomentarID");

                entity.Property(e => e.DatumObjave).HasColumnType("datetime");

                entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

                entity.Property(e => e.ReceptId).HasColumnName("ReceptID");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Komentari)
                    .HasForeignKey(d => d.KorisnikId)
                    .HasConstraintName("FK__Komentari__Koris__2B3F6F97");

                entity.HasOne(d => d.Recept)
                    .WithMany(p => p.Komentari)
                    .HasForeignKey(d => d.ReceptId)
                    .HasConstraintName("FK__Komentari__Recep__2C3393D0");
            });

            modelBuilder.Entity<Korisnici>(entity =>
            {
                entity.HasKey(e => e.KorisnikId);

                entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

                entity.Property(e => e.Mail).HasMaxLength(200);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<Recepti>(entity =>
            {
                entity.HasKey(e => e.ReceptId);

                entity.Property(e => e.ReceptId).HasColumnName("ReceptID");

                entity.Property(e => e.KategorijaId).HasColumnName("KategorijaID");

                entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

                entity.Property(e => e.Level).HasMaxLength(100);

                entity.Property(e => e.Naziv).HasMaxLength(300);

                entity.Property(e => e.VrijemeKuhanja).HasMaxLength(50);

                entity.HasOne(d => d.Kategorija)
                    .WithMany(p => p.Recepti)
                    .HasForeignKey(d => d.KategorijaId)
                    .HasConstraintName("FK__Recepti__Kategor__276EDEB3");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Recepti)
                    .HasForeignKey(d => d.KorisnikId)
                    .HasConstraintName("FK__Recepti__Korisni__286302EC");
            });
        }
    }
}
