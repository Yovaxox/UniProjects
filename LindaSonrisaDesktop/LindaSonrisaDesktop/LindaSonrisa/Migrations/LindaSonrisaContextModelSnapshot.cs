﻿// <auto-generated />
using System;
using LindaSonrisa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

namespace LindaSonrisa.Migrations
{
    [DbContext(typeof(LindaSonrisaContext))]
    partial class LindaSonrisaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:.FAMILIA_PRODUCTO_ID_SEQ", "'FAMILIA_PRODUCTO_ID_SEQ', '', '100', '1', '100', '999', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.ORDEN_ID_SEQ", "'ORDEN_ID_SEQ', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.PROVEEDOR_ID_SEQ", "'PROVEEDOR_ID_SEQ', '', '100', '1', '100', '999', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.TIPO_PRODUCTO_ID_SEQ", "'TIPO_PRODUCTO_ID_SEQ', '', '100', '1', '100', '999', 'Int32', 'False'");

            modelBuilder.Entity("LindaSonrisa.Models.Context.AuthUser", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("NUMBER(11)");

                    b.Property<DateTime>("DateJoined")
                        .HasColumnName("DATE_JOINED")
                        .HasColumnType("TIMESTAMP(6)");

                    b.Property<string>("Email")
                        .HasColumnName("EMAIL");

                    b.Property<string>("FirstName")
                        .HasColumnName("FIRST_NAME");

                    b.Property<bool>("IsActive")
                        .HasColumnName("IS_ACTIVE");

                    b.Property<bool>("IsStaff")
                        .HasColumnName("IS_STAFF");

                    b.Property<bool>("IsSuperUser")
                        .HasColumnName("IS_SUPERUSER");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnName("LAST_LOGIN")
                        .HasColumnType("TIMESTAMP(6)");

                    b.Property<string>("LastName")
                        .HasColumnName("LAST_NAME");

                    b.Property<string>("Password")
                        .HasColumnName("PASSWORD");

                    b.Property<string>("UserName")
                        .HasColumnName("USERNAME");

                    b.HasKey("Id");

                    b.ToTable("AUTH_USER");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Comuna", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<int>("RegionId")
                        .HasColumnName("REGION_ID");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("TITULO")
                        .HasColumnType("VARCHAR2(45)");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("COMUNA");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Contacto", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("ApMaterno")
                        .HasColumnName("AP_MATERNO")
                        .HasColumnType("VARCHAR2(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ApPaterno")
                        .HasColumnName("AP_PATERNO")
                        .HasColumnType("VARCHAR2(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .HasColumnName("EMAIL")
                        .HasColumnType("VARCHAR2(50)")
                        .HasMaxLength(50);

                    b.Property<string>("FonoMovil")
                        .IsRequired()
                        .HasColumnName("FONO_MOVIL")
                        .HasColumnType("VARCHAR2(8)")
                        .HasMaxLength(8);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnName("NOMBRE")
                        .HasColumnType("VARCHAR2(50)")
                        .HasMaxLength(50);

                    b.Property<int>("ProveedorId")
                        .HasColumnName("PROVEEDOR_ID");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

                    b.ToTable("CONTACTO");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.DetalleOrden", b =>
                {
                    b.Property<int>("OrdenId")
                        .HasColumnName("ORDEN_ID");

                    b.Property<int>("ProductoId")
                        .HasColumnName("PRODUCTO_ID");

                    b.Property<int>("Cantidad")
                        .HasColumnName("CANTIDAD");

                    b.HasKey("OrdenId", "ProductoId");

                    b.HasIndex("ProductoId");

                    b.ToTable("DETALLE_ORDEN");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Dia", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("TITULO")
                        .HasColumnType("VARCHAR2(25)");

                    b.HasKey("Id");

                    b.ToTable("DIA");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.EstadoCivil", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("TITULO")
                        .HasColumnType("VARCHAR2(25)");

                    b.HasKey("Id");

                    b.ToTable("ESTADO_CIVIL");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.FamiliaProducto", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("TITULO")
                        .HasColumnType("VARCHAR2(50)");

                    b.Property<string>("TituloNormalizado")
                        .IsRequired()
                        .HasColumnName("TITULO_NORMALIZADO")
                        .HasColumnType("VARCHAR2(50)");

                    b.HasKey("Id");

                    b.ToTable("FAMILIA_PRODUCTO");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Genero", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("TITULO")
                        .HasColumnType("VARCHAR2(25)");

                    b.HasKey("Id");

                    b.ToTable("GENERO");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Modulo", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<long>("Box")
                        .HasColumnName("BOX");

                    b.Property<int>("DiaId")
                        .HasColumnName("DIA_ID");

                    b.Property<string>("Disponible")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)))
                        .HasColumnName("DISPONIBLE");

