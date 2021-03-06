// <auto-generated />
using System;
using AgendaVeterinaria1.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgendaVeterinaria1.Migrations
{
    [DbContext(typeof(AgendaDBContext))]
    partial class AgendaDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AgendaVeterinaria1.Models.Agenda", b =>
                {
                    b.Property<int>("IDAgenda")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDAgenda"), 1L, 1);

                    b.Property<DateTime>("FechaDesde")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaHasta")
                        .HasColumnType("datetime2");

                    b.Property<string>("FranjaHoraria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IDProfesional")
                        .HasColumnType("int");

                    b.Property<int>("ProfesionalIDProfesional")
                        .HasColumnType("int");

                    b.Property<int>("TopeDeTurnos")
                        .HasColumnType("int");

                    b.HasKey("IDAgenda");

                    b.HasIndex("ProfesionalIDProfesional");

                    b.ToTable("Agendas");
                });

            modelBuilder.Entity("AgendaVeterinaria1.Models.Cliente", b =>
                {
                    b.Property<int>("IDCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDCliente"), 1L, 1);

                    b.Property<int>("DNI")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IDUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioIDUsuario")
                        .HasColumnType("int");

                    b.HasKey("IDCliente");

                    b.HasIndex("UsuarioIDUsuario");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("AgendaVeterinaria1.Models.Mascota", b =>
                {
                    b.Property<int>("IDMascota")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDMascota"), 1L, 1);

                    b.Property<int?>("ClienteIDCliente")
                        .HasColumnType("int");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoMascota")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDMascota");

                    b.HasIndex("ClienteIDCliente");

                    b.ToTable("Mascotas");
                });

            modelBuilder.Entity("AgendaVeterinaria1.Models.Profesional", b =>
                {
                    b.Property<int>("IDProfesional")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDProfesional"), 1L, 1);

                    b.Property<int>("DNI")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IDUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoProfesional")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioIDUsuario")
                        .HasColumnType("int");

                    b.HasKey("IDProfesional");

                    b.HasIndex("UsuarioIDUsuario");

                    b.ToTable("Profesionales");
                });

            modelBuilder.Entity("AgendaVeterinaria1.Models.Turno", b =>
                {
                    b.Property<int>("IDTurno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDTurno"), 1L, 1);

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Horario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IDCliente")
                        .HasColumnType("int");

                    b.Property<int>("IDProfesional")
                        .HasColumnType("int");

                    b.Property<string>("TipoDeTurno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDTurno");

                    b.HasIndex("IDCliente");

                    b.HasIndex("IDProfesional");

                    b.ToTable("Turnos");
                });

            modelBuilder.Entity("AgendaVeterinaria1.Models.Usuario", b =>
                {
                    b.Property<int>("IDUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDUsuario"), 1L, 1);

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoDeUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDUsuario");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("AgendaVeterinaria1.Models.Agenda", b =>
                {
                    b.HasOne("AgendaVeterinaria1.Models.Profesional", "Profesional")
                        .WithMany()
                        .HasForeignKey("ProfesionalIDProfesional")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profesional");
                });

            modelBuilder.Entity("AgendaVeterinaria1.Models.Cliente", b =>
                {
                    b.HasOne("AgendaVeterinaria1.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioIDUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AgendaVeterinaria1.Models.Mascota", b =>
                {
                    b.HasOne("AgendaVeterinaria1.Models.Cliente", null)
                        .WithMany("Mascotas")
                        .HasForeignKey("ClienteIDCliente");
                });

            modelBuilder.Entity("AgendaVeterinaria1.Models.Profesional", b =>
                {
                    b.HasOne("AgendaVeterinaria1.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioIDUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AgendaVeterinaria1.Models.Turno", b =>
                {
                    b.HasOne("AgendaVeterinaria1.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("IDCliente")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AgendaVeterinaria1.Models.Profesional", "Profesional")
                        .WithMany()
                        .HasForeignKey("IDProfesional")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Profesional");
                });

            modelBuilder.Entity("AgendaVeterinaria1.Models.Cliente", b =>
                {
                    b.Navigation("Mascotas");
                });
#pragma warning restore 612, 618
        }
    }
}
