using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.GDPR
{
    /// <summary>
    /// Solicitud formal de ejercicio de derechos GDPR presentada por un interesado.
    /// Cubre los derechos del Capítulo III del RGPD: exportación (Art.20),
    /// anonimización (Art.17), rectificación (Art.16), limitación (Art.18) y acceso (Art.15).
    ///
    /// IMPORTANTE: El tipo "Anonimizacion" sustituye al concepto de "Eliminacion".
    /// NUNCA se realizan eliminaciones físicas (hard-delete) de registros con
    /// historial de facturación o auditoría. Los datos personales se pseudonomizan
    /// y se registra la operación en RegistroAnonimizacionGDPR.
    ///
    /// Plazo legal de respuesta: 30 días (Art. 12.3 RGPD), ampliable a 90 días
    /// en casos complejos con notificación al interesado.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.UrlDescargaArchivo).HasMaxLength(500);
    ///   builder.Property(x => x.MotivoRechazo).HasMaxLength(1000);
    ///   builder.Property(x => x.TokenVerificacionGDPR).HasMaxLength(256);
    ///   builder.HasIndex(x => x.TokenVerificacionGDPR).IsUnique()
    ///          .HasFilter("\"TokenVerificacionGDPR\" IS NOT NULL");
    ///   builder.HasIndex(x => new { x.ClienteId, x.Estado });
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.Estado });
    ///   builder.HasIndex(x => x.FechaSolicitud);
    /// </remarks>
    public class SolicitudGDPR : EntidadBase
    {
        /// <summary>
        /// Identificador del cliente que presenta la solicitud.
        /// Mutuamente excluyente con EmpleadoId.
        /// </summary>
        public Guid? ClienteId { get; set; }

        /// <summary>
        /// Identificador del empleado que presenta la solicitud.
        /// Mutuamente excluyente con ClienteId.
        /// </summary>
        public Guid? EmpleadoId { get; set; }

        /// <summary>
        /// Tipo de derecho GDPR que el interesado desea ejercer.
        /// </summary>
        public TipoSolicitudGDPR TipoSolicitud { get; set; }

        /// <summary>
        /// Estado actual de tramitación de la solicitud en su ciclo de vida.
        /// </summary>
        public EstadoSolicitudGDPR Estado { get; set; } = EstadoSolicitudGDPR.Pendiente;

        /// <summary>
        /// Fecha y hora UTC en que el interesado presentó la solicitud.
        /// Punto de inicio del cómputo del plazo legal de 30 días.
        /// </summary>
        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fecha y hora UTC límite para dar respuesta a la solicitud.
        /// Por defecto: FechaSolicitud + 30 días (Art. 12.3 RGPD).
        /// </summary>
        public DateTime FechaLimiteRespuesta { get; set; }

        /// <summary>
        /// Fecha y hora UTC en que se completó o rechazó la solicitud.
        /// Nulo si la solicitud sigue en trámite.
        /// </summary>
        public DateTime? FechaResolucion { get; set; }

        /// <summary>
        /// URL de descarga del archivo generado para solicitudes de tipo Exportacion.
        /// Nulo para otros tipos de solicitud. La URL expira en FechaExpiracionUrl.
        /// </summary>
        public string? UrlDescargaArchivo { get; set; }

        /// <summary>
        /// Fecha y hora UTC de expiración del enlace de descarga del archivo exportado.
        /// Por defecto: FechaResolucion + 7 días. Tras esta fecha el archivo se elimina del servidor.
        /// </summary>
        public DateTime? FechaExpiracionUrl { get; set; }

        /// <summary>
        /// Motivo justificado del rechazo de la solicitud.
        /// Obligatorio cuando Estado = Rechazada. Se notifica al interesado.
        /// Debe ser claro y específico según el RGPD (ej. "Obligación legal de conservación fiscal").
        /// </summary>
        public string? MotivoRechazo { get; set; }

        /// <summary>
        /// Indica si esta solicitud implica una operación de anonimización de datos.
        /// True para TipoSolicitud = Anonimizacion.
        /// </summary>
        public bool EsAnonimizacion { get; set; } = false;

        /// <summary>
        /// Identificador del registro de anonimización creado al completar la operación.
        /// FK hacia RegistroAnonimizacionGDPR. Nulo si aún no se ha ejecutado la anonimización.
        /// </summary>
        public Guid? RegistroAnonimizacionId { get; set; }

        /// <summary>
        /// Token único generado para verificar la identidad del interesado antes de ejecutar
        /// operaciones sensibles como la anonimización o la exportación de datos.
        /// Se envía al email del interesado y debe ser confirmado antes de proceder.
        /// </summary>
        public string? TokenVerificacionGDPR { get; set; }

        /// <summary>
        /// Fecha y hora UTC en que se ejecutó efectivamente la anonimización.
        /// Nulo si la anonimización aún no se ha realizado.
        /// </summary>
        public DateTime? FechaAnonimizacion { get; set; }

        /// <summary>
        /// Identificador del responsable de datos (DPO o admin) que tramitó y resolvió la solicitud.
        /// </summary>
        public Guid? TramitadaPorId { get; set; }

        /// <summary>
        /// Notas internas del responsable de datos sobre la tramitación de la solicitud.
        /// No se muestran al interesado. Solo visibles para el equipo de privacidad.
        /// </summary>
        public string? NotasInternas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Registro de la operación de anonimización asociada a esta solicitud.</summary>
        public virtual RegistroAnonimizacionGDPR? RegistroAnonimizacion { get; set; }
    }
}
