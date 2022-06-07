using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaVeterinaria1.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IDUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDeUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IDUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IDCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDUsuario = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNI = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IDCliente);
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profesionales",
                columns: table => new
                {
                    IDProfesional = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDUsuario = table.Column<int>(type: "int", nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DNI = table.Column<int>(type: "int", nullable: false),
                    TipoProfesional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesionales", x => x.IDProfesional);
                    table.ForeignKey(
                        name: "FK_Profesionales_Usuarios_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mascotas",
                columns: table => new
                {
                    IDMascota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoMascota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteIDCliente = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mascotas", x => x.IDMascota);
                    table.ForeignKey(
                        name: "FK_Mascotas_Clientes_ClienteIDCliente",
                        column: x => x.ClienteIDCliente,
                        principalTable: "Clientes",
                        principalColumn: "IDCliente");
                });

            migrationBuilder.CreateTable(
                name: "Agendas",
                columns: table => new
                {
                    IDAgenda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FranjaHoraria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopeDeTurnos = table.Column<int>(type: "int", nullable: false),
                    FechaDesde = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaHasta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDProfesional = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendas", x => x.IDAgenda);
                    table.ForeignKey(
                        name: "FK_Agendas_Profesionales_IDProfesional",
                        column: x => x.IDProfesional,
                        principalTable: "Profesionales",
                        principalColumn: "IDProfesional",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    IDTurno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDeTurno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Horario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDMascota = table.Column<int>(type: "int", nullable: false),
                    IDProfesional = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.IDTurno);
                    table.ForeignKey(
                        name: "FK_Turnos_Mascotas_IDMascota",
                        column: x => x.IDMascota,
                        principalTable: "Mascotas",
                        principalColumn: "IDMascota");
                    table.ForeignKey(
                        name: "FK_Turnos_Profesionales_IDProfesional",
                        column: x => x.IDProfesional,
                        principalTable: "Profesionales",
                        principalColumn: "IDProfesional");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendas_IDProfesional",
                table: "Agendas",
                column: "IDProfesional");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_IDUsuario",
                table: "Clientes",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_ClienteIDCliente",
                table: "Mascotas",
                column: "ClienteIDCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Profesionales_IDUsuario",
                table: "Profesionales",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_IDMascota",
                table: "Turnos",
                column: "IDMascota");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_IDProfesional",
                table: "Turnos",
                column: "IDProfesional");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendas");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Mascotas");

            migrationBuilder.DropTable(
                name: "Profesionales");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
