using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ComputacionMovilAPI.Models
{
    public partial class ComputacionMovilDbContext : DbContext
    {
        public ComputacionMovilDbContext()
        {
        }

        public ComputacionMovilDbContext(DbContextOptions<ComputacionMovilDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EventoWRK> EventoWRK { get; set; }
        public virtual DbSet<HitoMSTR> HitoMSTR { get; set; }
        public virtual DbSet<InfanteHitoXREF> InfanteHitoXREF { get; set; }
        public virtual DbSet<InfanteMSTR> InfanteMSTR { get; set; }
        public virtual DbSet<OrganizacionMSTR> OrganizacionMSTR { get; set; }
        public virtual DbSet<PedriataMSTR> PedriataMSTR { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ComputacionMovilDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventoWRK>(entity =>
            {
                entity.HasOne(d => d.Hito)
                    .WithMany(p => p.EventoWRK)
                    .HasForeignKey(d => d.HitoID)
                    .HasConstraintName("FK_EventoWRK_HitoMSTR");

                entity.HasOne(d => d.Infante)
                    .WithMany(p => p.EventoWRK)
                    .HasForeignKey(d => d.InfanteID)
                    .HasConstraintName("FK_EventoWRK_InfanteMSTR");
            });

            modelBuilder.Entity<HitoMSTR>(entity =>
            {
                entity.Property(e => e.HitoDescripcion).IsUnicode(false);
            });

            modelBuilder.Entity<InfanteHitoXREF>(entity =>
            {
                entity.HasKey(e => new { e.HitoID, e.InfanteID });

                entity.HasOne(d => d.Hito)
                    .WithMany(p => p.InfanteHitoXREF)
                    .HasForeignKey(d => d.HitoID)
                    .HasConstraintName("FK_InfanteHitoXREF_HitoMSTR");

                entity.HasOne(d => d.Infante)
                    .WithMany(p => p.InfanteHitoXREF)
                    .HasForeignKey(d => d.InfanteID)
                    .HasConstraintName("FK_InfanteHitoXREF_InfanteMSTR");
            });

            modelBuilder.Entity<InfanteMSTR>(entity =>
            {
                entity.Property(e => e.CorreoElectronico).IsUnicode(false);

                entity.Property(e => e.EdadMeses).HasComputedColumnSql("(datediff(month,[FechaDeNacimiento],getdate()))");

                entity.Property(e => e.EdadYear).HasComputedColumnSql("(CONVERT([decimal](18,2),CONVERT([float],datediff(month,[FechaDeNacimiento],getdate()))/CONVERT([float],(12))))");

                entity.Property(e => e.InfanteNombre).IsUnicode(false);

                entity.Property(e => e.NotificarPorCorreo).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Pedriata)
                    .WithMany(p => p.InfanteMSTR)
                    .HasForeignKey(d => d.PedriataID)
                    .HasConstraintName("FK_InfanteMSTR_PedriataMSTR");
            });

            modelBuilder.Entity<OrganizacionMSTR>(entity =>
            {
                entity.Property(e => e.OrganizacionNombre).IsUnicode(false);
            });

            modelBuilder.Entity<PedriataMSTR>(entity =>
            {
                entity.Property(e => e.CorreoElectronico).IsUnicode(false);

                entity.Property(e => e.NotificarPorCorreo).HasDefaultValueSql("((0))");

                entity.Property(e => e.PedriataNombre).IsUnicode(false);

                entity.HasOne(d => d.Organizacion)
                    .WithMany(p => p.PedriataMSTR)
                    .HasForeignKey(d => d.OrganizacionID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PedriataMSTR_OrganizacionMSTR");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
