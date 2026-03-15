using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Operaciones
{
    /// <summary>
    /// Asignación o despacho de productos desde el almacén hacia un destino:
    /// cliente, empleado o consumo interno de la empresa.
    ///
    /// Genera automáticamente MovimientosInventario de salida al despacharse.
    /// Puede generar una Factura cuando el tipo es AsignacionCliente y los productos
    /// son de venta (no incluidos en el contrato de servicio fijo).
    ///
    /// El NumeroAlbaran es el número de albarán de entrega impreso en el documento PDF
    /// que firma el cliente al recibir los productos.
    ///
    /// La cadena VeriFactu aplica a la Factura generada, no al albarán.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NumeroAsignacion).IsRequired().HasMaxLength(30);
    ///   builder.Property(x => x.NumeroAlbaran).HasMaxLength(30);
    ///   builder.Property(x => x.BaseImponible).HasPrecision(18, 2);
    ///   builder.Property(x => x.Total).HasPrecision(18, 2);
    ///   builder.HasIndex(x => x.NumeroAsignacion).IsUnique();
    ///   builder.HasIndex(x => x.NumeroAlbaran).HasFilter("\"NumeroAlbaran\" IS NOT NULL");
    ///   builder.HasIndex(x => new { x.ClienteId, x.Estado });
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.Estado });
    ///   builder.HasIndex(x => x.FechaDespacho);
    /// </remarks>
    public class AsignacionProducto : EntidadBase
    {
        /// <summary>
        /// Número único correlativo de la asignación de productos.
        /// Formato: "ASIG-2026-0001".
        /// </summary>
        public string NumeroAsignacion { get; set; } = string.Empty;

        /// <summary>
        /// Número de albarán de entrega para la firma del cliente.
        /// Generado al despachar físicamente. Puede diferir del NumeroAsignacion.
        /// </summary>
        public string? NumeroAlbaran { get; set; }

        /// <summary>
        /// FK hacia el cliente destinatario. Nulo si la asignación es a empleado o interna.
        /// </summary>
        public Guid? ClienteId { get; set; }

        /// <summary>
        /// FK hacia el empleado destinatario de los materiales.
        /// Nulo si la asignación es a cliente o interna.
        /// </summary>
        public Guid? EmpleadoId { get; set; }

        /// <summary>FK hacia el almacén desde el que se despachan los productos.</summary>
        public Guid AlmacenId { get; set; }

        /// <summary>FK hacia la factura generada para esta asignación. Nulo hasta que se facture.</summary>
        public Guid? FacturaId { get; set; }

        /// <summary>FK hacia la orden de servicio relacionada con esta asignación de materiales.</summary>
        public Guid? OrdenServicioId { get; set; }

        /// <summary>Tipo de asignación: cliente, empleado o consumo interno.</summary>
        public TipoAsignacionProducto TipoAsignacion { get; set; }

        /// <summary>Estado actual de la asignación en su ciclo de vida.</summary>
        public EstadoAsignacion Estado { get; set; } = EstadoAsignacion.Pendiente;

        // ── Fechas ───────────────────────────────────────────────────────────

        /// <summary>Fecha y hora UTC programada para el despacho físico de los productos.</summary>
        public DateTime FechaDespachoEstimado { get; set; }

        /// <summary>Fecha y hora UTC real en que se despacharon los productos del almacén.</summary>
        public DateTime? FechaDespacho { get; set; }

        /// <summary>Fecha y hora UTC en que el destinatario confirmó la recepción.</summary>
        public DateTime? FechaConfirmacion { get; set; }

        // ── Importes (para asignaciones a cliente) ───────────────────────────

        /// <summary>Suma de importes de líneas antes de impuestos.</summary>
        public decimal BaseImponible { get; set; } = 0m;

        /// <summary>Importe total de IVA de la asignación.</summary>
        public decimal TotalIVA { get; set; } = 0m;

        /// <summary>Importe total de la asignación: BaseImponible + TotalIVA.</summary>
        public decimal Total { get; set; } = 0m;

        /// <summary>Notas o instrucciones de entrega para esta asignación.</summary>
        public string? Notas { get; set; }

        /// <summary>Motivo de la anulación cuando Estado = Anulada.</summary>
        public string? MotivoAnulacion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Cliente destinatario de la asignación.</summary>
        public virtual Comercial.Cliente? Cliente { get; set; }

        /// <summary>Empleado destinatario de los materiales.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }

        /// <summary>Líneas de productos incluidos en esta asignación.</summary>
        public virtual ICollection<DetalleAsignacion> Detalles { get; set; }
            = new List<DetalleAsignacion>();
    }
}
