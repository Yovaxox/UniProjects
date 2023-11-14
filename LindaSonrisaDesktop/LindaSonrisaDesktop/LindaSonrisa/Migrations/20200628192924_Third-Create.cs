using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaSonrisa.Migrations
{
    public partial class ThirdCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "USUARIO_ASP_NET_USERS_FK",
                table: "USUARIO");

            migrationBuilder.CreateTable(
                name: "DIA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    NOMBRE = table.Column<string>(type: "VARCHAR2(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DIA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SERVICIO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    NOMBRE = table.Column<string>(type: "VARCHAR2(50)", maxLength: 50, nullable: false),
                    NOMBRE_NORMALIZADO = table.Column<string>(type: "VARCHAR2(50)", nullable: false),
                    DESCRIPCION = table.Column<string>(type: "VARCHAR2(250)", maxLength: 250, nullable: false),
                    PRECIO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ESTA_INACTIVO = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    ES_PROMOCION = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    DESCUENTO = table.Column<decimal>(type: "NUMBER(3,2)", nullable: false),
                    COMENTARIO = table.Column<string>(type: "VARCHAR2(250)", nullable: true),
                    FILE_ID = table.Column<string>(type: "NVARCHAR2(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERVICIO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MODULO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    HORA_INICIO = table.Column<TimeSpan>(nullable: false),
                    HORA_TERMINO = table.Column<TimeSpan>(nullable: false),
                    ESTA_INACTIVO = table.Column<bool>(nullable: false),
                    COMENTARIO = table.Column<string>(type: "VARCHAR2(250)", nullable: true),
                    USUARIO_ID = table.Column<int>(nullable: false),
                    SERVICIO_ID = table.Column<int>(nullable: false),
                    DIA_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MODULO", x => x.ID);
                    table.ForeignKey(
                        name: "MODULO_DIA_FK",
                        column: x => x.DIA_ID,
                        principalTable: "DIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "MODULO_SERVICIO_FK",
                        column: x => x.SERVICIO_ID,
                        principalTable: "SERVICIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "MODULO_USUARIO_FK",
                        column: x => x.USUARIO_ID,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MODULO_DIA_ID",
                table: "MODULO",
                column: "DIA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MODULO_SERVICIO_ID",
                table: "MODULO",
                column: "SERVICIO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MODULO_USUARIO_ID",
                table: "MODULO",
                column: "USUARIO_ID");

            migrationBuilder.CreateIndex(
                name: "NOMBRE_SERVICIO_INDEX",
                table: "SERVICIO",
                column: "NOMBRE",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "USUARIO_ASP_NET_USER_FK",
                table: "USUARIO",
                column: "ASP_NET_USER_ID",
                principalTable: "ASP_NET_USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            // Secuencias

            migrationBuilder.CreateSequence(
                name: "SERVICIO_ID_SEQ");

            migrationBuilder.CreateSequence(
                name: "MODULO_ID_SEQ");

            // Disparadores

            migrationBuilder.Sql("CREATE OR REPLACE TRIGGER servicio_id_trg BEFORE INSERT ON servicio FOR EACH ROW BEGIN :new.id := servicio_id_seq.nextval; END;");

            migrationBuilder.Sql("CREATE OR REPLACE TRIGGER modulo_id_trg BEFORE INSERT ON modulo FOR EACH ROW BEGIN :new.id := modulo_id_seq.nextval; END;");


            // Inserciones

            migrationBuilder.Sql("insert into dia values (0,'Domingo');"); 
            migrationBuilder.Sql("insert into dia values (1,'Lunes');");
            migrationBuilder.Sql("insert into dia values (2,'Martes');");
            migrationBuilder.Sql("insert into dia values (3,'Miércoles ');");
            migrationBuilder.Sql("insert into dia values (4,'Jueves');");
            migrationBuilder.Sql("insert into dia values (5,'Viernes');");
            migrationBuilder.Sql("insert into dia values (6,'Sábado');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "USUARIO_ASP_NET_USER_FK",
                table: "USUARIO");

            migrationBuilder.DropTable(
                name: "MODULO");

            migrationBuilder.DropTable(
                name: "DIA");

            migrationBuilder.DropTable(
                name: "SERVICIO");

            migrationBuilder.AddForeignKey(
                name: "USUARIO_ASP_NET_USERS_FK",
                table: "USUARIO",
                column: "ASP_NET_USER_ID",
                principalTable: "ASP_NET_USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql("DROP TRIGGER servicio_id_trg;");

            migrationBuilder.Sql("DROP TRIGGER modulo_id_trg;");
        }
    }
}
