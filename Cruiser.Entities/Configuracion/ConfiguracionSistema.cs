using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Configuración global del sistema. Entidad Singleton: solo existe un registro en BD.
    /// Almacena los datos de la empresa, configuración visual, parámetros fiscales,
    /// formatos de números y fechas, y configuración de seguridad global.
    ///
    /// Se carga al arrancar la aplicación y se cachea en memoria durante 10 minutos
    /// mediante ICacheService para evitar consultas constantes a la base de datos.
    ///
    /// El logo y favicon se almacenan como rutas relativas al sistema de archivos del servidor.
    /// Al actualizar desde la UI, el preview en topbar/login se refresca automáticamente
    /// invalidando la caché del sistema.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.NombreEmpresa).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.NIF).HasMaxLength(20);
    ///   builder.Property(x => x.Email).HasMaxLength(256);
    ///   builder.Property(x => x.Telefono).HasMaxLength(20);
    ///   builder.Property(x => x.SitioWeb).HasMaxLength(200);
    ///   builder.Property(x => x.RutaLogo).HasMaxLength(500);
    ///   builder.Property(x => x.RutaFavicon).HasMaxLength(500);
    ///   builder.Property(x => x.MonedaCodigoISO).HasMaxLength(3).HasDefaultValue("EUR");
    ///   builder.Property(x => x.FormatoFecha).HasMaxLength(20).HasDefaultValue("dd/MM/yyyy");
    ///   builder.Property(x => x.ZonaHoraria).HasMaxLength(50).HasDefaultValue("Europe/Madrid");
    ///
    ///   Restricción Singleton en la aplicación:
    ///   No se aplica restricción única en BD. Se gestiona en la capa de servicio.
    ///   Se usa HasData() en el seed para insertar el único registro inicial.
    /// </remarks>
    public class ConfiguracionSistema : EntidadBase
    {
        // ── Datos de la empresa ──────────────────────────────────────────────

        /// <summary>Nombre completo de la empresa o negocio que usa el sistema.</summary>
        public string NombreEmpresa { get; set; } = string.Empty;

        /// <summary>Nombre comercial o abreviado de la empresa (para documentos y encabezados).</summary>
        public string? NombreComercial { get; set; }

        /// <summary>
        /// NIF, CIF o número de identificación fiscal de la empresa.
        /// Se imprime en todas las facturas y documentos fiscales generados por QuestPDF.
        /// </summary>
        public string? NIF { get; set; }

        /// <summary>Email de contacto principal de la empresa (se muestra en documentos).</summary>
        public string? Email { get; set; }

        /// <summary>Teléfono de contacto principal de la empresa.</summary>
        public string? Telefono { get; set; }

        /// <summary>Sitio web corporativo de la empresa.</summary>
        public string? SitioWeb { get; set; }

        /// <summary>
        /// Ruta relativa del logo corporativo en el sistema de archivos.
        /// Resoluciones recomendadas: 200×60px (topbar), 400×120px (documentos PDF).
        /// Formatos soportados: PNG (preferido, con transparencia) y SVG.
        /// </summary>
        public string? RutaLogo { get; set; }

        /// <summary>
        /// Ruta relativa del favicon de la aplicación web.
        /// Formato recomendado: ICO o PNG 32×32px.
        /// </summary>
        public string? RutaFavicon { get; set; }

        // ── Dirección fiscal ─────────────────────────────────────────────────

        /// <summary>Calle y número de la dirección fiscal de la empresa.</summary>
        public string? DireccionFiscal { get; set; }

        /// <summary>Código postal de la dirección fiscal.</summary>
        public string? CodigoPostalFiscal { get; set; }

        /// <summary>Ciudad de la dirección fiscal.</summary>
        public string? CiudadFiscal { get; set; }

        /// <summary>Provincia de la dirección fiscal.</summary>
        public string? ProvinciaFiscal { get; set; }

        /// <summary>País de la dirección fiscal. Por defecto: España.</summary>
        public string? PaisFiscal { get; set; } = "España";

        // ── Configuración fiscal y monetaria ────────────────────────────────

        /// <summary>
        /// Código ISO 4217 de la moneda base del sistema. Por defecto "EUR".
        /// Se usa en todos los cálculos y documentos fiscales.
        /// </summary>
        public string MonedaCodigoISO { get; set; } = "EUR";

        /// <summary>
        /// Porcentaje de IVA aplicado por defecto en nuevos productos y servicios.
        /// Puede ser sobreescrito por cada producto/servicio individualmente.
        /// </summary>
        public decimal PorcentajeIVAPorDefecto { get; set; } = 21.00m;

        /// <summary>
        /// Indica si los precios en la interfaz y documentos se muestran con IVA incluido.
        /// False = precios sin IVA + IVA desglosado. True = precio final con IVA.
        /// </summary>
        public bool MostrarPreciosConIVA { get; set; } = false;

        /// <summary>
        /// Número de decimales para mostrar importes monetarios en la UI y documentos.
        /// Por defecto 2 (Ej: 1.234,56 €).
        /// </summary>
        public int DecimalesMoneda { get; set; } = 2;

        // ── Configuración de formatos ────────────────────────────────────────

        /// <summary>
        /// Formato de fecha utilizado en la interfaz de usuario.
        /// Por defecto "dd/MM/yyyy" (formato español).
        /// </summary>
        public string FormatoFecha { get; set; } = "dd/MM/yyyy";

        /// <summary>
        /// Zona horaria principal de la empresa en formato IANA.
        /// Por defecto "Europe/Madrid". Se usa para cálculos de fechas y jobs Hangfire.
        /// </summary>
        public string ZonaHoraria { get; set; } = "Europe/Madrid";

        /// <summary>
        /// Separador decimal para importes monetarios.
        /// Por defecto "," (estándar español: 1.234,56).
        /// </summary>
        public string SeparadorDecimal { get; set; } = ",";

        /// <summary>
        /// Separador de miles para importes monetarios.
        /// Por defecto "." (estándar español: 1.234,56).
        /// </summary>
        public string SeparadorMiles { get; set; } = ".";

        // ── Configuración de seguridad global ───────────────────────────────

        /// <summary>
        /// Número máximo de intentos de login fallidos antes de bloquear la cuenta.
        /// Por defecto 5 intentos.
        /// </summary>
        public int MaxIntentosFallidos { get; set; } = 5;

        /// <summary>
        /// Minutos que permanece bloqueada una cuenta tras alcanzar el máximo de intentos fallidos.
        /// Por defecto 30 minutos. 0 = bloqueo indefinido hasta desbloqueo manual.
        /// </summary>
        public int MinutosBloqueo { get; set; } = 30;

        /// <summary>
        /// Tiempo de vida en minutos del token JWT de acceso.
        /// Por defecto 15 minutos. Menor tiempo = mayor seguridad.
        /// </summary>
        public int TiempoVidaJWTMinutos { get; set; } = 15;

        /// <summary>
        /// Tiempo de vida en días del Refresh Token.
        /// Por defecto 7 días. Controla la duración máxima de sesión sin reautenticación.
        /// </summary>
        public int TiempoVidaRefreshTokenDias { get; set; } = 7;

        /// <summary>
        /// Número de contraseñas anteriores que no pueden reutilizarse.
        /// Por defecto 5. El sistema verifica el historial al cambiar contraseña.
        /// </summary>
        public int HistorialContrasenas { get; set; } = 5;

        /// <summary>
        /// Tiempo en minutos de advertencia antes de la expiración del JWT que se muestra
        /// al usuario mediante SweetAlert2 invitándole a renovar la sesión.
        /// Por defecto 5 minutos antes de los 15 minutos de vida del JWT.
        /// </summary>
        public int MinutosAdvertenciaExpiracion { get; set; } = 5;

        // ── Configuración de stock ───────────────────────────────────────────

        /// <summary>
        /// Indica si el sistema debe controlar el stock de productos y generar alertas.
        /// False desactiva completamente el módulo de alertas de stock.
        /// </summary>
        public bool ControlarStock { get; set; } = true;

        /// <summary>
        /// Indica si el sistema debe bloquear la venta/asignación cuando el stock llega a cero.
        /// False permite stock negativo (ventas en descubierto).
        /// </summary>
        public bool BloquearVentaSinStock { get; set; } = true;

        // ── Datos del administrador del sistema ──────────────────────────────

        /// <summary>
        /// Email del administrador principal del sistema.
        /// Recibe alertas críticas, notificaciones de seguridad y errores del sistema.
        /// </summary>
        public string? EmailAdministrador { get; set; }

        /// <summary>
        /// Versión actual del sistema CruiserCat, leída desde la versión del ensamblado.
        /// Se muestra en el footer de la interfaz.
        /// </summary>
        public string? VersionSistema { get; set; }

        /// <summary>
        /// Texto personalizado que aparece en el pie de página de todos los documentos PDF
        /// generados por QuestPDF (facturas, contratos, reportes).
        /// </summary>
        public string? TextoPiePaginaPDF { get; set; }
    }
}
