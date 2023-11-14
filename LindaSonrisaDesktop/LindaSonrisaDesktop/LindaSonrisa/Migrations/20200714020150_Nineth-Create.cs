using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaSonrisa.Migrations
{
    public partial class NinethCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "USER_ID",
                table: "USUARIO",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AUTH_USER",
                columns: table => new
                {
                    ID = table.Column<long>(type: "NUMBER(11)", nullable: false),
                    PASSWORD = table.Column<string>(nullable: true),
                    LAST_LOGIN = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: true),
                    IS_SUPERUSER = table.Column<bool>(nullable: false),
                    USERNAME = table.Column<string>(nullable: true),
                    FIRST_NAME = table.Column<string>(nullable: true),
                    LAST_NAME = table.Column<string>(nullable: true),
                    EMAIL = table.Column<string>(nullable: true),
                    IS_STAFF = table.Column<bool>(nullable: false),
                    IS_ACTIVE = table.Column<bool>(nullable: false),
                    DATE_JOINED = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_USER", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_USER_ID",
                table: "USUARIO",
                column: "USER_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "USUARIO_AUTH_USER_FK",
                table: "USUARIO",
                column: "USER_ID",
                principalTable: "AUTH_USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            // Secuencias

            migrationBuilder.CreateSequence(
                name: "AUTH_USER_ID_SEQ");

            // Disparadores

            migrationBuilder.Sql("CREATE OR REPLACE TRIGGER auth_user_id_trg BEFORE INSERT ON auth_user FOR EACH ROW BEGIN :new.id := auth_user_id_seq.nextval; END;");

            // Inserción en las tablas ASP_NET_USER, ASP_NET_USER_ROLE y USUARIO (datos para pruebas)

            migrationBuilder.Sql("Insert into ASP_NET_USER values ('42bc034c-2003-4903-8211-fd472c751ba5','20.419.431-9','20.419.431-9','dannygamboacaycho@gmail.com','DANNYGAMBOACAYCHO@GMAIL.COM','0','AQAAAAEAACcQAAAAEEaNyF314PdxPqEdwZoaUpPtBNu0UIDrK99B3P6L/sJAAMOLKUi/MCLgsShUUNcxiQ==','OB7BTQEHE72FMT3OLWLNL3KNRM664QRP','e7871569-0f40-4299-aec8-f70c96683f7e',null,'0','0',null,'1','0');");
            migrationBuilder.Sql("Insert into ASP_NET_USER_ROLE values ('42bc034c-2003-4903-8211-fd472c751ba5','1648f1f0-d362-4905-9cc5-77f1ebf3495a');");
            migrationBuilder.Sql("Insert into USUARIO (ID,NOMBRE,AP_PATERNO,AP_MATERNO,FECHA_NACIMIENTO,FONO_FIJO,FONO_MOVIL,DIRECCION,CREADO_EL,ACTUALIZADO_EL,ES_EXTRANJERO,COMUNA_ID,GENERO_ID,ESTADO_CIVIL_ID,ASP_NET_USER_ID) values (null,'Danny Gabriel','Gamboa','Caycho',to_date('23/03/00','DD/MM/RR'),'24802945','84802945','Pintor Agustin Abarca 449',to_date('09/06/20','DD/MM/RR'),null,'0','125','1','5','42bc034c-2003-4903-8211-fd472c751ba5');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "USUARIO_AUTH_USER_FK",
                table: "USUARIO");

            migrationBuilder.DropTable(
                name: "AUTH_USER");

            migrationBuilder.DropIndex(
                name: "IX_USUARIO_USER_ID",
                table: "USUARIO");

            migrationBuilder.AlterColumn<long>(
                name: "USER_ID",
                table: "USUARIO",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
