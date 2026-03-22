using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Notificaciones
{
    /// <summary>
    /// Regla configurable que define cuándo y cómo generar una notificación automática.
    /// El evaluador de reglas de Hangfire revisa periódicamente las reglas activas
    /// y cuando la Condicion (expresión JSON) se cumple, ejecuta la AccionResultado.
    ///
    /// ESTRUCTURA DE CONDICION (JSON):
    /// {
    ///   "entidad": "Producto",
    ///   "campo": "StockActual",
    ///   "operador": "menorQue",
    ///   "valor": "StockMinimo",
    ///   "adicional": { "soloActivos": true }
    /// }
    ///
    /// ACCIONES DISPONIBLES:
    /// - "crearNotificacion": crea una Notificacion del TipoNotificacionId configurado.
    /// - "enviarEmail": envía un email directo sin crear notificación en panel.
    /// - "ambos": crea notificación Y envía email.
    ///
    /// UltimaEjecucion permite que el evaluador evite reprocesar reglas
    /// que ya se evaluaron recientemente, controlando la frecuencia de disparo.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.Condicion).HasColumnType("jsonb");
    ///   builder.Property(x => x.AccionResultado).HasMaxLength(50);
    ///   builder.HasIndex(x => x.EstaActiva);
    ///   builder.HasIndex(x => x.Condicion).HasMethod("gin");
    /// </remarks>
    public class ReglaNotificacion : EntidadBase
    {
        /// <summary>Nombre descriptivo de la regla para identificarla en la administración.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del evento o condición que monitoriza esta regla.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Condición de disparo en formato JSONB.
        /// Define la entidad monitoreada, el campo, el operador y el valor umbral.
        /// Evaluada periódicamente por el job de Hangfire.
        /// </summary>
        public string? Condicion { get; set; }

        /// <summary>FK hacia el tipo de notificación que se genera cuando se cumple la condición.</summary>
        public Guid TipoNotificacionId { get; set; }

        /// <summary>
        /// Acción a ejecutar cuando se cumple la condición.
        /// Valores: "crearNotificacion", "enviarEmail", "ambos".
        /// </summary>
        public string AccionResultado { get; set; } = "crearNotificacion";

        /// <summary>Indica si la regla está activa y será evaluada por Hangfire.</summary>
        public bool EstaActiva { get; set; } = true;

        /// <summary>
        /// Fecha y hora UTC de la última vez que se evaluó esta regla.
        /// Permite controlar la frecuencia de evaluación y evitar disparos duplicados.
        /// </summary>
        public DateTime? UltimaEjecucion { get; set; }

        /// <summary>Número de veces que esta regla ha generado una notificación desde su creación.</summary>
        public int NumeroDisparos { get; set; } = 0;

        /// <summary>
        /// Minutos mínimos entre disparos consecutivos de esta regla para el mismo registro.
        /// Evita bombardear al usuario con la misma alerta repetidamente. Por defecto: 60 minutos.
        /// </summary>
        public int MinutosEntrDisparos { get; set; } = 60;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Tipo de notificación generada cuando se dispara esta regla.</summary>
        public virtual TipoNotificacion? TipoNotificacion { get; set; }
    }
}
