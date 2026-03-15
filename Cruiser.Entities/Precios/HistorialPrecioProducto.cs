using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Precios
{
    /// <summary>
    /// Registro histórico de cada cambio de precio de un producto en el catálogo.
    /// Permite auditar la evolución de precios, analizar tendencias de costos
    /// y justificar revisiones de tarifas ante clientes y dirección.
    ///
    /// Se crea automáticamente cada vez que el sistema detecta un cambio en
    /// Producto.PrecioVenta o Producto.PrecioCompra mediante el interceptor de EF Core.
    ///
    /// NO hereda de EntidadBase: registro append-only de auditoría de precios.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.PrecioAnterior).HasPrecision(18, 4);
    ///   builder.Property(x => x.PrecioNuevo).HasPrecision(18, 4);
    ///   builder.Property(x => x.PorcentajeCambio).HasPrecision(7, 4);
    ///   builder.HasIndex(x => new { x.ProductoId, x.FechaCambio });
    ///   builder.HasIndex(x => x.TipoCambio);
    ///   builder.HasIndex(x => x.FechaCambio);
    /// </remarks>
    public class HistorialPrecioProducto
    {
        /// <summary>Identificador único del registro de cambio de precio.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia el producto cuyo precio cambió.</summary>
        public Guid ProductoId { get; set; }

        /// <summary>FK hacia el usuario que realizó el cambio de precio.</summary>
        public Guid? CambiadoPorId { get; set; }

        /// <summary>
        /// Precio anterior a este cambio.
        /// Puede ser PrecioVenta o PrecioCompra según EsPrecioVenta.
        /// </summary>
        public decimal PrecioAnterior { get; set; }

        /// <summary>Nuevo precio establecido tras el cambio.</summary>
        public decimal PrecioNuevo { get; set; }

        /// <summary>
        /// Variación porcentual del precio respecto al anterior.
        /// Calculado: ((PrecioNuevo - PrecioAnterior) / PrecioAnterior) × 100.
        /// Positivo = incremento de precio. Negativo = reducción de precio.
        /// </summary>
        public decimal PorcentajeCambio { get; set; }

        /// <summary>
        /// Indica si el cambio es sobre el precio de venta (true) o el precio de compra (false).
        /// </summary>
        public bool EsPrecioVenta { get; set; } = true;

        /// <summary>Motivo o causa del cambio de precio.</summary>
        public TipoCambioPrecio TipoCambio { get; set; }

        /// <summary>
        /// Descripción adicional del motivo del cambio.
        /// Ejemplo: "Revisión anual de tarifas Q1 2026", "Proveedor incrementa precio por inflación".
        /// </summary>
        public string? Motivo { get; set; }

        /// <summary>Fecha y hora UTC en que se realizó el cambio de precio.</summary>
        public DateTime FechaCambio { get; set; } = DateTime.UtcNow;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Producto cuyo precio cambió.</summary>
        public virtual Catalogo.Producto? Producto { get; set; }
    }
}
