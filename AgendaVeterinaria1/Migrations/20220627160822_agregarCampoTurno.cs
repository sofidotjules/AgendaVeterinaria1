using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaVeterinaria1.Migrations
{
    public partial class agregarCampoTurno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IDEspecialidad",
                table: "Turnos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_IDEspecialidad",
                table: "Turnos",
                column: "IDEspecialidad");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Especialidades_IDEspecialidad",
                table: "Turnos",
                column: "IDEspecialidad",
                principalTable: "Especialidades",
                principalColumn: "IDEspecialidad",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Especialidades_IDEspecialidad",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_IDEspecialidad",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "IDEspecialidad",
                table: "Turnos");
        }
    }
}
