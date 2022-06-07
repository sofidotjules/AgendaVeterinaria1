using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaVeterinaria1.Migrations
{
    public partial class AgendaVeterinaria1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendas_Profesionales_ProfesionalIDProfesional",
                table: "Agendas");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_UsuarioIDUsuario",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Profesionales_Usuarios_UsuarioIDUsuario",
                table: "Profesionales");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Clientes_IDCliente",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Profesionales_UsuarioIDUsuario",
                table: "Profesionales");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_UsuarioIDUsuario",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Agendas_ProfesionalIDProfesional",
                table: "Agendas");

            migrationBuilder.DropColumn(
                name: "UsuarioIDUsuario",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "UsuarioIDUsuario",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ProfesionalIDProfesional",
                table: "Agendas");

            migrationBuilder.RenameColumn(
                name: "IDCliente",
                table: "Turnos",
                newName: "IDMascota");

            migrationBuilder.RenameIndex(
                name: "IX_Turnos_IDCliente",
                table: "Turnos",
                newName: "IX_Turnos_IDMascota");

            migrationBuilder.AlterColumn<int>(
                name: "IDProfesional",
                table: "Profesionales",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_IDUsuario",
                table: "Clientes",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Agendas_IDProfesional",
                table: "Agendas",
                column: "IDProfesional");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendas_Profesionales_IDProfesional",
                table: "Agendas",
                column: "IDProfesional",
                principalTable: "Profesionales",
                principalColumn: "IDProfesional",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Usuarios_IDUsuario",
                table: "Clientes",
                column: "IDUsuario",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profesionales_Usuarios_IDProfesional",
                table: "Profesionales",
                column: "IDProfesional",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Mascotas_IDMascota",
                table: "Turnos",
                column: "IDMascota",
                principalTable: "Mascotas",
                principalColumn: "IDMascota");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendas_Profesionales_IDProfesional",
                table: "Agendas");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_IDUsuario",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Profesionales_Usuarios_IDProfesional",
                table: "Profesionales");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Mascotas_IDMascota",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_IDUsuario",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Agendas_IDProfesional",
                table: "Agendas");

            migrationBuilder.RenameColumn(
                name: "IDMascota",
                table: "Turnos",
                newName: "IDCliente");

            migrationBuilder.RenameIndex(
                name: "IX_Turnos_IDMascota",
                table: "Turnos",
                newName: "IX_Turnos_IDCliente");

            migrationBuilder.AlterColumn<int>(
                name: "IDProfesional",
                table: "Profesionales",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioIDUsuario",
                table: "Profesionales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioIDUsuario",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfesionalIDProfesional",
                table: "Agendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Profesionales_UsuarioIDUsuario",
                table: "Profesionales",
                column: "UsuarioIDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UsuarioIDUsuario",
                table: "Clientes",
                column: "UsuarioIDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Agendas_ProfesionalIDProfesional",
                table: "Agendas",
                column: "ProfesionalIDProfesional");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendas_Profesionales_ProfesionalIDProfesional",
                table: "Agendas",
                column: "ProfesionalIDProfesional",
                principalTable: "Profesionales",
                principalColumn: "IDProfesional",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Usuarios_UsuarioIDUsuario",
                table: "Clientes",
                column: "UsuarioIDUsuario",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profesionales_Usuarios_UsuarioIDUsuario",
                table: "Profesionales",
                column: "UsuarioIDUsuario",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Clientes_IDCliente",
                table: "Turnos",
                column: "IDCliente",
                principalTable: "Clientes",
                principalColumn: "IDCliente");
        }
    }
}
