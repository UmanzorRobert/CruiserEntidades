using System;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Registro histórico de cada cambio de estado de una orden de servicio.
    /// Append-only: permite trazar el ciclo de vida completo de la orden,
    /// quién realizó cada cambio y desde qué coordenadas GPS.
    ///
    /// Los cambios de estado registrados desde la PWA en campo incluyen
    /// las coordenadas GPS del empleado en el momento del cambio para
    /// verificar que estaba físicamente en las instalaciones del cliente.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   NO hereda EntidadBase — log append-only.
    ///   builder.HasKey(x => x.Id);
    ///   builder.HasIndex(x => new { x.OrdenServicioId, x.FechaCambio });
    /// </remarks>
    public class SeguimientoOrdenServicio
    {
        /// <summary>Identificador único del registro de seguimiento.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia la orden de servicio cuyo estado cambió.</summary>
        public Guid OrdenServicioId { get; set; }

        /// <summary>Estado anterior de la orden antes del cambio.</summary>
        public EstadoOrdenServicio EstadoAnterior { get; set; }

        /// <summary>Nuevo estado de la orden tras el cambio.</summary>
        public EstadoOrdenServicio EstadoNuevo { get; set; }

        /// <summary>Fecha y hora UTC del cambio de estado.</summary>
        public DateTime FechaCambio { get; set; } = DateTime.UtcNow;

        /// <summary>FK hacia el usuario (empleado o responsable) que realizó el cambio.</summary>
        public Guid CambiadoPorId { get; set; }

        /// <summary>
        /// Latitud GPS del empleado en el momento del cambio de estado.
        /// Registrada desde la PWA si el empleado concedió permiso de geolocalización.
        /// </summary>
        public decimal? Latitud { get; set; }

        /// <summary>Longitud GPS del empleado en el momento del cambio de estado.</summary>
        public decimal? Longitud { get; set; }

        /// <summary>Notas o motivo del cambio de estado.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Orden de servicio a la que pertenece este registro de seguimiento.</summary>
        public virtual OrdenServicio? OrdenServicio { get; set; }
    }
}
