using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaSonrisa.Migrations
{
    public partial class SecondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ESTADO_CIVIL",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    TITULO = table.Column<string>(type: "VARCHAR2(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTADO_CIVIL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GENERO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    TITULO = table.Column<string>(type: "VARCHAR2(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GENERO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "REGION",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    NOMBRE = table.Column<string>(type: "VARCHAR2(45)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGION", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "COMUNA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    NOMBRE = table.Column<string>(type: "VARCHAR2(45)", nullable: false),
                    REGION_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMUNA", x => x.ID);
                    table.ForeignKey(
                        name: "COMUNA_REGION_FK",
                        column: x => x.REGION_ID,
                        principalTable: "REGION",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    NOMBRE = table.Column<string>(type: "VARCHAR2(50)", maxLength: 50, nullable: false),
                    AP_PATERNO = table.Column<string>(type: "VARCHAR2(50)", maxLength: 50, nullable: false),
                    AP_MATERNO = table.Column<string>(type: "VARCHAR2(50)", maxLength: 50, nullable: false),
                    FECHA_NACIMIENTO = table.Column<DateTime>(type: "DATE", nullable: false),
                    FONO_FIJO = table.Column<string>(type: "VARCHAR2(8)", nullable: true),
                    FONO_MOVIL = table.Column<string>(type: "VARCHAR2(8)", nullable: true),
                    DIRECCION = table.Column<string>(type: "VARCHAR2(50)", maxLength: 50, nullable: false),
                    ES_EXTRANJERO = table.Column<bool>(nullable: false),
                    CREADO_EL = table.Column<DateTime>(nullable: true),
                    ACTUALIZADO_EL = table.Column<DateTime>(nullable: true),
                    COMUNA_ID = table.Column<int>(nullable: false),
                    GENERO_ID = table.Column<int>(nullable: false),
                    ESTADO_CIVIL_ID = table.Column<int>(nullable: true),
                    ASP_NET_USER_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                    table.ForeignKey(
                        name: "USUARIO_COMUNA_FK",
                        column: x => x.COMUNA_ID,
                        principalTable: "COMUNA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "USUARIO_ESTADO_CIVIL_FK",
                        column: x => x.ESTADO_CIVIL_ID,
                        principalTable: "ESTADO_CIVIL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "USUARIO_GENERO_FK",
                        column: x => x.GENERO_ID,
                        principalTable: "GENERO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "USUARIO_ASP_NET_USERS_FK",
                        column: x => x.ASP_NET_USER_ID,
                        principalTable: "ASP_NET_USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_COMUNA_REGION_ID",
                table: "COMUNA",
                column: "REGION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_COMUNA_ID",
                table: "USUARIO",
                column: "COMUNA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_ESTADO_CIVIL_ID",
                table: "USUARIO",
                column: "ESTADO_CIVIL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_GENERO_ID",
                table: "USUARIO",
                column: "GENERO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_ASP_NET_USER_ID",
                table: "USUARIO",
                column: "ASP_NET_USER_ID",
                unique: true);

            // Secuencias

            migrationBuilder.CreateSequence(
                name: "USUARIO_ID_SEQ");

            // Disparadores

            migrationBuilder.Sql("CREATE OR REPLACE TRIGGER usuario_id_trg BEFORE INSERT ON usuario FOR EACH ROW BEGIN :new.id := usuario_id_seq.nextval; END;");

            // Inserción en la tabla GENERO según ISO/IEC 5218

            migrationBuilder.Sql("insert into genero values (1,'Masculino');");
            migrationBuilder.Sql("insert into genero values (2,'Femenino');");
            migrationBuilder.Sql("insert into genero values (9,'No aplicable');");

            // Inserción en la tabla ESTADO_CIVIL

            migrationBuilder.Sql("insert into estado_civil values (1,'Casado(a)');");
            migrationBuilder.Sql("insert into estado_civil values (2,'Separado(a) judicialmente');");
            migrationBuilder.Sql("insert into estado_civil values (3,'Divorciado(a)');");
            migrationBuilder.Sql("insert into estado_civil values (4,'Viudo(a)');");
            migrationBuilder.Sql("insert into estado_civil values (5,'Soltero(a)');");
            migrationBuilder.Sql("insert into estado_civil values (6,'Conviviente Civil');");

            // Inserción en la tabla REGION (Chile)

            migrationBuilder.Sql("insert into region (id,nombre) values (1,'Arica y Parinacota');");
            migrationBuilder.Sql("insert into region (id,nombre) values (2,'Tarapacá');");
            migrationBuilder.Sql("insert into region (id,nombre) values (3,'Antofagasta');");
            migrationBuilder.Sql("insert into region (id,nombre) values (4,'Atacama');");
            migrationBuilder.Sql("insert into region (id,nombre) values (5,'Coquimbo');");
            migrationBuilder.Sql("insert into region (id,nombre) values (6,'Valparaiso');");
            migrationBuilder.Sql("insert into region (id,nombre) values (7,'Metropolitana de Santiago');");
            migrationBuilder.Sql("insert into region (id,nombre) values (8,'Libertador General Bernardo O’Higgins');");
            migrationBuilder.Sql("insert into region (id,nombre) values (9,'Maule');");
            migrationBuilder.Sql("insert into region (id,nombre) values (10,'Ñuble');");
            migrationBuilder.Sql("insert into region (id,nombre) values (11,'Biobío');");
            migrationBuilder.Sql("insert into region (id,nombre) values (12,'La Araucanía');");
            migrationBuilder.Sql("insert into region (id,nombre) values (13,'Los Ríos');");
            migrationBuilder.Sql("insert into region (id,nombre) values (14,'Los Lagos');");
            migrationBuilder.Sql("insert into region (id,nombre) values (15,'Aysén del General Carlos Ibáñez del Campo');");
            migrationBuilder.Sql("insert into region (id,nombre) values (16,'Magallanes y de la Antártica Chilena');");

            // Inserción en la tabla COMUNA (Chile)

            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values (1,'Arica',1);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values (2,'Camarones',1);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values (3,'General Lagos',1);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values (4,'Putre',1);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values (5,'Alto Hospicio',2);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(6,'Iquique',2);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(7,'Camiña',2);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(8,'Colchane',2);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(9,'Huara',2);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(10,'Pica',2);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(11,'Pozo Almonte',2);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(12,'Antofagasta',3);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(13,'Mejillones',3);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(14,'Sierra Gorda',3);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(15,'Taltal',3);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(16,'Calama',3);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(17,'Ollague',3);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(18,'San Pedro de Atacama',3);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(19,'María Elena',3);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(20,'Tocopilla',3);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(21,'Chañaral',4);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(22,'Diego de Almagro',4);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(23,'Caldera',4);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(24,'Copiapó',4);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(25,'Tierra Amarilla',4);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(26,'Alto del Carmen',4);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(27,'Freirina',4);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(28,'Huasco',4);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(29,'Vallenar',4);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(30,'Canela',5);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(31,'Illapel',5);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(32,'Los Vilos',5);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(33,'Salamanca',5);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(34,'Andacollo',5);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(35,'Coquimbo',5);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(36,'La Higuera',5);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(37,'La Serena',5);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(38,'Paihuaco',5);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(39,'Vicuña',5);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(40,'Combarbalá',5);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(41,'Monte Patria',5);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(42,'Ovalle',5);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(43,'Punitaqui',5);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(44,'Río Hurtado',5);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(45,'Isla de Pascua',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(46,'Calle Larga',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(47,'Los Andes',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(48,'Rinconada',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(49,'San Esteban',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(50,'La Ligua',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(51,'Papudo',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(52,'Petorca',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(53,'Zapallar',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(54,'Hijuelas',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(55,'La Calera',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(56,'La Cruz',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(57,'Limache',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(58,'Nogales',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(59,'Olmué',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(60,'Quillota',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(61,'Algarrobo',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(62,'Cartagena',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(63,'El Quisco',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(64,'El Tabo',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(65,'San Antonio',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(66,'Santo Domingo',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(67,'Catemu',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(68,'Llaillay',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(69,'Panquehue',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(70,'Putaendo',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(71,'San Felipe',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(72,'Santa María',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(73,'Casablanca',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(74,'Concón',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(75,'Juan Fernández',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(76,'Puchuncaví',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(77,'Quilpué',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(78,'Quintero',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(79,'Valparaíso',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(80,'Villa Alemana',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(81,'Viña del Mar',6);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(82,'Colina',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(83,'Lampa',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(84,'Tiltil',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(85,'Pirque',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(86,'Puente Alto',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(87,'San José de Maipo',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(88,'Buin',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(89,'Calera de Tango',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(90,'Paine',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(91,'San Bernardo',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(92,'Alhué',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(93,'Curacaví',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(94,'María Pinto',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(95,'Melipilla',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(96,'San Pedro',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(97,'Cerrillos',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(98,'Cerro Navia',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(99,'Conchalí',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(100,'El Bosque',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(101,'Estación Central',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(102,'Huechuraba',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(103,'Independencia',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(104,'La Cisterna',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(105,'La Granja',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(106,'La Florida',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(107,'La Pintana',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(108,'La Reina',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(109,'Las Condes',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(110,'Lo Barnechea',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(111,'Lo Espejo',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(112,'Lo Prado',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(113,'Macul',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(114,'Maipú',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(115,'Ñuñoa',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(116,'Pedro Aguirre Cerda',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(117,'Peñalolén',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(118,'Providencia',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(119,'Pudahuel',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(120,'Quilicura',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(121,'Quinta Normal',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(122,'Recoleta',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(123,'Renca',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(124,'San Miguel',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(125,'San Joaquín',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(126,'San Ramón',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(127,'Santiago',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(128,'Vitacura',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(129,'El Monte',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(130,'Isla de Maipo',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(131,'Padre Hurtado',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(132,'Peñaflor',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(133,'Talagante',7);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(134,'Codegua',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(135,'Coínco',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(136,'Coltauco',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(137,'Doñihue',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(138,'Graneros',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(139,'Las Cabras',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(140,'Machalí',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(141,'Malloa',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(142,'Mostazal',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(143,'Olivar',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(144,'Peumo',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(145,'Pichidegua',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(146,'Quinta de Tilcoco',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(147,'Rancagua',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(148,'Rengo',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(149,'Requínoa',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(150,'San Vicente de Tagua Tagua',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(151,'La Estrella',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(152,'Litueche',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(153,'Marchihue',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(154,'Navidad',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(155,'Peredones',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(156,'Pichilemu',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(157,'Chépica',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(158,'Chimbarongo',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(159,'Lolol',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(160,'Nancagua',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(161,'Palmilla',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(162,'Peralillo',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(163,'Placilla',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(164,'Pumanque',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(165,'San Fernando',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(166,'Santa Cruz',8);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(167,'Cauquenes',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(168,'Chanco',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(169,'Pelluhue',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(170,'Curicó',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(171,'Hualañé',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(172,'Licantén',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(173,'Molina',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(174,'Rauco',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(175,'Romeral',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(176,'Sagrada Familia',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(177,'Teno',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(178,'Vichuquén',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(179,'Colbún',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(180,'Linares',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values (181,'Longaví',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(182,'Parral',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(183,'Retiro',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(184,'San Javier',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(185,'Villa Alegre',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(186,'Yerbas Buenas',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(187,'Constitución',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(188,'Curepto',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(189,'Empedrado',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(190,'Maule',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(191,'Pelarco',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(192,'Pencahue',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(193,'Río Claro',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(194,'San Clemente',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(195,'San Rafael',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(196,'Talca',9);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(197,'Bulnes',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(198,'Chillán',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(199,'Chillán Viejo',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(200,'Cobquecura',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(201,'Coelemu',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(202,'Coihueco',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(203,'El Carmen',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(204,'Ninhue',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(205,'Ñiquen',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(206,'Pemuco',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(207,'Pinto',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(208,'Portezuelo',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(209,'Quirihue',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(210,'Ránquil',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(211,'Treguaco',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(212,'Quillón',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(213,'San Carlos',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(214,'San Fabián',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(215,'San Ignacio',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(216,'San Nicolás',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(217,'Yungay',10);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(218,'Arauco',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(219,'Cañete',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(220,'Contulmo',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(221,'Curanilahue',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(222,'Lebu',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(223,'Los Álamos',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(224,'Tirúa',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(225,'Alto Biobío',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(226,'Antuco',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(227,'Cabrero',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(228,'Laja',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(229,'Los Ángeles',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(230,'Mulchén',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(231,'Nacimiento',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(232,'Negrete',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(233,'Quilaco',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(234,'Quilleco',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(235,'San Rosendo',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(236,'Santa Bárbara',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(237,'Tucapel',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(238,'Yumbel',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(239,'Chiguayante',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(240,'Concepción',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(241,'Coronel',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(242,'Florida',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(243,'Hualpén',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(244,'Hualqui',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(245,'Lota',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(246,'Penco',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(247,'San Pedro de La Paz',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(248,'Santa Juana',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(249,'Talcahuano',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(250,'Tomé',11);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(251,'Carahue',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(252,'Cholchol',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(253,'Cunco',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(254,'Curarrehue',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(255,'Freire',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(256,'Galvarino',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(257,'Gorbea',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(258,'Lautaro',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(259,'Loncoche',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(260,'Melipeuco',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(261,'Nueva Imperial',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(262,'Padre Las Casas',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(263,'Perquenco',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(264,'Pitrufquén',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(265,'Pucón',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(266,'Saavedra',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(267,'Temuco',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(268,'Teodoro Schmidt',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(269,'Toltén',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(270,'Vilcún',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(271,'Villarrica',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(272,'Angol',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(273,'Collipulli',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(274,'Curacautín',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(275,'Ercilla',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(276,'Lonquimay',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(277,'Los Sauces',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(278,'Lumaco',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(279,'Purén',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(280,'Renaico',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(281,'Traiguén',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(282,'Victoria',12);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(283,'Corral',13);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(284,'Lanco',13);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(285,'Los Lagos',13);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(286,'Máfil',13);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(287,'Mariquina',13);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(288,'Paillaco',13);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(289,'Panguipulli',13);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(290,'Valdivia',13);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(291,'Futrono',13);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(292,'La Unión',13);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(293,'Lago Ranco',13);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(294,'Río Bueno',13);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(295,'Ancud',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(296,'Castro',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(297,'Chonchi',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(298,'Curaco de Vélez',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(299,'Dalcahue',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(300,'Puqueldón',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(301,'Queilén',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(302,'Quemchi',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(303,'Quellón',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(304,'Quinchao',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(305,'Calbuco',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(306,'Cochamó',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(307,'Fresia',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(308,'Frutillar',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(309,'Llanquihue',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(310,'Los Muermos',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(311,'Maullín',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(312,'Puerto Montt',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(313,'Puerto Varas',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(314,'Osorno',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(315,'Puero Octay',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(316,'Purranque',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(317,'Puyehue',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(318,'Río Negro',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(319,'San Juan de la Costa',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(320,'San Pablo',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(321,'Chaitén',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(322,'Futaleufú',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(323,'Hualaihué',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(324,'Palena',14);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(325,'Aisén',15);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(326,'Cisnes',15);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(327,'Guaitecas',15);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(328,'Cochrane',15);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(329,'O’higgins',15);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(330,'Tortel',15);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(331,'Coihaique',15);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(332,'Lago Verde',15);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(333,'Chile Chico',15);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(334,'Río Ibáñez',15);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(335,'Antártica',16);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(336,'Cabo de Hornos',16);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(337,'Laguna Blanca',16);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(338,'Punta Arenas',16);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(339,'Río Verde',16);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(340,'San Gregorio',16);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(341,'Porvenir',16);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(342,'Primavera',16);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(343,'Timaukel',16);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(344,'Natales',16);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(345,'Torres del Paine',16);");
            migrationBuilder.Sql("insert into comuna (id, nombre, region_id) values	(346,'Cabildo',6);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "COMUNA");

            migrationBuilder.DropTable(
                name: "ESTADO_CIVIL");

            migrationBuilder.DropTable(
                name: "GENERO");

            migrationBuilder.DropTable(
                name: "REGION");

            // Disparadores

            migrationBuilder.Sql("DROP TRIGGER usuario_id_trg;");
        }
    }
}
