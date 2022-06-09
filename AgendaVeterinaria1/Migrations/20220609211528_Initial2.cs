using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaVeterinaria1.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Especialidades",
                columns: table => new
                {
                    IDEspecialidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades", x => x.IDEspecialidad);
                });

            migrationBuilder.CreateTable(
                name: "EspecialidadProfesional",
                columns: table => new
                {
                    EspecialidadesIDEspecialidad = table.Column<int>(type: "int", nullable: false),
                    ProfesionalesIDProfesional = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecialidadProfesional", x => new { x.EspecialidadesIDEspecialidad, x.ProfesionalesIDProfesional });
                    table.ForeignKey(
                        name: "FK_EspecialidadProfesional_Especialidades_EspecialidadesIDEspecialidad",
                        column: x => x.EspecialidadesIDEspecialidad,
                        principalTable: "Especialidades",
                        principalColumn: "IDEspecialidad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EspecialidadProfesional_Profesionales_ProfesionalesIDProfesional",
                        column: x => x.ProfesionalesIDProfesional,
                        principalTable: "Profesionales",
                        principalColumn: "IDProfesional",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EspecialidadProfesional_ProfesionalesIDProfesional",
                table: "EspecialidadProfesional",
                column: "ProfesionalesIDProfesional");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EspecialidadProfesional");

            migrationBuilder.DropTable(
                name: "Especialidades");
        }
    }
}
