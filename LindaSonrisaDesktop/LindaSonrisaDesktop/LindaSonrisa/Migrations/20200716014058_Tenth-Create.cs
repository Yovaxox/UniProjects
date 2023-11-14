using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaSonrisa.Migrations
{
    public partial class TenthCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "FAMILIA_PRODUCTO_ID_SEQ",
                startValue: 100L,
                minValue: 100L,
                maxValue: 999L);

            migrationBuilder.CreateSequence<int>(
                name: "ORDEN_ID_SEQ");

            migrationBuilder.CreateSequence<int>(
                name: "PROVEEDOR_ID_SEQ",
                startValue: 100L,
                minValue: 100L,
                maxValue: 999L);

            migrationBuilder.CreateSequence<int>(
                name: "TIPO_PRODUCTO_ID_SEQ",
                startValue: 100L,
                minValue: 100L,
                maxValue: 999L);

            migrationBuilder.CreateTable(
                name: "ORDEN",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    SOLICITADO_EL = table.Column<DateTime>(nullable: false),
                    ACTUALIZADO_EL = table.Column<DateTime>(nullable: true),
                    FUE_RECEPCIONADO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDEN", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DETALLE_ORDEN",
                columns: table => new
                {
                    ORDEN_ID = table.Column<int>(nullable: false),
                    PRODUCTO_ID = table.Column<int>(nullable: false),
                    CANTIDAD = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DETALLE_ORDEN", x => new { x.ORDEN_ID, x.PRODUCTO_ID });
                    table.ForeignKey(
                        name: "ORDEN_DETALLE_ORDEN_FK",
                        column: x => x.ORDEN_ID,
                        principalTable: "ORDEN",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "PRODUCTO_DETALLE_ORDEN_FK",
                        column: x => x.PRODUCTO_ID,
                        principalTable: "PRODUCTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RECEPCION_ORDEN",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    CODIFICACION = table.Column<string>(type: "VARCHAR2(25)", nullable: true),
                    RECEPCIONADO_EL = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false),
                    CANTIDAD = table.Column<int>(nullable: false),
                    PRODUCTO = table.Column<string>(type: "VARCHAR2(50)", nullable: true),
                    PRODUCTO_ID = table.Column<int>(nullable: false),
                    ORDEN_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RECEPCION_ORDEN", x => x.ID);
                    table.ForeignKey(
                        name: "RECEPCION_ORDEN_FK",
                        column: x => x.ORDEN_ID,
                        principalTable: "ORDEN",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DETALLE_ORDEN_PRODUCTO_ID",
                table: "DETALLE_ORDEN",
                column: "PRODUCTO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RECEPCION_ORDEN_ORDEN_ID",
                table: "RECEPCION_ORDEN",
                column: "ORDEN_ID");

            // Disparadores

            migrationBuilder.Sql("CREATE OR REPLACE TRIGGER reserva_id_trg BEFORE INSERT ON reserva FOR EACH ROW BEGIN :new.id := reserva_id_seq.nextval; END;");

            migrationBuilder.Sql("CREATE OR REPLACE TRIGGER tipo_producto_id_trg BEFORE INSERT ON tipo_producto FOR EACH ROW BEGIN :new.id := tipo_producto_id_seq.nextval; END;");
            migrationBuilder.Sql("CREATE OR REPLACE TRIGGER familia_producto_id_trg BEFORE INSERT ON familia_producto FOR EACH ROW BEGIN :new.id := familia_producto_id_seq.nextval; END;");
            migrationBuilder.Sql("CREATE OR REPLACE TRIGGER proveedor_id_trg BEFORE INSERT ON proveedor FOR EACH ROW BEGIN :new.id := proveedor_id_seq.nextval; END;");
            migrationBuilder.Sql("CREATE OR REPLACE TRIGGER orden_id_trg BEFORE INSERT ON orden FOR EACH ROW BEGIN :new.id := orden_id_seq.nextval; END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DETALLE_ORDEN");

            migrationBuilder.DropTable(
                name: "RECEPCION_ORDEN");

            migrationBuilder.DropTable(
                name: "ORDEN");

            migrationBuilder.DropSequence(
                name: "FAMILIA_PRODUCTO_ID_SEQ");

            migrationBuilder.DropSequence(
                name: "ORDEN_ID_SEQ");

            migrationBuilder.DropSequence(
                name: "PROVEEDOR_ID_SEQ");

            migrationBuilder.DropSequence(
                name: "TIPO_PRODUCTO_ID_SEQ");
        }
    }
}
