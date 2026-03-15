using System;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Registro histórico de cada cambio de estado de un contrato de servicio.
    /// Append-only: se crea un registro cada vez que el contrato cambia de estado.
    /// Permite auditar completamente el ciclo de vida de cada contrato.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   NO hereda EntidadBase — log append-only.
    ///   builder.HasKey(x => x.Id);
    ///   builder.HasIndex(x => new { x.ContratoServicioId, x.FechaCambio });
    /// </remarks>
    public class HistorialEstadoContrato
    {
        /// <summary>Identificador único del registro histórico.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia el contrato cuyo estado cambió.</summary>
        public Guid ContratoServicioId { get; set; }

        /// <summary>Estado anterior del contrato antes del cambio.</summary>
        public EstadoContrato EstadoAnterior { get; set; }

        /// <summary>Nuevo estado del contrato tras el cambio.</summary>
        public EstadoContrato EstadoNuevo { get; set; }

        /// <summary>Fecha y hora UTC en que se realizó el cambio de estado.</summary>
        public DateTime FechaCambio { get; set; } = DateTime.UtcNow;

        /// <summary>FK hacia el usuario que realizó el cambio de estado.</summary>
        public Guid CambiadoPorId { get; set; }

        /// <summary>Motivo del cambio de estado. Obligatorio para estados Suspendido y Rescindido.</summary>
        public string? Motivo { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Contrato al que pertenece este registro de historial.</summary>
        public virtual ContratoServicio? ContratoServicio { get; set; }
    }
}
