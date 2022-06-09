using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaVeterinaria1.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_IDUsuario",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Profesionales_Usuarios_IDUsuario",
                table: "Profesionales");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Profesionales_IDUsuario",
                table: "Profesionales");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_IDUsuario",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "IDUsuario",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "IDUsuario",
                table: "Clientes");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Profesionales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contraseña",
                table: "Profesionales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Contraseña",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contraseña",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "Contraseña",
                table: "Clientes");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Profesionales",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IDUsuario",
                table: "Profesionales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IDUsuario",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IDUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDeUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IDUsuario);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profesionales_IDUsuario",
                table: "Profesionales",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_IDUsuario",
                table: "Clientes",
                column: "IDUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Usuarios_IDUsuario",
                table: "Clientes",
                column: "IDUsuario",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profesionales_Usuarios_IDUsuario",
                table: "Profesionales",
                column: "IDUsuario",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
