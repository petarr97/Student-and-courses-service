using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjekatPage.Models1
{
    public partial class PageBazaContext : DbContext
    {
        public PageBazaContext()
        {
        }

        public PageBazaContext(DbContextOptions<PageBazaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Korisnici> Korisnici { get; set; }
        public virtual DbSet<Kurs> Kurs { get; set; }
        public virtual DbSet<StatusStudenta> StatusStudenta { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Studentkurs> Studentkurs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=desktop-kg2i6n9;Database=PageBaza;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Korisnici>(entity =>
            {
                entity.ToTable("KORISNICI");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Kurs>(entity =>
            {
                entity.ToTable("KURS");

                entity.Property(e => e.Kursid).HasColumnName("KURSID");

                entity.Property(e => e.Nazivkursa)
                    .HasColumnName("NAZIVKURSA")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StatusStudenta>(entity =>
            {
                entity.ToTable("STATUS_STUDENTA");

                entity.Property(e => e.Statusstudentaid).HasColumnName("STATUSSTUDENTAID");

                entity.Property(e => e.Nazivstatusa)
                    .HasColumnName("NAZIVSTATUSA")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("STUDENT");

                entity.HasIndex(e => e.Statusstudentaid)
                    .HasName("RELATIONSHIP_4_FK");

                entity.Property(e => e.Studentid).HasColumnName("STUDENTID");

                entity.Property(e => e.Brojindeksa)
                    .HasColumnName("BROJINDEKSA")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Godina).HasColumnName("GODINA");

                entity.Property(e => e.Ime)
                    .HasColumnName("IME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Prezime)
                    .HasColumnName("PREZIME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Statusstudentaid).HasColumnName("STATUSSTUDENTAID");
            });

            modelBuilder.Entity<Studentkurs>(entity =>
            {
                entity.HasKey(e => new { e.Kursid, e.Studentid });

                entity.ToTable("STUDENTKURS");

                entity.HasIndex(e => e.Kursid)
                    .HasName("RELATIONSHIP_3_FK");

                entity.HasIndex(e => e.Studentid)
                    .HasName("RELATIONSHIP_2_FK");

                entity.Property(e => e.Kursid).HasColumnName("KURSID");

                entity.Property(e => e.Studentid).HasColumnName("STUDENTID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
