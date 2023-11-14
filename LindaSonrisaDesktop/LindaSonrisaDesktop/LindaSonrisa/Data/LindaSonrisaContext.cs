using LindaSonrisa.Models.Identity;
using LindaSonrisa.Models.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LindaSonrisa.Data
{
    public class LindaSonrisaContext : IdentityDbContext<User>
    {
        public LindaSonrisaContext(DbContextOptions<LindaSonrisaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Secuencias

            modelBuilder.HasSequence<int>("PROVEEDOR_ID_SEQ")
                .HasMin(100)
                .HasMax(999)
                .StartsAt(100)
                .IncrementsBy(1);            
            
            modelBuilder.HasSequence<int>("ORDEN_ID_SEQ")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>("TIPO_PRODUCTO_ID_SEQ")
                .HasMin(100)
                .HasMax(999)
                .StartsAt(100)
                .IncrementsBy(1);            
            
            modelBuilder.HasSequence<int>("FAMILIA_PRODUCTO_ID_SEQ")
                .HasMin(100)
                .HasMax(999)
                .StartsAt(100)
                .IncrementsBy(1);

            // Se personaliza las clases de Identity (uso en base de datos Oracle)

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("ASP_NET_ROLE_CLAIM");

                entity.HasKey(rc => rc.Id);

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_ROLE_CLAIM_ROLE_ID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(rc => rc.RoleId)
                    .HasColumnName("ROLE_ID")
                    .HasMaxLength(450);

                entity.Property(rc => rc.ClaimType)
                    .HasColumnName("CLAIM_TYPE");

                entity.Property(rc => rc.ClaimValue)
                    .HasColumnName("CLAIM_VALUE");
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("ASP_NET_ROLE");

                entity.HasIndex(e => e.NormalizedName)
                    .HasName("ROLE_NAME_INDEX")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(256);

                entity.Property(e => e.NormalizedName)
                    .HasColumnName("NORMALIZED_NAME")
                    .HasMaxLength(256);

                entity.Property(e => e.ConcurrencyStamp).HasColumnName("CONCURRENCY_STAMP");

            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("ASP_NET_USER_CLAIM");

                entity.HasIndex(e => e.UserId)
                   .HasName("IX_USER_CLAIMS_USER_ID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_USER_CLAIMS_USER_ID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("USER_ID");

                entity.Property(e => e.ClaimType).HasColumnName("CLAIM_TYPE");

                entity.Property(e => e.ClaimValue).HasColumnName("CLAIM_VALUE");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.ToTable("ASP_NET_USER_LOGIN");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_USER_LOGIN_USER_ID");

                entity.Property(e => e.LoginProvider)
                    .HasColumnName("LOGIN_PROVIDER")
                    .HasMaxLength(128);

                entity.Property(e => e.ProviderKey)
                    .HasColumnName("PROVIDER_KEY")
                    .HasMaxLength(128);

                entity.Property(e => e.ProviderDisplayName).HasColumnName("PROVIDER_DISPLAY_NAME");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("USER_ID");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("ASP_NET_USER_ROLE");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_USER_ROLE_ROLE_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("ASP_NET_USER");

                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EMAIL_INDEX");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("USER_NAME_INDEX")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AccessFailedCount).HasColumnName("ACCESS_FAILED_COUNT");

                entity.Property(e => e.ConcurrencyStamp).HasColumnName("CONCURRENCY_STAMP");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(256);

                entity.Property(e => e.EmailConfirmed).HasColumnName("EMAIL_CONFIRMED");

                entity.Property(e => e.LockoutEnabled).HasColumnName("LOCKOUT_ENABLED");

                entity.Property(e => e.LockoutEnd).HasColumnName("LOCKOUT_END");

                entity.Property(e => e.NormalizedEmail)
                    .HasColumnName("NORMALIZED_EMAIL")
                    .HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName)
                    .HasColumnName("NORMALIZED_USER_NAME")
                    .HasMaxLength(256);

                entity.Property(e => e.PasswordHash).HasColumnName("PASSWORD_HASH");

                entity.Property(e => e.PhoneNumber).HasColumnName("PHONE_NUMBER");

                entity.Property(e => e.PhoneNumberConfirmed).HasColumnName("PHONE_NUMBER_CONFIRMED");

                entity.Property(e => e.SecurityStamp).HasColumnName("SECURITY_STAMP");

                entity.Property(e => e.TwoFactorEnabled).HasColumnName("TWO_FACTOR_ENABLED");

                entity.Property(e => e.UserName)
                    .HasColumnName("USER_NAME")
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.ToTable("ASP_NET_USER_TOKEN");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.LoginProvider)
                    .HasColumnName("LOGIN_PROVIDER")
                    .HasMaxLength(128);

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(128);

                entity.Property(e => e.Value).HasColumnName("VALUE");

            });

            // Se construyen los modelos de la base de datos

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIO");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("NOMBRE")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.ApMaterno)
                    .IsRequired()
                    .HasColumnName("AP_MATERNO")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.ApPaterno)
                    .IsRequired()
                    .HasColumnName("AP_PATERNO")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.Direccion)
                    .HasColumnName("DIRECCION")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("FECHA_NACIMIENTO")
                    .HasColumnType("DATE");

                entity.Property(e => e.FonoFijo)
                    .HasColumnName("FONO_FIJO")
                    .HasColumnType("VARCHAR2(8)");

                entity.Property(e => e.FonoMovil)
                    .HasColumnName("FONO_MOVIL")
                    .HasColumnType("VARCHAR2(8)");

                entity.Property(e => e.EsExtranjero)
                    .HasColumnName("ES_EXTRANJERO")
                    .IsRequired();

                entity.Property(e => e.CreadoEl)
                    .HasColumnName("CREADO_EL");

                entity.Property(e => e.ActualizadoEl)
                    .HasColumnName("ACTUALIZADO_EL");

                entity.Property(e => e.RutaFoto)
                    .HasColumnName("RUTA_FOTO")
                    .HasColumnType("NVARCHAR2(100)");

                entity.Property(e => e.ComunaId).HasColumnName("COMUNA_ID");

                entity.Property(e => e.GeneroId).HasColumnName("GENERO_ID");

                entity.Property(e => e.EstadoCivilId).HasColumnName("ESTADO_CIVIL_ID");

                // IdentityUser
                entity.Property(e => e.UserId).HasColumnName("ASP_NET_USER_ID");

                // AuthUser (Django)
                entity.Property(e => e.AuthUserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.Comuna)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.ComunaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("USUARIO_COMUNA_FK");

                entity.HasOne(d => d.EstadoCivil)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.EstadoCivilId)
                    .HasConstraintName("USUARIO_ESTADO_CIVIL_FK");

                entity.HasOne(d => d.Genero)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.GeneroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("USUARIO_GENERO_FK");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Usuario)
                    .HasForeignKey<Usuario>(d => d.UserId)
                    .HasConstraintName("USUARIO_ASP_NET_USER_FK");

                entity.HasOne(d => d.AuthUser)
                    .WithOne(p => p.Usuario)
                    .HasForeignKey<Usuario>(d => d.AuthUserId)
                    .HasConstraintName("USUARIO_AUTH_USER_FK");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("REGION");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("TITULO")
                    .HasColumnType("VARCHAR2(45)");
            });

            modelBuilder.Entity<Comuna>(entity =>
            {
                entity.ToTable("COMUNA");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RegionId).HasColumnName("REGION_ID");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("TITULO")
                    .HasColumnType("VARCHAR2(45)");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Comuna)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("COMUNA_REGION_FK");
            });

            modelBuilder.Entity<EstadoCivil>(entity =>
            {
                entity.ToTable("ESTADO_CIVIL");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("TITULO")
                    .HasColumnType("VARCHAR2(25)");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.ToTable("GENERO");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("TITULO")
                    .HasColumnType("VARCHAR2(25)");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.ToTable("SERVICIO");

                entity.HasIndex(e => e.Titulo)
                    .HasName("TITULO_SERVICIO_INDEX")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("TITULO")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.TituloNormalizado)
                    .IsRequired()
                    .HasColumnName("TITULO_NORMALIZADO")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.Precio)
                    .HasColumnName("PRECIO")
                    .HasColumnType("NUMBER(10)");

                entity.Property(e => e.Descuento)
                    .HasColumnName("DESCUENTO")
                    .HasColumnType("NUMBER(3,2)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("DESCRIPCION")
                    .HasColumnType("VARCHAR2(250)");

                entity.Property(e => e.Comentario)
                    .HasColumnName("COMENTARIO")
                    .HasColumnType("VARCHAR2(250)");

                entity.Property(e => e.EstaInactivo)
                    .IsRequired()
                    .HasColumnName("ESTA_INACTIVO");

                entity.Property(e => e.EsPromocion)
                    .IsRequired()
                    .HasColumnName("ES_PROMOCION");

                entity.Property(e => e.FileId)
                    .HasColumnName("FILE_ID")
                    .HasColumnType("NVARCHAR2(250)");
            });

            modelBuilder.Entity<Dia>(entity =>
            {
                entity.ToTable("DIA");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("TITULO")
                    .HasColumnType("VARCHAR2(25)");
            });

            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.ToTable("MODULO");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.HoraInicio)
                    .HasColumnName("HORA_INICIO")
                    .HasColumnType("VARCHAR2(5)")
                    .IsRequired();

                entity.Property(e => e.HoraTermino)
                    .HasColumnName("HORA_TERMINO")
                    .HasColumnType("VARCHAR2(5)")
                    .IsRequired();

                entity.Property(e => e.Disponible)
                    .IsRequired()
                    .HasColumnName("DISPONIBLE");

                entity.Property(e => e.Box)
                    .IsRequired()
                    .HasColumnName("BOX");

                entity.Property(e => e.DiaId).HasColumnName("DIA_ID");

                entity.Property(e => e.ServicioId).HasColumnName("SERVICIO_ID");

                entity.Property(e => e.UsuarioId).HasColumnName("USUARIO_ID");

                entity.HasOne(d => d.Dia)
                    .WithMany(p => p.Modulo)
                    .HasForeignKey(d => d.DiaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MODULO_DIA_FK");

                entity.HasOne(d => d.Servicio)
                    .WithMany(p => p.Modulo)
                    .HasForeignKey(d => d.ServicioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MODULO_SERVICIO_FK");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Modulo)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MODULO_USUARIO_FK");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.ToTable("PROVEEDOR");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EstaInactivo)
                    .HasColumnName("ESTA_INACTIVO")
                    .IsRequired();

                entity.Property(e => e.Comentario)
                    .HasColumnName("COMENTARIO")
                    .HasColumnType("VARCHAR2(250)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.FonoFijo)
                    .HasColumnName("FONO_FIJO")
                    .HasColumnType("VARCHAR2(8)");

                entity.Property(e => e.FonoMovil)
                    .HasColumnName("FONO_MOVIL")
                    .HasColumnType("VARCHAR2(8)");

                entity.Property(e => e.NombreNormalizado)
                    .IsRequired()
                    .HasColumnName("NOMBRE_NORMALIZADO")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("NOMBRE")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.RubroId).HasColumnName("RUBRO_ID");

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasColumnType("VARCHAR2(50)");

                entity.HasOne(d => d.Rubro)
                    .WithMany(p => p.Proveedor)
                    .HasForeignKey(d => d.RubroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PROVEEDOR_RUBRO_FK");
            });

            modelBuilder.Entity<Rubro>(entity =>
            {
                entity.ToTable("RUBRO");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("TITULO")
                    .HasColumnType("VARCHAR2(100)");
            });

            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.ToTable("CONTACTO");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ApMaterno)
                    .HasColumnName("AP_MATERNO")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.ApPaterno)
                    .HasColumnName("AP_PATERNO")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.FonoMovil)
                    .IsRequired()
                    .HasColumnName("FONO_MOVIL")
                    .HasColumnType("VARCHAR2(8)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("NOMBRE")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.ProveedorId).HasColumnName("PROVEEDOR_ID");

                entity.HasOne(d => d.Proveedor)
                    .WithMany(p => p.Contacto)
                    .HasForeignKey(d => d.ProveedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CONTACTO_PROVEEDOR_FK");
            });

            modelBuilder.Entity<TipoProducto>(entity =>
            {
                entity.ToTable("TIPO_PRODUCTO");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FamiliaProductoId).HasColumnName("FAMILIA_PRODUCTO_ID");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("TITULO")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.TituloNormalizado)
                    .IsRequired()
                    .HasColumnName("TITULO_NORMALIZADO")
                    .HasColumnType("VARCHAR2(50)");

                entity.HasOne(d => d.FamiliaProducto)
                    .WithMany(p => p.TipoProducto)
                    .HasForeignKey(d => d.FamiliaProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TIPO_FAMILIA_PRODUCTO_FK");
            });

            modelBuilder.Entity<FamiliaProducto>(entity =>
            {
                entity.ToTable("FAMILIA_PRODUCTO");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("TITULO")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.TituloNormalizado)
                    .IsRequired()
                    .HasColumnName("TITULO_NORMALIZADO")
                    .HasColumnType("VARCHAR2(50)");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.ToTable("RESERVA");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SolicitadoEl)
                    .HasColumnName("SOLICITADO_EL")
                    .HasColumnType("DATE");

                entity.Property(e => e.FueAnulada)
                    .IsRequired()
                    .HasColumnName("FUE_ANULADA");

                entity.Property(e => e.FechaReserva)
                    .HasColumnName("FECHA_RESERVA")
                    .HasColumnType("DATE")
                    .IsRequired();

                entity.Property(e => e.FechaAnulacion)
                    .HasColumnName("FECHA_ANULACION")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnName("FECHA_MODIFICACION")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Hora)
                    .HasColumnName("HORA")
                    .HasColumnType("NVARCHAR2(20)");

                entity.Property(e => e.ModuloId).HasColumnName("MODULO_ID");

                entity.Property(e => e.UsuarioId).HasColumnName("USUARIO_ID");

                entity.HasOne(d => d.Modulo)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.ModuloId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RESERVA_MODULO_FK");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RESERVA_USUARIO_FK");
            });

            modelBuilder.Entity<AuthUser>(entity =>
            {
                entity.ToTable("AUTH_USER");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("NUMBER(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .HasColumnName("PASSWORD")
                    .IsRequired(false);

                entity.Property(e => e.LastLogin)
                    .HasColumnName("LAST_LOGIN")
                    .HasColumnType("TIMESTAMP(6)")
                    .IsRequired(false);

                entity.Property(e => e.IsSuperUser)
                    .HasColumnName("IS_SUPERUSER")
                    .IsRequired();

                entity.Property(e => e.UserName)
                    .HasColumnName("USERNAME")
                    .IsRequired(false);

                entity.Property(e => e.FirstName)
                    .HasColumnName("FIRST_NAME")
                    .IsRequired(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("LAST_NAME")
                    .IsRequired(false);

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .IsRequired(false);

                entity.Property(e => e.IsStaff)
                    .HasColumnName("IS_STAFF")
                    .IsRequired();

                entity.Property(e => e.IsActive)
                    .HasColumnName("IS_ACTIVE")
                    .IsRequired();

                entity.Property(e => e.DateJoined)
                    .HasColumnName("DATE_JOINED")
                    .HasColumnType("TIMESTAMP(6)")
                    .IsRequired();
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("PRODUCTO");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EstaInactivo)
                    .HasColumnName("ESTA_INACTIVO")
                    .IsRequired();

                entity.Property(e => e.FileId)
                    .HasColumnName("FILE_ID")
                    .HasColumnType("NVARCHAR2(250)");

                entity.Property(e => e.Comentario)
                    .HasColumnName("COMENTARIO")
                    .HasColumnType("VARCHAR2(250)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("DESCRIPCION")
                    .HasColumnType("VARCHAR2(250)");

                entity.Property(e => e.Descuento)
                    .HasColumnName("DESCUENTO")
                    .HasColumnType("NUMBER(3,2)");

                entity.Property(e => e.EsPromocion).
                    HasColumnName("ES_PROMOCION")
                    .IsRequired();

                entity.Property(e => e.ProveedorId).HasColumnName("PROVEEDOR_ID");

                entity.Property(e => e.StockCritico)
                    .HasColumnName("STOCK_CRITICO")
                    .HasColumnType("NUMBER(10)");

                entity.Property(e => e.TipoProductoId).HasColumnName("TIPO_PRODUCTO_ID");

                entity.Property(e => e.TituloNormalizado)
                    .IsRequired()
                    .HasColumnName("TITULO_NORMALIZADO")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("TITULO")
                    .HasColumnType("VARCHAR2(50)");

                entity.HasOne(d => d.Proveedor)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.ProveedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PRODUCTO_PROVEEDOR_FK");

                entity.HasOne(d => d.TipoProducto)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.TipoProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PRODUCTO_TIPO_PRODUCTO_FK");
            });

            modelBuilder.Entity<Orden>(entity =>
            {
                entity.ToTable("ORDEN");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SolicitadoEl)
                    .HasColumnName("SOLICITADO_EL")
                    .IsRequired();

                entity.Property(e => e.ActualizadoEl)
                    .HasColumnName("ACTUALIZADO_EL");

                entity.Property(e => e.FueRecepcionada)
                    .HasColumnName("FUE_RECEPCIONADO")
                    .IsRequired();
            });

            modelBuilder.Entity<DetalleOrden>(entity => {

                entity.ToTable("DETALLE_ORDEN");

                entity.HasKey(d => new { d.OrdenId, d.ProductoId });

                entity.Property(e => e.Cantidad)
                    .HasColumnName("CANTIDAD")
                    .IsRequired();

                entity.Property(e => e.OrdenId)
                    .HasColumnName("ORDEN_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductoId)
                    .HasColumnName("PRODUCTO_ID")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<DetalleOrden>()
                .HasOne<Orden>(d => d.Orden)
                .WithMany(o => o.DetalleOrden)
                .HasForeignKey(d => d.OrdenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ORDEN_DETALLE_ORDEN_FK");

            modelBuilder.Entity<DetalleOrden>()
                .HasOne<Producto>(d => d.Producto)
                .WithMany(p => p.DetalleOrden)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PRODUCTO_DETALLE_ORDEN_FK");

            modelBuilder.Entity<RecepcionOrden>(entity => {

                entity.ToTable("RECEPCION_ORDEN");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Codificacion)
                    .HasColumnName("CODIFICACION")
                    .HasColumnType("VARCHAR2(25)");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("CANTIDAD");

                entity.Property(e => e.RecepcionadoEl)
                    .HasColumnName("RECEPCIONADO_EL")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Producto)
                    .HasColumnName("PRODUCTO")
                    .ValueGeneratedNever()
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.ProductoId)
                    .HasColumnName("PRODUCTO_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrdenId)
                    .HasColumnName("ORDEN_ID")
                    .ValueGeneratedNever();

                entity.HasOne<Orden>(r => r.Orden)
                    .WithMany(o => o.RecepcionOrden)
                    .HasForeignKey(d => d.OrdenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RECEPCION_ORDEN_FK");
            });
        }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Modulo> Modulo { get; set; }

        public DbSet<Servicio> Servicio { get; set; }

        public DbSet<Comuna> Comuna { get; set; }

        public DbSet<Region> Region { get; set; }

        public DbSet<Proveedor> Proveedor { get; set; }

        public DbSet<Contacto> Contacto { get; set; }

        public DbSet<Producto> Producto { get; set; }

        public DbSet<TipoProducto> TipoProducto { get; set; }

        public DbSet<FamiliaProducto> FamiliaProducto { get; set; }

        public DbSet<Reserva> Reserva { get; set; }

        public DbSet<AuthUser> AuthUser { get; set; }

        public DbSet<Orden> Orden { get; set; }
        public DbSet<DetalleOrden> DetalleOrden { get; set; }
    }
}
