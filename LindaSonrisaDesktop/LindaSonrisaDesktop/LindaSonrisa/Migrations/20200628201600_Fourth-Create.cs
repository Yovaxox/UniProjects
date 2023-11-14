using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaSonrisa.Migrations
{
    public partial class FourthCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FILE_ID",
                table: "USUARIO",
                type: "NVARCHAR2(250)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FILE_ID",
                table: "SERVICIO",
                type: "NVARCHAR2(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(250)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FILE_ID",
                table: "USUARIO");

            migrationBuilder.AlterColumn<string>(
                name: "FILE_ID",
                table: "SERVICIO",
                type: "NVARCHAR2(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(250)",
                oldNullable: true);
        }
    }
}
