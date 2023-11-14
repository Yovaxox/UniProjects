using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaSonrisa.Migrations
{
    public partial class SeventhCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FAMILIA_PRODUCTO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    TITULO = table.Column<string>(type: "VARCHAR2(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAMILIA_PRODUCTO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RUBRO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    TITULO = table.Column<string>(type: "VARCHAR2(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RUBRO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TIPO_PRODUCTO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    TITULO = table.Column<string>(type: "VARCHAR2(50)", nullable: false),
                    FAMILIA_PRODUCTO_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_PRODUCTO", x => x.ID);
                    table.ForeignKey(
                        name: "TIPO_FAMILIA_PRODUCTO_FK",
                        column: x => x.FAMILIA_PRODUCTO_ID,
                        principalTable: "FAMILIA_PRODUCTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PROVEEDOR",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    NOMBRE = table.Column<string>(type: "VARCHAR2(50)", maxLength: 50, nullable: false),
                    NOMBRE_NORMALIZADO = table.Column<string>(type: "VARCHAR2(50)", nullable: false),
                    FONO_FIJO = table.Column<string>(type: "VARCHAR2(8)", nullable: false),
                    FONO_MOVIL = table.Column<string>(type: "VARCHAR2(8)", nullable: true),
                    EMAIL = table.Column<string>(type: "VARCHAR2(50)", maxLength: 50, nullable: false),
                    URL = table.Column<string>(type: "VARCHAR2(50)", maxLength: 50, nullable: true),
                    COMENTARIO = table.Column<string>(type: "VARCHAR2(250)", maxLength: 250, nullable: true),
                    ESTA_INACTIVO = table.Column<bool>(nullable: false),
                    RUBRO_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROVEEDOR", x => x.ID);
                    table.ForeignKey(
                        name: "PROVEEDOR_RUBRO_FK",
                        column: x => x.RUBRO_ID,
                        principalTable: "RUBRO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CONTACTO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    NOMBRE = table.Column<string>(type: "VARCHAR2(50)", maxLength: 50, nullable: false),
                    AP_PATERNO = table.Column<string>(type: "VARCHAR2(50)", maxLength: 50, nullable: true),
                    AP_MATERNO = table.Column<string>(type: "VARCHAR2(50)", maxLength: 50, nullable: true),
                    FONO_MOVIL = table.Column<string>(type: "VARCHAR2(8)", maxLength: 8, nullable: false),
                    EMAIL = table.Column<string>(type: "VARCHAR2(50)", maxLength: 50, nullable: true),
                    PROVEEDOR_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTACTO", x => x.ID);
                    table.ForeignKey(
                        name: "CONTACTO_PROVEEDOR_FK",
                        column: x => x.PROVEEDOR_ID,
                        principalTable: "PROVEEDOR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    NOMBRE = table.Column<string>(type: "VARCHAR2(50)", maxLength: 50, nullable: false),
                    NOMBRE_NORMALIZADO = table.Column<string>(type: "VARCHAR2(50)", nullable: false),
                    DESCRIPCION = table.Column<string>(type: "VARCHAR2(250)", maxLength: 250, nullable: false),
                    STOCK_CRITICO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ES_PROMOCION = table.Column<bool>(nullable: false),
                    DESCUENTO = table.Column<decimal>(type: "NUMBER(3,2)", nullable: false),
                    COMENTARIO = table.Column<string>(type: "VARCHAR2(250)", maxLength: 250, nullable: true),
                    ESTA_INACTIVO = table.Column<bool>(nullable: false),
                    FILE_ID = table.Column<string>(type: "NVARCHAR2(250)", nullable: true),
                    PROVEEDOR_ID = table.Column<int>(nullable: false),
                    TIPO_PRODUCTO_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTO", x => x.ID);
                    table.ForeignKey(
                        name: "PRODUCTO_PROVEEDOR_FK",
                        column: x => x.PROVEEDOR_ID,
                        principalTable: "PROVEEDOR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "PRODUCTO_TIPO_PRODUCTO_FK",
                        column: x => x.TIPO_PRODUCTO_ID,
                        principalTable: "TIPO_PRODUCTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONTACTO_PROVEEDOR_ID",
                table: "CONTACTO",
                column: "PROVEEDOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTO_PROVEEDOR_ID",
                table: "PRODUCTO",
                column: "PROVEEDOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTO_TIPO_PRODUCTO_ID",
                table: "PRODUCTO",
                column: "TIPO_PRODUCTO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PROVEEDOR_RUBRO_ID",
                table: "PROVEEDOR",
                column: "RUBRO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TIPO_PRODUCTO_FAMILIA_PRODUCTO_ID",
                table: "TIPO_PRODUCTO",
                column: "FAMILIA_PRODUCTO_ID");

            // Secuencias

            migrationBuilder.CreateSequence(
                name: "CONTACTO_ID_SEQ");

            migrationBuilder.CreateSequence(
                name: "PRODUCTO_ID_SEQ");

            // Disparadores

            migrationBuilder.Sql("CREATE OR REPLACE TRIGGER contacto_id_trg BEFORE INSERT ON contacto FOR EACH ROW BEGIN :new.id := contacto_id_seq.nextval; END;");
            migrationBuilder.Sql("CREATE OR REPLACE TRIGGER producto_id_trg BEFORE INSERT ON producto FOR EACH ROW BEGIN :new.id := producto_id_seq.nextval; END;");

            // Inserciones en tabla RUBRO

            migrationBuilder.Sql("insert into rubro values (1,'Agricultura, Ganadería, Silvicultura y Pesca');");
            migrationBuilder.Sql("insert into rubro values (2,'Explotación de Minas y Canteras');");
            migrationBuilder.Sql("insert into rubro values (3,'Industrias Manufacturera');");
            migrationBuilder.Sql("insert into rubro values (4,'Suministro de Electricidad, Gas, Vapor y Aire Acondicionado');");
            migrationBuilder.Sql("insert into rubro values (5,'Suministro de Agua; Evacuación de Agua residuales, gestión de desechos y descontaminación');");
            migrationBuilder.Sql("insert into rubro values (6,'Construcción');");
            migrationBuilder.Sql("insert into rubro values (7,'Comercio al Por Mayor y al por Menor; Reparación de Vehículos Automotores y Motocicletas');");
            migrationBuilder.Sql("insert into rubro values (8,'Transporte y Almacenamiento');");
            migrationBuilder.Sql("insert into rubro values (9,'Actividades de Alojamiento y de Servicio de Comidas');");
            migrationBuilder.Sql("insert into rubro values (10,'Información y Comunicaciones');");
            migrationBuilder.Sql("insert into rubro values (11,'Actividades Financieras y de Seguros');");
            migrationBuilder.Sql("insert into rubro values (12,'Actividades inmobiliarias');");
            migrationBuilder.Sql("insert into rubro values (13,'Actividades Profesionales, Cientificas y Técnicas');");
            migrationBuilder.Sql("insert into rubro values (14,'Actividades de Servicios Administrativos y de Apoyo');");
            migrationBuilder.Sql("insert into rubro values (15,'Adm. Pública y Defensa; Planes de Seguridad Social de Afiliación Obligatoria');");
            migrationBuilder.Sql("insert into rubro values (16,'Enseñanza');");
            migrationBuilder.Sql("insert into rubro values (17,'Actividades de Atención de la Salud Humana y de Asistencia Social');");
            migrationBuilder.Sql("insert into rubro values (18,'Actividades Artísticas, de Entretenimiento y Recreativas');");
            migrationBuilder.Sql("insert into rubro values (19,'Otras Actividades de Servicios');");
            migrationBuilder.Sql("insert into rubro values (20,'Actividades de los Hogares como Empleadores; Actividades No Diferenciadas de los Hogares');");
            migrationBuilder.Sql("insert into rubro values (21,'Actividades de Organizaciones y Órganos Extraterritoriales');");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONTACTO");

            migrationBuilder.DropTable(
                name: "PRODUCTO");

            migrationBuilder.DropTable(
                name: "PROVEEDOR");

            migrationBuilder.DropTable(
                name: "TIPO_PRODUCTO");

            migrationBuilder.DropTable(
                name: "RUBRO");

            migrationBuilder.DropTable(
                name: "FAMILIA_PRODUCTO");
        }
    }
}
