using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaVeterinaria1.Migrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contraseña",
                table: "Profesionales",
                newName: "Contrasenia");

            migrationBuilder.RenameColumn(
                name: "Contraseña",
                table: "Clientes",
                newName: "Contrasenia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contrasenia",
                table: "Profesionales",
                newName: "Contraseña");

            migrationBuilder.RenameColumn(
                name: "Contrasenia",
                table: "Clientes",
                newName: "Contraseña");
        }
    }
}