                    b.Property<string>("HoraInicio")
                        .IsRequired()
                        .HasColumnName("HORA_INICIO")
                        .HasColumnType("VARCHAR2(5)");

                    b.Property<string>("HoraTermino")
                        .IsRequired()
                        .HasColumnName("HORA_TERMINO")
                        .HasColumnType("VARCHAR2(5)");

                    b.Property<int>("ServicioId")
                        .HasColumnName("SERVICIO_ID");

                    b.Property<int>("UsuarioId")
                        .HasColumnName("USUARIO_ID");

                    b.HasKey("Id");

                    b.HasIndex("DiaId");

                    b.HasIndex("ServicioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("MODULO");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Orden", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<DateTime?>("ActualizadoEl")
                        .HasColumnName("ACTUALIZADO_EL");

                    b.Property<bool>("FueRecepcionada")
                        .HasColumnName("FUE_RECEPCIONADO");

                    b.Property<DateTime>("SolicitadoEl")
                        .HasColumnName("SOLICITADO_EL");

                    b.HasKey("Id");

                    b.ToTable("ORDEN");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Producto", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("Comentario")
                        .HasColumnName("COMENTARIO")
                        .HasColumnType("VARCHAR2(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnName("DESCRIPCION")
                        .HasColumnType("VARCHAR2(250)")
                        .HasMaxLength(250);

                    b.Property<decimal>("Descuento")
                        .HasColumnName("DESCUENTO")
                        .HasColumnType("NUMBER(3,2)");

                    b.Property<string>("EsPromocion")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)))
                        .HasColumnName("ES_PROMOCION");

                    b.Property<bool>("EstaInactivo")
                        .HasColumnName("ESTA_INACTIVO");

                    b.Property<string>("FileId")
                        .HasColumnName("FILE_ID")
                        .HasColumnType("NVARCHAR2(250)");

                    b.Property<int>("ProveedorId")
                        .HasColumnName("PROVEEDOR_ID");

                    b.Property<int>("StockCritico")
                        .HasColumnName("STOCK_CRITICO")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("TipoProductoId")
                        .HasColumnName("TIPO_PRODUCTO_ID");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("TITULO")
                        .HasColumnType("VARCHAR2(50)")
                        .HasMaxLength(50);

                    b.Property<string>("TituloNormalizado")
                        .IsRequired()
                        .HasColumnName("TITULO_NORMALIZADO")
                        .HasColumnType("VARCHAR2(50)");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

                    b.HasIndex("TipoProductoId");

                    b.ToTable("PRODUCTO");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Proveedor", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("Comentario")
                        .HasColumnName("COMENTARIO")
                        .HasColumnType("VARCHAR2(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("EMAIL")
                        .HasColumnType("VARCHAR2(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("EstaInactivo")
                        .HasColumnName("ESTA_INACTIVO");

                    b.Property<string>("FonoFijo")
                        .IsRequired()
                        .HasColumnName("FONO_FIJO")
                        .HasColumnType("VARCHAR2(8)");

                    b.Property<string>("FonoMovil")
                        .HasColumnName("FONO_MOVIL")
                        .HasColumnType("VARCHAR2(8)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnName("NOMBRE")
                        .HasColumnType("VARCHAR2(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NombreNormalizado")
                        .IsRequired()
                        .HasColumnName("NOMBRE_NORMALIZADO")
                        .HasColumnType("VARCHAR2(50)");

                    b.Property<int>("RubroId")
                        .HasColumnName("RUBRO_ID");

                    b.Property<string>("Url")
                        .HasColumnName("URL")
                        .HasColumnType("VARCHAR2(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("RubroId");

                    b.ToTable("PROVEEDOR");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.RecepcionOrden", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<int>("Cantidad")
                        .HasColumnName("CANTIDAD");

                    b.Property<string>("Codificacion")
                        .HasColumnName("CODIFICACION")
                        .HasColumnType("VARCHAR2(25)");

                    b.Property<int>("OrdenId")
                        .HasColumnName("ORDEN_ID");

                    b.Property<string>("Producto")
                        .HasColumnName("PRODUCTO")
                        .HasColumnType("VARCHAR2(50)");

                    b.Property<int>("ProductoId")
                        .HasColumnName("PRODUCTO_ID");

                    b.Property<DateTime>("RecepcionadoEl")
                        .HasColumnName("RECEPCIONADO_EL")
                        .HasColumnType("TIMESTAMP(6)");

                    b.HasKey("Id");

                    b.HasIndex("OrdenId");

                    b.ToTable("RECEPCION_ORDEN");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Region", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("TITULO")
                        .HasColumnType("VARCHAR2(45)");

                    b.HasKey("Id");

                    b.ToTable("REGION");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<DateTime?>("FechaAnulacion")
                        .HasColumnName("FECHA_ANULACION")
                        .HasColumnType("TIMESTAMP(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnName("FECHA_MODIFICACION")
                        .HasColumnType("TIMESTAMP(6)");

                    b.Property<DateTime>("FechaReserva")
                        .HasColumnName("FECHA_RESERVA")
                        .HasColumnType("DATE");

                    b.Property<string>("FueAnulada")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)))
                        .HasColumnName("FUE_ANULADA");

                    b.Property<string>("Hora")
                        .HasColumnName("HORA")
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<int>("ModuloId")
                        .HasColumnName("MODULO_ID");

                    b.Property<DateTime>("SolicitadoEl")
                        .HasColumnName("SOLICITADO_EL")
                        .HasColumnType("DATE");

                    b.Property<int>("UsuarioId")
                        .HasColumnName("USUARIO_ID");

                    b.HasKey("Id");

                    b.HasIndex("ModuloId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("RESERVA");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Rubro", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("TITULO")
                        .HasColumnType("VARCHAR2(100)");

                    b.HasKey("Id");

                    b.ToTable("RUBRO");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Servicio", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("Comentario")
                        .HasColumnName("COMENTARIO")
                        .HasColumnType("VARCHAR2(250)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnName("DESCRIPCION")
                        .HasColumnType("VARCHAR2(250)")
                        .HasMaxLength(250);

                    b.Property<decimal>("Descuento")
                        .HasColumnName("DESCUENTO")
                        .HasColumnType("NUMBER(3,2)");

                    b.Property<string>("EsPromocion")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)))
                        .HasColumnName("ES_PROMOCION");

                    b.Property<string>("EstaInactivo")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)))
                        .HasColumnName("ESTA_INACTIVO");

                    b.Property<string>("FileId")
                        .HasColumnName("FILE_ID")
                        .HasColumnType("NVARCHAR2(250)");

                    b.Property<int>("Precio")
                        .HasColumnName("PRECIO")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("TITULO")
                        .HasColumnType("VARCHAR2(50)")
                        .HasMaxLength(50);

                    b.Property<string>("TituloNormalizado")
                        .IsRequired()
                        .HasColumnName("TITULO_NORMALIZADO")
                        .HasColumnType("VARCHAR2(50)");

                    b.HasKey("Id");

                    b.HasIndex("Titulo")
                        .IsUnique()
                        .HasName("TITULO_SERVICIO_INDEX");

                    b.ToTable("SERVICIO");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.TipoProducto", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<int>("FamiliaProductoId")
                        .HasColumnName("FAMILIA_PRODUCTO_ID");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("TITULO")
                        .HasColumnType("VARCHAR2(50)");

                    b.Property<string>("TituloNormalizado")
                        .IsRequired()
                        .HasColumnName("TITULO_NORMALIZADO")
                        .HasColumnType("VARCHAR2(50)");

                    b.HasKey("Id");

                    b.HasIndex("FamiliaProductoId");

                    b.ToTable("TIPO_PRODUCTO");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<DateTime?>("ActualizadoEl")
                        .HasColumnName("ACTUALIZADO_EL");

                    b.Property<string>("ApMaterno")
                        .IsRequired()
                        .HasColumnName("AP_MATERNO")
                        .HasColumnType("VARCHAR2(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ApPaterno")
                        .IsRequired()
                        .HasColumnName("AP_PATERNO")
                        .HasColumnType("VARCHAR2(50)")
                        .HasMaxLength(50);

                    b.Property<long?>("AuthUserId")
                        .HasColumnName("USER_ID");

                    b.Property<int>("ComunaId")
                        .HasColumnName("COMUNA_ID");

                    b.Property<DateTime?>("CreadoEl")
                        .HasColumnName("CREADO_EL");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnName("DIRECCION")
                        .HasColumnType("VARCHAR2(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("EsExtranjero")
                        .HasColumnName("ES_EXTRANJERO");

                    b.Property<int?>("EstadoCivilId")
                        .HasColumnName("ESTADO_CIVIL_ID");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnName("FECHA_NACIMIENTO")
                        .HasColumnType("DATE");

                    b.Property<string>("FonoFijo")
                        .HasColumnName("FONO_FIJO")
                        .HasColumnType("VARCHAR2(8)");

                    b.Property<string>("FonoMovil")
                        .HasColumnName("FONO_MOVIL")
                        .HasColumnType("VARCHAR2(8)");

                    b.Property<int>("GeneroId")
                        .HasColumnName("GENERO_ID");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnName("NOMBRE")
                        .HasColumnType("VARCHAR2(50)")
                        .HasMaxLength(50);

                    b.Property<string>("RutaFoto")
                        .HasColumnName("RUTA_FOTO")
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("UserId")
                        .HasColumnName("ASP_NET_USER_ID");

                    b.HasKey("Id");

                    b.HasIndex("AuthUserId")
                        .IsUnique();

                    b.HasIndex("ComunaId");

                    b.HasIndex("EstadoCivilId");

                    b.HasIndex("GeneroId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("USUARIO");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Identity.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("ID");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnName("ACCESS_FAILED_COUNT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("CONCURRENCY_STAMP");

                    b.Property<string>("Email")
                        .HasColumnName("EMAIL")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnName("EMAIL_CONFIRMED");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnName("LOCKOUT_ENABLED");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnName("LOCKOUT_END");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnName("NORMALIZED_EMAIL")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnName("NORMALIZED_USER_NAME")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnName("PASSWORD_HASH");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("PHONE_NUMBER");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnName("PHONE_NUMBER_CONFIRMED");

                    b.Property<string>("SecurityStamp")
                        .HasColumnName("SECURITY_STAMP");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnName("TWO_FACTOR_ENABLED");

                    b.Property<string>("UserName")
                        .HasColumnName("USER_NAME")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EMAIL_INDEX");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("USER_NAME_INDEX");

                    b.ToTable("ASP_NET_USER");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("CONCURRENCY_STAMP");

                    b.Property<string>("Name")
                        .HasColumnName("NAME")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnName("NORMALIZED_NAME")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("ROLE_NAME_INDEX");

                    b.ToTable("ASP_NET_ROLE");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("ClaimType")
                        .HasColumnName("CLAIM_TYPE");

                    b.Property<string>("ClaimValue")
                        .HasColumnName("CLAIM_VALUE");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnName("ROLE_ID")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("RoleId")
                        .HasName("IX_ROLE_CLAIM_ROLE_ID");

                    b.ToTable("ASP_NET_ROLE_CLAIM");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("ClaimType")
                        .HasColumnName("CLAIM_TYPE");

                    b.Property<string>("ClaimValue")
                        .HasColumnName("CLAIM_VALUE");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("USER_ID");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasName("IX_USER_CLAIMS_USER_ID");

                    b.ToTable("ASP_NET_USER_CLAIM");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnName("LOGIN_PROVIDER")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnName("PROVIDER_KEY")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnName("PROVIDER_DISPLAY_NAME");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("USER_ID");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId")
                        .HasName("IX_USER_LOGIN_USER_ID");

                    b.ToTable("ASP_NET_USER_LOGIN");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnName("USER_ID");

                    b.Property<string>("RoleId")
                        .HasColumnName("ROLE_ID");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId")
                        .HasName("IX_USER_ROLE_ROLE_ID");

                    b.ToTable("ASP_NET_USER_ROLE");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnName("USER_ID");

                    b.Property<string>("LoginProvider")
                        .HasColumnName("LOGIN_PROVIDER")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnName("NAME")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnName("VALUE");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("ASP_NET_USER_TOKEN");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Comuna", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Context.Region", "Region")
                        .WithMany("Comuna")
                        .HasForeignKey("RegionId")
                        .HasConstraintName("COMUNA_REGION_FK");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Contacto", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Context.Proveedor", "Proveedor")
                        .WithMany("Contacto")
                        .HasForeignKey("ProveedorId")
                        .HasConstraintName("CONTACTO_PROVEEDOR_FK");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.DetalleOrden", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Context.Orden", "Orden")
                        .WithMany("DetalleOrden")
                        .HasForeignKey("OrdenId")
                        .HasConstraintName("ORDEN_DETALLE_ORDEN_FK");

                    b.HasOne("LindaSonrisa.Models.Context.Producto", "Producto")
                        .WithMany("DetalleOrden")
                        .HasForeignKey("ProductoId")
                        .HasConstraintName("PRODUCTO_DETALLE_ORDEN_FK");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Modulo", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Context.Dia", "Dia")
                        .WithMany("Modulo")
                        .HasForeignKey("DiaId")
                        .HasConstraintName("MODULO_DIA_FK");

                    b.HasOne("LindaSonrisa.Models.Context.Servicio", "Servicio")
                        .WithMany("Modulo")
                        .HasForeignKey("ServicioId")
                        .HasConstraintName("MODULO_SERVICIO_FK");

                    b.HasOne("LindaSonrisa.Models.Context.Usuario", "Usuario")
                        .WithMany("Modulo")
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("MODULO_USUARIO_FK");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Producto", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Context.Proveedor", "Proveedor")
                        .WithMany("Producto")
                        .HasForeignKey("ProveedorId")
                        .HasConstraintName("PRODUCTO_PROVEEDOR_FK");

                    b.HasOne("LindaSonrisa.Models.Context.TipoProducto", "TipoProducto")
                        .WithMany("Producto")
                        .HasForeignKey("TipoProductoId")
                        .HasConstraintName("PRODUCTO_TIPO_PRODUCTO_FK");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Proveedor", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Context.Rubro", "Rubro")
                        .WithMany("Proveedor")
                        .HasForeignKey("RubroId")
                        .HasConstraintName("PROVEEDOR_RUBRO_FK");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.RecepcionOrden", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Context.Orden", "Orden")
                        .WithMany("RecepcionOrden")
                        .HasForeignKey("OrdenId")
                        .HasConstraintName("RECEPCION_ORDEN_FK");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Reserva", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Context.Modulo", "Modulo")
                        .WithMany("Reserva")
                        .HasForeignKey("ModuloId")
                        .HasConstraintName("RESERVA_MODULO_FK");

                    b.HasOne("LindaSonrisa.Models.Context.Usuario", "Usuario")
                        .WithMany("Reserva")
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("RESERVA_USUARIO_FK");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.TipoProducto", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Context.FamiliaProducto", "FamiliaProducto")
                        .WithMany("TipoProducto")
                        .HasForeignKey("FamiliaProductoId")
                        .HasConstraintName("TIPO_FAMILIA_PRODUCTO_FK");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Context.Usuario", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Context.AuthUser", "AuthUser")
                        .WithOne("Usuario")
                        .HasForeignKey("LindaSonrisa.Models.Context.Usuario", "AuthUserId")
                        .HasConstraintName("USUARIO_AUTH_USER_FK");

                    b.HasOne("LindaSonrisa.Models.Context.Comuna", "Comuna")
                        .WithMany("Usuario")
                        .HasForeignKey("ComunaId")
                        .HasConstraintName("USUARIO_COMUNA_FK");

                    b.HasOne("LindaSonrisa.Models.Context.EstadoCivil", "EstadoCivil")
                        .WithMany("Usuario")
                        .HasForeignKey("EstadoCivilId")
                        .HasConstraintName("USUARIO_ESTADO_CIVIL_FK");

                    b.HasOne("LindaSonrisa.Models.Context.Genero", "Genero")
                        .WithMany("Usuario")
                        .HasForeignKey("GeneroId")
                        .HasConstraintName("USUARIO_GENERO_FK");

                    b.HasOne("LindaSonrisa.Models.Identity.User", "User")
                        .WithOne("Usuario")
                        .HasForeignKey("LindaSonrisa.Models.Context.Usuario", "UserId")
                        .HasConstraintName("USUARIO_ASP_NET_USER_FK");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Identity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Identity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LindaSonrisa.Models.Identity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Identity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
