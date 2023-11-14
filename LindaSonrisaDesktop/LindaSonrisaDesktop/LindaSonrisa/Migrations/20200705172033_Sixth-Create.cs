using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaSonrisa.Migrations
{
    public partial class SixthCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "COMENTARIO",
                table: "MODULO");

            migrationBuilder.AlterColumn<string>(
                name: "HORA_TERMINO",
                table: "MODULO",
                type: "VARCHAR2(5)",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<string>(
                name: "HORA_INICIO",
                table: "MODULO",
                type: "VARCHAR2(5)",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.CreateTable(
                name: "RESERVA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    FECHA = table.Column<DateTime>(type: "DATE", nullable: false),
                    SOLICITADO_EL = table.Column<DateTime>(nullable: false),
                    FUE_ANULADA = table.Column<bool>(nullable: false),
                    USUARIO_ID = table.Column<int>(nullable: false),
                    MODULO_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESERVA", x => x.ID);
                    table.ForeignKey(
                        name: "RESERVA_MODULO_FK",
                        column: x => x.MODULO_ID,
                        principalTable: "MODULO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "RESERVA_USUARIO_FK",
                        column: x => x.USUARIO_ID,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RESERVA_MODULO_ID",
                table: "RESERVA",
                column: "MODULO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVA_USUARIO_ID",
                table: "RESERVA",
                column: "USUARIO_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RESERVA");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HORA_TERMINO",
                table: "MODULO",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR2(5)");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HORA_INICIO",
                table: "MODULO",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR2(5)");

            migrationBuilder.AddColumn<string>(
                name: "COMENTARIO",
                table: "MODULO",
                type: "VARCHAR2(250)",
                nullable: true);
        }
    }
}
