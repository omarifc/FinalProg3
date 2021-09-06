using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyFutbol.Models
{
    public partial class FutbolDbContext : DbContext
    {
        public DbSet<Futbolista> Futbolistas {get; set;}
        public DbSet<Equipo> Equipos {get; set;}
        public DbSet<Posicion> Posiciones {get; set;}
        public DbSet<Liga> Ligas {get; set;}
        public DbSet<Pais> Paises {get; set;}
        public DbSet<Contrato> Contratos {get; set;}
        public DbSet<FutbolistaPosicion> FutbolistaPosiciones {get; set;}
        
        public FutbolDbContext()
        {

        }

        public FutbolDbContext(DbContextOptions<FutbolDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Futbol;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<FutbolistaPosicion>().HasKey(
                fp => new {fp.FutbolistaId, fp.PosicionId});
            

            modelBuilder.Entity<FutbolistaPosicion>().HasOne<Futbolista>(fp=> fp.Futbolista)
            .WithMany(f => f.FutbolistaPosiciones)
            .HasForeignKey(fp => fp.FutbolistaId);

            modelBuilder.Entity<FutbolistaPosicion>().HasOne<Posicion>(fp=> fp.Posicion)
            .WithMany(p => p.FutbolistaPosiciones)
            .HasForeignKey(fp => fp.PosicionId);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
