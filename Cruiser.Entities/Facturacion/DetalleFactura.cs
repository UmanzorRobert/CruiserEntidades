using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Facturacion
{
    /// <summary>
    /// Línea de detalle de una factura con el servicio o producto facturado,
    /// su precio unitario, descuentos aplicados, tipo de IVA y código contable.
    ///
    /// El IVA se desglosa por línea (no un único IVA global) para cumplir con
    /// la normativa española que requiere desglose por tipo impositivo cuando
    /// una factura incluye productos con diferentes tipos de IVA.
    ///
    /// El CodigoCuentaContable facilita la exportación de datos a software contable
    /// externo (ContaPlus, Sage, A3, etc.) mediante la integración del módulo.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(500);
    ///   builder.Property(x => x.Cantidad).HasPrecision(18, 4);
    ///   builder.Property(x => x.PrecioUnitario).HasPrecision(18, 4);
    ///   builder.Property(x => x.PorcentajeDescuento).HasPrecision(5, 2);
    ///   builder.Property(x => x.PorcentajeIVA).HasPrecision(5, 2);
    ///   builder.Property(x => x.ImporteNeto).HasPrecision(18, 2);
    ///   builder.Property(x => x.ImporteIVA).HasPrecision(18, 2);
    ///   builder.Property(x => x.ImporteTotal).HasPrecision(18, 2);
    ///   builder.Property(x => x.CodigoCuentaContable).HasMaxLength(20);
    ///   builder.Property(x => x.CentroCosto).HasMaxLength(50);
    ///   builder.HasIndex(x => new { x.FacturaId, x.NumeroLinea });
    /// </remarks>
    public class DetalleFactura : EntidadBase
    {
        /// <summary>FK hacia la factura a la que pertenece esta línea de detalle.</summary>
        public Guid FacturaId { get; set; }

        /// <summary>
        /// FK hacia el producto o servicio facturado.
        /// Nulo para líneas de texto libre sin producto del catálogo.
        /// </summary>
        public Guid? ProductoId { get; set; }

        /// <summary>Número de línea para el orden de presentación en la factura. Desde 1.</summary>
        public int NumeroLinea { get; set; }

        /// <summary>
        /// Descripción de la línea tal como aparece en la factura PDF.
        /// Por defecto: Producto.Nombre. Puede editarse para personalizar la descripción.
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>Cantidad facturada en la unidad de medida del producto o servicio.</summary>
        public decimal Cantidad { get; set; }

        /// <summary>Precio unitario bruto antes de descuento e impuestos.</summary>
        public decimal PrecioUnitario { get; set; }

        /// <summary>Porcentaje de descuento de línea.</summary>
        public decimal PorcentajeDescuento { get; set; } = 0m;

        /// <summary>Importe de descuento de línea: PrecioUnitario × Cantidad × PorcentajeDescuento / 100.</summary>
        public decimal ImporteDescuento { get; set; } = 0m;

        /// <summary>
        /// Importe neto de la línea sin IVA: (PrecioUnitario × Cantidad) - ImporteDescuento.
        /// Es la base imponible de esta línea.
        /// </summary>
        public decimal ImporteNeto { get; set; }

        /// <summary>Porcentaje de IVA o impuesto indirecto aplicable a esta línea.</summary>
        public decimal PorcentajeIVA { get; set; } = 21m;

        /// <summary>Importe de IVA de la línea: ImporteNeto × PorcentajeIVA / 100.</summary>
        public decimal ImporteIVA { get; set; }

        /// <summary>Importe total de la línea con IVA: ImporteNeto + ImporteIVA.</summary>
        public decimal ImporteTotal { get; set; }

        /// <summary>
        /// Código de cuenta contable del Plan General de Contabilidad (PGC) para esta línea.
        /// Ejemplo: "705" (Prestaciones de servicios), "700" (Ventas de mercaderías).
        /// Usado en la exportación contable.
        /// </summary>
        public string? CodigoCuentaContable { get; set; }

        /// <summary>
        /// Centro de costo o analítica al que se imputa el ingreso de esta línea.
        /// Permite análisis de rentabilidad por centro de coste, zona o proyecto.
        /// </summary>
        public string? CentroCosto { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Factura a la que pertenece esta línea de detalle.</summary>
        public virtual Factura? Factura { get; set; }

        /// <summary>Producto o servicio facturado en esta línea.</summary>
        public virtual Catalogo.Producto? Producto { get; set; }
    }
}
