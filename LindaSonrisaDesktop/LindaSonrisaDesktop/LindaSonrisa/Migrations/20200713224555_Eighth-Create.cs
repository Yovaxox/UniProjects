using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LindaSonrisa.Migrations
{
    public partial class EighthCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ESTA_INACTIVO",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "FILE_ID",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "ESTA_INACTIVO",
                table: "MODULO");

            migrationBuilder.RenameColumn(
                name: "NOMBRE_NORMALIZADO",
                table: "SERVICIO",
                newName: "TITULO_NORMALIZADO");

            migrationBuilder.RenameColumn(
                name: "NOMBRE",
                table: "SERVICIO",
                newName: "TITULO");

            migrationBuilder.RenameIndex(
                name: "NOMBRE_SERVICIO_INDEX",
                table: "SERVICIO",
                newName: "TITULO_SERVICIO_INDEX");

            migrationBuilder.RenameColumn(
                name: "FECHA",
                table: "RESERVA",
                newName: "FECHA_RESERVA");

            migrationBuilder.RenameColumn(
                name: "NOMBRE",
                table: "REGION",
                newName: "TITULO");

            migrationBuilder.RenameColumn(
                name: "NOMBRE_NORMALIZADO",
                table: "PRODUCTO",
                newName: "TITULO_NORMALIZADO");

            migrationBuilder.RenameColumn(
                name: "NOMBRE",
                table: "PRODUCTO",
                newName: "TITULO");

            migrationBuilder.RenameColumn(
                name: "NOMBRE",
                table: "DIA",
                newName: "TITULO");

            migrationBuilder.RenameColumn(
                name: "NOMBRE",
                table: "COMUNA",
                newName: "TITULO");

            migrationBuilder.AddColumn<string>(
                name: "RUTA_FOTO",
                table: "USUARIO",
                type: "NVARCHAR2(100)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "USER_ID",
                table: "USUARIO",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TITULO_NORMALIZADO",
                table: "TIPO_PRODUCTO",
                type: "VARCHAR2(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ESTA_INACTIVO",
                table: "SERVICIO",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "NUMBER(1)");

            migrationBuilder.AlterColumn<string>(
                name: "ES_PROMOCION",
                table: "SERVICIO",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "NUMBER(1)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SOLICITADO_EL",
                table: "RESERVA",
                type: "DATE",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "FUE_ANULADA",
                table: "RESERVA",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<DateTime>(
                name: "FECHA_ANULACION",
                table: "RESERVA",
                type: "TIMESTAMP(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FECHA_MODIFICACION",
                table: "RESERVA",
                type: "TIMESTAMP(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HORA",
                table: "RESERVA",
                type: "NVARCHAR2(20)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ES_PROMOCION",
                table: "PRODUCTO",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<long>(
                name: "BOX",
                table: "MODULO",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "DISPONIBLE",
                table: "MODULO",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TITULO_NORMALIZADO",
                table: "FAMILIA_PRODUCTO",
                type: "VARCHAR2(50)",
                nullable: false,
                defaultValue: "");


            // Secuencias

            migrationBuilder.CreateSequence(
                name: "RESERVA_ID_SEQ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RUTA_FOTO",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "USER_ID",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "TITULO_NORMALIZADO",
                table: "TIPO_PRODUCTO");

            migrationBuilder.DropColumn(
                name: "FECHA_ANULACION",
                table: "RESERVA");

            migrationBuilder.DropColumn(
                name: "FECHA_MODIFICACION",
                table: "RESERVA");

            migrationBuilder.DropColumn(
                name: "HORA",
                table: "RESERVA");

            migrationBuilder.DropColumn(
                name: "BOX",
                table: "MODULO");

            migrationBuilder.DropColumn(
                name: "DISPONIBLE",
                table: "MODULO");

            migrationBuilder.DropColumn(
                name: "TITULO_NORMALIZADO",
                table: "FAMILIA_PRODUCTO");

            migrationBuilder.RenameColumn(
                name: "TITULO_NORMALIZADO",
                table: "SERVICIO",
                newName: "NOMBRE_NORMALIZADO");

            migrationBuilder.RenameColumn(
                name: "TITULO",
                table: "SERVICIO",
                newName: "NOMBRE");

            migrationBuilder.RenameIndex(
                name: "TITULO_SERVICIO_INDEX",
                table: "SERVICIO",
                newName: "NOMBRE_SERVICIO_INDEX");

            migrationBuilder.RenameColumn(
                name: "FECHA_RESERVA",
                table: "RESERVA",
                newName: "FECHA");

            migrationBuilder.RenameColumn(
                name: "TITULO",
                table: "REGION",
                newName: "NOMBRE");

            migrationBuilder.RenameColumn(
                name: "TITULO_NORMALIZADO",
                table: "PRODUCTO",
                newName: "NOMBRE_NORMALIZADO");

            migrationBuilder.RenameColumn(
                name: "TITULO",
                table: "PRODUCTO",
                newName: "NOMBRE");

            migrationBuilder.RenameColumn(
                name: "TITULO",
                table: "DIA",
                newName: "NOMBRE");

            migrationBuilder.RenameColumn(
                name: "TITULO",
                table: "COMUNA",
                newName: "NOMBRE");

            migrationBuilder.AddColumn<bool>(
                name: "ESTA_INACTIVO",
                table: "USUARIO",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FILE_ID",
                table: "USUARIO",
                type: "NVARCHAR2(250)",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ESTA_INACTIVO",
                table: "SERVICIO",
                type: "NUMBER(1)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<bool>(
                name: "ES_PROMOCION",
                table: "SERVICIO",
                type: "NUMBER(1)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "SOLICITADO_EL",
                table: "RESERVA",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATE");

            migrationBuilder.AlterColumn<bool>(
                name: "FUE_ANULADA",
                table: "RESERVA",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<bool>(
                name: "ES_PROMOCION",
                table: "PRODUCTO",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<bool>(
                name: "ESTA_INACTIVO",
                table: "MODULO",
                nullable: false,
                defaultValue: false);
        }
    }
}
