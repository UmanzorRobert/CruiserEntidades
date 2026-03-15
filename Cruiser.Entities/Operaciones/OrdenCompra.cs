using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Operaciones
{
    /// <summary>
    /// Orden de compra emitida a un proveedor para la adquisición de productos.
    /// Gestiona el ciclo completo desde la creación hasta la recepción total,
    /// pasando por aprobación interna y confirmación del proveedor.
    ///
    /// Soporta recepción parcial: los productos pueden recibirse en múltiples
    /// entregas, actualizando CantidadRecibida en cada DetalleOrdenCompra.
    ///
    /// El PDF de la orden se genera con QuestPDF y puede enviarse al proveedor
    /// como adjunto del email de notificación mediante MailKit.
    ///
    /// La trazabilidad fiscal se mantiene mediante el campo NumeroOrden correlativo
    /// y los registros de AprobacionDocumento vinculados.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NumeroOrden).IsRequired().HasMaxLength(30);
    ///   builder.Property(x => x.BaseImponible).HasPrecision(18, 2);
    ///   builder.Property(x => x.TotalIVA).HasPrecision(18, 2);
    ///   builder.Property(x => x.Total).HasPrecision(18, 2);
    ///   builder.Property(x => x.PorcentajeDescuento).HasPrecision(5, 2);
    ///   builder.HasIndex(x => x.NumeroOrden).IsUnique();
    ///   builder.HasIndex(x => new { x.ProveedorId, x.EstadoOrdenId });
    ///   builder.HasIndex(x => x.FechaEmision);
    ///   builder.HasIndex(x => x.FechaEntregaEstimada).HasFilter("\"FechaEntregaEstimada\" IS NOT NULL");
    /// </remarks>
    public class OrdenCompra : EntidadBase
    {
        /// <summary>
        /// Número único correlativo de la orden de compra.
        /// Formato: "OC-2026-0001". Sin saltos garantizados por la SerieFactura.
        /// Es el identificador legal y comercial de la orden.
        /// </summary>
        public string NumeroOrden { get; set; } = string.Empty;

        /// <summary>FK hacia el proveedor al que se emite la orden de compra.</summary>
        public Guid ProveedorId { get; set; }

        /// <summary>FK hacia el estado actual de la orden en su ciclo de vida.</summary>
        public Guid EstadoOrdenId { get; set; }

        /// <summary>
        /// FK hacia el almacén donde se recibirá la mercancía pedida.
        /// Determina el destino de los MovimientosInventario generados en la recepción.
        /// </summary>
        public Guid AlmacenId { get; set; }

        /// <summary>FK hacia el empleado que creó la orden de compra.</summary>
        public Guid CreadoPorId { get; set; }

        // ── Fechas ───────────────────────────────────────────────────────────

        /// <summary>Fecha de emisión de la orden de compra.</summary>
        public DateOnly FechaEmision { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        /// <summary>
        /// Fecha estimada de entrega acordada con el proveedor.
        /// Se calcula automáticamente: FechaEmision + ProveedorProducto.TiempoEntregaDias.
        /// </summary>
        public DateOnly? FechaEntregaEstimada { get; set; }

        /// <summary>Fecha real de la última recepción de mercancía de esta orden.</summary>
        public DateOnly? FechaUltimaRecepcion { get; set; }

        /// <summary>Fecha y hora UTC en que se aprobó la orden internamente.</summary>
        public DateTime? FechaAprobacion { get; set; }

        /// <summary>FK hacia el usuario que aprobó la orden.</summary>
        public Guid? AprobadoPorId { get; set; }

        // ── Importes y fiscal ────────────────────────────────────────────────

        /// <summary>Suma de importes de las líneas antes de impuestos y descuentos globales.</summary>
        public decimal BaseImponible { get; set; } = 0m;

        /// <summary>Porcentaje de descuento global aplicado sobre la base imponible.</summary>
        public decimal PorcentajeDescuento { get; set; } = 0m;

        /// <summary>Importe de descuento global: BaseImponible × PorcentajeDescuento / 100.</summary>
        public decimal ImporteDescuento { get; set; } = 0m;

        /// <summary>Base imponible neta tras aplicar el descuento global.</summary>
        public decimal BaseImponibleNeta { get; set; } = 0m;

        /// <summary>Importe total de IVA calculado sobre la base imponible neta.</summary>
        public decimal TotalIVA { get; set; } = 0m;

        /// <summary>Importe total de la orden: BaseImponibleNeta + TotalIVA.</summary>
        public decimal Total { get; set; } = 0m;

        /// <summary>FK hacia la moneda de facturación del proveedor. Nulo = moneda base (EUR).</summary>
        public Guid? MonedaId { get; set; }

        // ── Logística y entrega ──────────────────────────────────────────────

        /// <summary>Condiciones de entrega acordadas (EXW, FCA, CIF, etc.).</summary>
        public string? CondicionesEntrega { get; set; }

        /// <summary>Dirección de entrega si difiere del almacén principal.</summary>
        public string? DireccionEntrega { get; set; }

        /// <summary>Notas o instrucciones especiales para el proveedor.</summary>
        public string? NotasProveedor { get; set; }

        /// <summary>Notas internas no visibles en el PDF enviado al proveedor.</summary>
        public string? NotasInternas { get; set; }

        /// <summary>Ruta relativa del PDF generado de la orden de compra.</summary>
        public string? RutaDocumentoPDF { get; set; }

        /// <summary>
        /// FK hacia la PlantillaOrdenCompra desde la que se generó esta orden.
        /// Nulo si se creó manualmente o desde una nueva orden.
        /// </summary>
        public Guid? PlantillaOrigenId { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Estado actual de la orden de compra.</summary>
        public virtual EstadoOrden? EstadoOrden { get; set; }

        /// <summary>Líneas de productos solicitados en la orden.</summary>
        public virtual ICollection<DetalleOrdenCompra> Detalles { get; set; }
            = new List<DetalleOrdenCompra>();

        /// <summary>Aprobaciones del documento en el flujo de aprobación.</summary>
        public virtual ICollection<AprobacionDocumento> Aprobaciones { get; set; }
            = new List<AprobacionDocumento>();
    }
}
