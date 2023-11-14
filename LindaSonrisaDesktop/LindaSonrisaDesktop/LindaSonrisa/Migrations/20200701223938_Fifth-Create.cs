using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaSonrisa.Migrations
{
    public partial class FifthCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ESTA_INACTIVO",
                table: "USUARIO",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ESTA_INACTIVO",
                table: "USUARIO");
        }
    }
}
