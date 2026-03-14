using Cruiser.Entities.Base;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Configuración del servidor SMTP para el envío de emails mediante MailKit.
    /// Entidad Singleton: solo existe un registro activo en BD.
    ///
    /// Soporta Gmail (puerto 587 TLS), Outlook/Office365 (puerto 587 STARTTLS)
    /// y servidores SMTP personalizados.
    ///
    /// La contraseña SMTP se almacena en variables de entorno de Railway (SMTP_PASSWORD),
    /// NO en base de datos. Este campo almacena solo el nombre de la variable de entorno.
    ///
    /// Desde la interfaz de administración se puede probar la conexión SMTP en vivo
    /// antes de guardar la configuración.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.ServidorSMTP).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.UsuarioSMTP).HasMaxLength(256);
    ///   builder.Property(x => x.EmailRemitente).IsRequired().HasMaxLength(256);
    ///   builder.Property(x => x.NombreRemitente).HasMaxLength(200);
    ///   builder.Property(x => x.PlantillaHTMLBase).HasMaxLength(500);
    /// </remarks>
    public class ConfiguracionEmail : EntidadBase
    {
        /// <summary>
        /// Hostname o IP del servidor SMTP.
        /// Ejemplos: "smtp.gmail.com", "smtp.office365.com", "mail.empresa.com".
        /// </summary>
        public string ServidorSMTP { get; set; } = string.Empty;

        /// <summary>Puerto del servidor SMTP. Valores comunes: 25, 465 (SSL), 587 (TLS/STARTTLS).</summary>
        public int Puerto { get; set; } = 587;

        /// <summary>
        /// Indica si se debe usar SSL/TLS para la conexión SMTP.
        /// Puerto 465 = SSL implícito (UsarSSL=true).
        /// Puerto 587 = STARTTLS (UsarSSL=false, el servidor negocia TLS automáticamente).
        /// </summary>
        public bool UsarSSL { get; set; } = false;

        /// <summary>
        /// Indica si se debe usar autenticación SMTP (usuario/contraseña).
        /// Prácticamente todos los servidores modernos lo requieren.
        /// </summary>
        public bool RequiereAutenticacion { get; set; } = true;

        /// <summary>
        /// Nombre de usuario (email) para autenticarse en el servidor SMTP.
        /// Para Gmail: la dirección de Gmail completa.
        /// Para Outlook: el email de la cuenta de Microsoft 365.
        /// </summary>
        public string? UsuarioSMTP { get; set; }

        /// <summary>
        /// Dirección de email que aparece como remitente en los emails enviados.
        /// Puede ser diferente a UsuarioSMTP (alias, noreply@empresa.com, etc.).
        /// </summary>
        public string EmailRemitente { get; set; } = string.Empty;

        /// <summary>
        /// Nombre visible del remitente que aparece en el cliente de email del destinatario.
        /// Ejemplo: "CruiserCat - Notificaciones", "Empresa S.L.".
        /// </summary>
        public string? NombreRemitente { get; set; }

        /// <summary>
        /// Código de la PlantillaEmail que se usa como wrapper HTML base para todos los emails.
        /// Proporciona el diseño corporativo coherente (logo, colores, footer) sin repetirlo.
        /// </summary>
        public string? PlantillaHTMLBase { get; set; }

        /// <summary>
        /// Número máximo de intentos de reenvío cuando falla el envío de un email.
        /// Gestionado por Hangfire con backoff exponencial. Por defecto 3 intentos.
        /// </summary>
        public int MaxReintentos { get; set; } = 3;

        /// <summary>
        /// Minutos de espera entre reintentos de envío fallido.
        /// Hangfire aplica backoff exponencial: 1×Espera, 2×Espera, 4×Espera.
        /// </summary>
        public int MinutosEntreReintentos { get; set; } = 5;

        /// <summary>
        /// Indica si el envío de emails está activo en el sistema.
        /// False desactiva completamente el envío sin eliminar la configuración.
        /// Útil para entornos de desarrollo/testing.
        /// </summary>
        public bool EstaActivo { get; set; } = true;

        /// <summary>
        /// Email de prueba donde se redirigen todos los emails en entorno de desarrollo.
        /// Nulo en producción. Si tiene valor, todos los emails van a esta dirección.
        /// </summary>
        public string? EmailPruebas { get; set; }
    }
}
