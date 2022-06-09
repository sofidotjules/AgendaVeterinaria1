using AgendaVeterinaria1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AgendaVeterinaria1.Context
{
    public class AgendaDBContext : DbContext
    {
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }


        public AgendaDBContext(DbContextOptions<AgendaDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Turno>()
            .HasKey(j => new { j.IDTurno });
            modelBuilder.Entity<Turno>()
            .HasOne(x => x.Profesional).WithMany().HasForeignKey(x => x.IDProfesional).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Turno>()
            .HasOne(x => x.Mascota).WithMany().HasForeignKey(x => x.IDMascota).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Agenda>()
            .HasOne(x => x.Profesional).WithMany().HasForeignKey(x => x.IDProfesional);

            modelBuilder.Entity<Especialidad>()
            .HasMany(x => x.Profesionales);
            modelBuilder.Entity<Profesional>()
            .HasMany(x => x.Especialidades);
        }
//        protected override void OnConfiguring(DbContextOptionsBuilder
//       optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDBCF
//;Trusted_Connection=True;");
//        }
    }
}
