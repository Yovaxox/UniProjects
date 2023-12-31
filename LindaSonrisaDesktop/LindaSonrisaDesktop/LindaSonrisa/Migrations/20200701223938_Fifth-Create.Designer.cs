﻿// <auto-generated />
using System;
using LindaSonrisa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

namespace LindaSonrisa.Migrations
{
    [DbContext(typeof(LindaSonrisaContext))]
    [Migration("20200701223938_Fifth-Create")]
    partial class FifthCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            modelBuilder.Entity("LindaSonrisa.Models.Comuna", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnName("NOMBRE")
                        .HasColumnType("VARCHAR2(45)");

                    b.Property<int>("RegionId")
                        .HasColumnName("REGION_ID");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("COMUNA");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Dia", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnName("NOMBRE")
                        .HasColumnType("VARCHAR2(25)");

                    b.HasKey("Id");

                    b.ToTable("DIA");
                });

            modelBuilder.Entity("LindaSonrisa.Models.EstadoCivil", b =>
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

            modelBuilder.Entity("LindaSonrisa.Models.Genero", b =>
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

            modelBuilder.Entity("LindaSonrisa.Models.Modulo", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("Comentario")
                        .HasColumnName("COMENTARIO")
                        .HasColumnType("VARCHAR2(250)");

                    b.Property<int>("DiaId")
                        .HasColumnName("DIA_ID");

                    b.Property<bool>("EstaInactivo")
                        .HasColumnName("ESTA_INACTIVO");

                    b.Property<TimeSpan>("HoraInicio")
                        .HasColumnName("HORA_INICIO");

                    b.Property<TimeSpan>("HoraTermino")
                        .HasColumnName("HORA_TERMINO");

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

            modelBuilder.Entity("LindaSonrisa.Models.Region", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnName("NOMBRE")
                        .HasColumnType("VARCHAR2(45)");

                    b.HasKey("Id");

                    b.ToTable("REGION");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Servicio", b =>
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

                    b.Property<bool>("EsPromocion")
                        .HasColumnName("ES_PROMOCION")
                        .HasColumnType("NUMBER(1)");

                    b.Property<bool>("EstaInactivo")
                        .HasColumnName("ESTA_INACTIVO")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("FileId")
                        .HasColumnName("FILE_ID")
                        .HasColumnType("NVARCHAR2(250)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnName("NOMBRE")
                        .HasColumnType("VARCHAR2(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NombreNormalizado")
                        .IsRequired()
                        .HasColumnName("NOMBRE_NORMALIZADO")
                        .HasColumnType("VARCHAR2(50)");

                    b.Property<int>("Precio")
                        .HasColumnName("PRECIO")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("Nombre")
                        .IsUnique()
                        .HasName("NOMBRE_SERVICIO_INDEX");

                    b.ToTable("SERVICIO");
                });

            modelBuilder.Entity("LindaSonrisa.Models.User", b =>
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

            modelBuilder.Entity("LindaSonrisa.Models.Usuario", b =>
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

                    b.Property<bool>("EstaInactivo")
                        .HasColumnName("ESTA_INACTIVO");

                    b.Property<int?>("EstadoCivilId")
                        .HasColumnName("ESTADO_CIVIL_ID");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnName("FECHA_NACIMIENTO")
                        .HasColumnType("DATE");

                    b.Property<string>("FileId")
                        .HasColumnName("FILE_ID")
                        .HasColumnType("NVARCHAR2(250)");

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

                    b.Property<string>("UserId")
                        .HasColumnName("ASP_NET_USER_ID");

                    b.HasKey("Id");

                    b.HasIndex("ComunaId");

                    b.HasIndex("EstadoCivilId");

                    b.HasIndex("GeneroId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("USUARIO");
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

            modelBuilder.Entity("LindaSonrisa.Models.Comuna", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Region", "Region")
                        .WithMany("Comuna")
                        .HasForeignKey("RegionId")
                        .HasConstraintName("COMUNA_REGION_FK");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Modulo", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Dia", "Dia")
                        .WithMany("Modulo")
                        .HasForeignKey("DiaId")
                        .HasConstraintName("MODULO_DIA_FK");

                    b.HasOne("LindaSonrisa.Models.Servicio", "Servicio")
                        .WithMany("Modulo")
                        .HasForeignKey("ServicioId")
                        .HasConstraintName("MODULO_SERVICIO_FK");

                    b.HasOne("LindaSonrisa.Models.Usuario", "Usuario")
                        .WithMany("Modulo")
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("MODULO_USUARIO_FK");
                });

            modelBuilder.Entity("LindaSonrisa.Models.Usuario", b =>
                {
                    b.HasOne("LindaSonrisa.Models.Comuna", "Comuna")
                        .WithMany("Usuario")
                        .HasForeignKey("ComunaId")
                        .HasConstraintName("USUARIO_COMUNA_FK");

                    b.HasOne("LindaSonrisa.Models.EstadoCivil", "EstadoCivil")
                        .WithMany("Usuario")
                        .HasForeignKey("EstadoCivilId")
                        .HasConstraintName("USUARIO_ESTADO_CIVIL_FK");

                    b.HasOne("LindaSonrisa.Models.Genero", "Genero")
                        .WithMany("Usuario")
                        .HasForeignKey("GeneroId")
                        .HasConstraintName("USUARIO_GENERO_FK");

                    b.HasOne("LindaSonrisa.Models.User", "User")
                        .WithOne("Usuario")
                        .HasForeignKey("LindaSonrisa.Models.Usuario", "UserId")
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
                    b.HasOne("LindaSonrisa.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LindaSonrisa.Models.User")
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

                    b.HasOne("LindaSonrisa.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LindaSonrisa.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
