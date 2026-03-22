using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Notificaciones
{
    /// <summary>
    /// Catálogo de tipos de notificación del sistema. Define el comportamiento
    /// de cada tipo de alerta: prioridad, canales de envío, persistencia,
    /// sonido, tiempo de auto-ocultado y apariencia visual.
    ///
    /// Este catálogo permite que el administrador configure el comportamiento
    /// de cada tipo de notificación desde la interfaz sin modificar código.
    ///
    /// SEED: StockBajo, StockAgotado, ITVVencida, SeguroVencido, FormacionCaducada,
    ///       ContratoPorRenovar, FacturaVencida, OrdenServicioPendiente,
    ///       NuevaOrdenServicio, FichajeIncompleto, ErrorSincronizacion.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(50);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150);
    ///   builder.Property(x => x.Color).HasMaxLength(7);
    ///   builder.Property(x => x.Icono).HasMaxLength(100);
    ///   builder.Property(x => x.ClaseCSS).HasMaxLength(100);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    /// </remarks>
    public class TipoNotificacion : EntidadBase
    {
        /// <summary>
        /// Código único del tipo de notificación en SCREAMING_SNAKE_CASE.
        /// Ejemplo: "STOCK_BAJO", "ITV_VENCIDA", "FACTURA_VENCIDA".
        /// Usado en el código para referenciar el tipo sin depender del Guid.
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo del tipo de notificación para la interfaz.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del evento que origina este tipo de notificación.</summary>
        public string? Descripcion { get; set; }

        /// <summary>Nivel de prioridad que determina la presentación visual y el comportamiento.</summary>
        public PrioridadNotificacion Prioridad { get; set; } = PrioridadNotificacion.Info;

        /// <summary>
        /// Indica si la notificación requiere que el usuario realice una acción explícita
        /// (aprobar, revisar, corregir). Aparece con botón de acción en el panel.
        /// </summary>
        public bool RequiereAccion { get; set; } = false;

        /// <summary>
        /// Indica si la notificación debe persistir en el panel hasta que el usuario
        /// la marque como leída. True para alertas críticas de seguridad o compliance.
        /// </summary>
        public bool EsPersistente { get; set; } = false;

        /// <summary>
        /// Segundos hasta auto-ocultar el toast en la interfaz.
        /// 0 = no se auto-oculta (requiere cierre manual). Por defecto: 5 segundos.
        /// </summary>
        public int SegundosAutoOcultar { get; set; } = 5;

        /// <summary>
        /// Indica si se debe reproducir un sonido de alerta al recibir esta notificación.
        /// Solo para prioridades Advertencia, Error y Crítica.
        /// </summary>
        public bool ReproducirSonido { get; set; } = false;

        // ── Canales de envío ─────────────────────────────────────────────────

        /// <summary>Indica si este tipo de notificación se envía por el canal Sistema (SignalR).</summary>
        public bool EnviarSistema { get; set; } = true;

        /// <summary>Indica si este tipo de notificación se envía por email.</summary>
        public bool EnviarEmail { get; set; } = false;

        /// <summary>Indica si este tipo de notificación se envía como push notification.</summary>
        public bool EnviarPush { get; set; } = false;

        /// <summary>Indica si este tipo de notificación se envía por SMS.</summary>
        public bool EnviarSMS { get; set; } = false;

        // ── Apariencia visual ────────────────────────────────────────────────

        /// <summary>Color hexadecimal (#RRGGBB) del icono y borde de la notificación.</summary>
        public string? Color { get; set; }

        /// <summary>Clase CSS del icono Font Awesome. Ejemplo: "fa-bell", "fa-exclamation-triangle".</summary>
        public string? Icono { get; set; }

        /// <summary>Clase CSS adicional para aplicar estilos especiales al toast de la notificación.</summary>
        public string? ClaseCSS { get; set; }

        /// <summary>
        /// FK hacia la plantilla de email que se usa cuando EnviarEmail = true.
        /// Define el diseño HTML del email de notificación.
        /// </summary>
        public System.Guid? PlantillaEmailId { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Notificaciones generadas de este tipo.</summary>
        public virtual ICollection<Notificacion> Notificaciones { get; set; }
            = new List<Notificacion>();

        /// <summary>Reglas de notificación que generan notificaciones de este tipo.</summary>
        public virtual ICollection<ReglaNotificacion> Reglas { get; set; }
            = new List<ReglaNotificacion>();
    }
}
