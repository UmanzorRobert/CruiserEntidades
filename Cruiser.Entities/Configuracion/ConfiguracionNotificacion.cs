using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Regla de configuración que define cuándo, cómo y a quién se envía
    /// una notificación automática para un tipo de evento determinado.
    ///
    /// Por ejemplo: "Cuando un contrato venza en 30 días, enviar notificación
    /// por Email y Sistema al rol Supervisor, usando la PlantillaEmail CONTRATO_POR_VENCER".
    ///
    /// Las notificaciones se generan mediante jobs de Hangfire que evalúan estas
    /// configuraciones periódicamente y crean las Notificaciones correspondientes.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.TipoEvento).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.RolesDestinatarios).HasColumnType("jsonb");
    ///   builder.HasIndex(x => new { x.TipoEvento, x.EstaActivo });
    ///
    ///   Estructura JSON de RolesDestinatarios:
    ///   ["Supervisor", "Administrador"]  o  ["SuperAdmin"]
    /// </remarks>
    public class ConfiguracionNotificacion : EntidadBase
    {
        /// <summary>
        /// Código del tipo de evento que dispara esta notificación.
        /// Ejemplos: "CONTRATO_POR_VENCER", "STOCK_MINIMO", "FACTURA_VENCIDA",
        /// "ITV_VEHICULO", "FORMACION_CADUCIDAD", "ORDEN_COMPLETADA".
        /// </summary>
        public string TipoEvento { get; set; } = string.Empty;

        /// <summary>Descripción legible del evento para mostrar en la interfaz de administración.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Canal a través del cual se envía la notificación.
        /// </summary>
        public CanalEnvioNotificacion CanalEnvio { get; set; } = CanalEnvioNotificacion.Sistema;

        /// <summary>
        /// Identificador de la PlantillaEmail a usar cuando CanalEnvio incluye Email.
        /// Nulo si el canal no incluye email o se usa el asunto/mensaje por defecto.
        /// </summary>
        public Guid? PlantillaEmailId { get; set; }

        /// <summary>
        /// Días de anticipación con los que se genera la notificación antes del evento.
        /// Ejemplo: 30 días antes del vencimiento de un contrato.
        /// 0 = notificación en el momento exacto del evento.
        /// </summary>
        public int DiasAnticipacion { get; set; } = 0;

        /// <summary>
        /// Hora del día (en formato HH:mm) en que se genera la notificación.
        /// El job de Hangfire evalúa si hay notificaciones pendientes a esta hora.
        /// Por defecto "08:00" (8 de la mañana en la zona horaria del sistema).
        /// </summary>
        public string HoraEnvio { get; set; } = "08:00";

        /// <summary>
        /// Roles cuyos usuarios deben recibir esta notificación, en formato JSON array.
        /// Almacenado en JSONB para flexibilidad.
        /// Ejemplo: ["Supervisor", "Administrador"]
        /// </summary>
        public string? RolesDestinatarios { get; set; }

        /// <summary>
        /// Indica si esta regla de notificación está activa.
        /// Las reglas inactivas no generan notificaciones aunque ocurra el evento.
        /// </summary>
        public bool EstaActivo { get; set; } = true;

        /// <summary>
        /// Fecha y hora UTC de la última vez que esta regla fue evaluada por Hangfire.
        /// Útil para diagnosticar si las notificaciones se están generando correctamente.
        /// </summary>
        public DateTime? UltimaEjecucion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Plantilla de email a usar para las notificaciones por email.</summary>
        public virtual PlantillaEmail? PlantillaEmail { get; set; }
    }
}
