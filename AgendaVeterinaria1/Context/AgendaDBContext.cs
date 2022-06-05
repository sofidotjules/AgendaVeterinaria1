using AgendaVeterinaria1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AgendaVeterinaria1.Context
{
    public class AgendaDBContext:DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Turno> Turnos{ get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Agenda> Agenda { get; set; }


        public AgendaDBContext(DbContextOptions<AgendaDBContext> options) : base(options){
        }
//        protected override void OnConfiguring(DbContextOptionsBuilder
//       optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDBCF
//;Trusted_Connection=True;");
//        }
    }
}
