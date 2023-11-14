using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaSonrisa.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ASP_NET_ROLE",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    NAME = table.Column<string>(maxLength: 256, nullable: true),
                    NORMALIZED_NAME = table.Column<string>(maxLength: 256, nullable: true),
                    CONCURRENCY_STAMP = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASP_NET_ROLE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ASP_NET_USER",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    USER_NAME = table.Column<string>(maxLength: 256, nullable: true),
                    NORMALIZED_USER_NAME = table.Column<string>(maxLength: 256, nullable: true),
                    EMAIL = table.Column<string>(maxLength: 256, nullable: true),
                    NORMALIZED_EMAIL = table.Column<string>(maxLength: 256, nullable: true),
                    EMAIL_CONFIRMED = table.Column<bool>(nullable: false),
                    PASSWORD_HASH = table.Column<string>(nullable: true),
                    SECURITY_STAMP = table.Column<string>(nullable: true),
                    CONCURRENCY_STAMP = table.Column<string>(nullable: true),
                    PHONE_NUMBER = table.Column<string>(nullable: true),
                    PHONE_NUMBER_CONFIRMED = table.Column<bool>(nullable: false),
                    TWO_FACTOR_ENABLED = table.Column<bool>(nullable: false),
                    LOCKOUT_END = table.Column<DateTimeOffset>(nullable: true),
                    LOCKOUT_ENABLED = table.Column<bool>(nullable: false),
                    ACCESS_FAILED_COUNT = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASP_NET_USER", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ASP_NET_ROLE_CLAIM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    ROLE_ID = table.Column<string>(maxLength: 450, nullable: false),
                    CLAIM_TYPE = table.Column<string>(nullable: true),
                    CLAIM_VALUE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASP_NET_ROLE_CLAIM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ASP_NET_ROLE_CLAIM_ASP_NET_ROLE_ROLE_ID",
                        column: x => x.ROLE_ID,
                        principalTable: "ASP_NET_ROLE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ASP_NET_USER_CLAIM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    USER_ID = table.Column<string>(nullable: false),
                    CLAIM_TYPE = table.Column<string>(nullable: true),
                    CLAIM_VALUE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASP_NET_USER_CLAIM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ASP_NET_USER_CLAIM_ASP_NET_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "ASP_NET_USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ASP_NET_USER_LOGIN",
                columns: table => new
                {
                    LOGIN_PROVIDER = table.Column<string>(maxLength: 128, nullable: false),
                    PROVIDER_KEY = table.Column<string>(maxLength: 128, nullable: false),
                    PROVIDER_DISPLAY_NAME = table.Column<string>(nullable: true),
                    USER_ID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASP_NET_USER_LOGIN", x => new { x.LOGIN_PROVIDER, x.PROVIDER_KEY });
                    table.ForeignKey(
                        name: "FK_ASP_NET_USER_LOGIN_ASP_NET_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "ASP_NET_USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ASP_NET_USER_ROLE",
                columns: table => new
                {
                    USER_ID = table.Column<string>(nullable: false),
                    ROLE_ID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASP_NET_USER_ROLE", x => new { x.USER_ID, x.ROLE_ID });
                    table.ForeignKey(
                        name: "FK_ASP_NET_USER_ROLE_ASP_NET_ROLE_ROLE_ID",
                        column: x => x.ROLE_ID,
                        principalTable: "ASP_NET_ROLE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ASP_NET_USER_ROLE_ASP_NET_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "ASP_NET_USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ASP_NET_USER_TOKEN",
                columns: table => new
                {
                    USER_ID = table.Column<string>(nullable: false),
                    LOGIN_PROVIDER = table.Column<string>(maxLength: 128, nullable: false),
                    NAME = table.Column<string>(maxLength: 128, nullable: false),
                    VALUE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASP_NET_USER_TOKEN", x => new { x.USER_ID, x.LOGIN_PROVIDER, x.NAME });
                    table.ForeignKey(
                        name: "FK_ASP_NET_USER_TOKEN_ASP_NET_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "ASP_NET_USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ROLE_NAME_INDEX",
                table: "ASP_NET_ROLE",
                column: "NORMALIZED_NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_CLAIM_ROLE_ID",
                table: "ASP_NET_ROLE_CLAIM",
                column: "ROLE_ID");

            migrationBuilder.CreateIndex(
                name: "EMAIL_INDEX",
                table: "ASP_NET_USER",
                column: "NORMALIZED_EMAIL");

            migrationBuilder.CreateIndex(
                name: "USER_NAME_INDEX",
                table: "ASP_NET_USER",
                column: "NORMALIZED_USER_NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_CLAIMS_USER_ID",
                table: "ASP_NET_USER_CLAIM",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_LOGIN_USER_ID",
                table: "ASP_NET_USER_LOGIN",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLE_ROLE_ID",
                table: "ASP_NET_USER_ROLE",
                column: "ROLE_ID");

            // Inserción en la tabla ASP_NET_ROLE

            migrationBuilder.Sql("Insert into ASP_NET_ROLE(ID, NAME, NORMALIZED_NAME, CONCURRENCY_STAMP) values('1648f1f0-d362-4905-9cc5-77f1ebf3495a', 'Administrador', 'ADMINISTRADOR', '073ce4ef-b745-416a-bd0f-387d4208e1c7');");
            migrationBuilder.Sql("Insert into ASP_NET_ROLE(ID, NAME, NORMALIZED_NAME, CONCURRENCY_STAMP) values('85a107fd-8ca0-4dc9-9775-d603276e6723', 'Odontólogo', 'ODONTÓLOGO', '18d8ff2b-9e1e-4469-a854-2dcd5898b1bd');");
            migrationBuilder.Sql("Insert into ASP_NET_ROLE(ID, NAME, NORMALIZED_NAME, CONCURRENCY_STAMP) values('a11c626d-cb81-4a54-9255-7ec785bd66f1', 'Recepcionista', 'RECEPCIONISTA', 'f8a03e61-02ed-4fc6-a47f-29f7d7a76a41');");
        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASP_NET_ROLE_CLAIM");

            migrationBuilder.DropTable(
                name: "ASP_NET_USER_CLAIM");

            migrationBuilder.DropTable(
                name: "ASP_NET_USER_LOGIN");

            migrationBuilder.DropTable(
                name: "ASP_NET_USER_ROLE");

            migrationBuilder.DropTable(
                name: "ASP_NET_USER_TOKEN");

            migrationBuilder.DropTable(
                name: "ASP_NET_ROLE");

            migrationBuilder.DropTable(
                name: "ASP_NET_USER");
        }
    }
}
