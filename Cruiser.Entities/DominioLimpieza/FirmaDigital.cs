using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Firma digital del cliente obtenida en la pantalla del dispositivo del empleado
    /// al finalizar el servicio de limpieza. Sirve como acuse de recibo y conformidad
    /// del cliente con el servicio prestado.
    ///
    /// La firma se captura en un canvas HTML5 en la PWA del empleado. La imagen
    /// resultante (PNG en base64) se almacena como archivo en el servidor.
    ///
    /// EsValida=false indica que la firma fue rechazada posteriormente
    /// (por ejemplo, el cliente alega que no firmó). En este caso se crea
    /// una IncidenciaServicio para investigación.
    ///
    /// MODO OFFLINE: la firma se captura offline, se almacena en IndexedDB
    /// del dispositivo y se sube al servidor al recuperar la conexión.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.FirmadoPorNombre).IsRequired().HasMaxLength(150);
    ///   builder.Property(x => x.RutaImagenFirma).IsRequired().HasMaxLength(1000);
    ///   builder.HasIndex(x => x.OrdenServicioId).IsUnique();
    /// </remarks>
    public class FirmaDigital : EntidadBase
    {
        /// <summary>FK hacia la orden de servicio para la que se obtuvo la firma.</summary>
        public Guid OrdenServicioId { get; set; }

        /// <summary>FK hacia el cliente que firmó la conformidad.</summary>
        public Guid ClienteId { get; set; }

        /// <summary>FK hacia el empleado testigo que recogió la firma del cliente.</summary>
        public Guid EmpleadoTestigoId { get; set; }

        /// <summary>Nombre completo de la persona que firmó (puede no ser el titular del contrato).</summary>
        public string FirmadoPorNombre { get; set; } = string.Empty;

        /// <summary>Cargo o relación de la persona que firmó con el cliente.</summary>
        public string? FirmadoPorCargo { get; set; }

        /// <summary>Ruta relativa del archivo PNG de la imagen de la firma en el servidor.</summary>
        public string RutaImagenFirma { get; set; } = string.Empty;

        /// <summary>Fecha y hora UTC en que se obtuvo la firma del cliente.</summary>
        public DateTime FechaFirma { get; set; } = DateTime.UtcNow;

        /// <summary>Latitud GPS donde se obtuvo la firma.</summary>
        public decimal? Latitud { get; set; }

        /// <summary>Longitud GPS donde se obtuvo la firma.</summary>
        public decimal? Longitud { get; set; }

        /// <summary>
        /// Indica si la firma es válida y aceptada.
        /// False indica que ha sido impugnada y está en investigación.
        /// </summary>
        public bool EsValida { get; set; } = true;

        /// <summary>Motivo de invalidación de la firma si EsValida = false.</summary>
        public string? MotivoInvalidacion { get; set; }

        /// <summary>Indica si la firma fue capturada en modo offline y sincronizada después.</summary>
        public bool FueCapturadaOffline { get; set; } = false;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Orden de servicio a la que pertenece la firma digital.</summary>
        public virtual OrdenServicio? OrdenServicio { get; set; }
    }
}
