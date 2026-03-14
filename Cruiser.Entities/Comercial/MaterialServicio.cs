using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Línea de material o producto incluido en una cotización de servicio.
    /// Define qué productos se necesitan para ejecutar un tipo de servicio,
    /// con su cantidad estimada y si son obligatorios o recomendados.
    ///
    /// También se usa como catálogo de materiales recomendados por TipoServicio,
    /// permitiendo precargar automáticamente los materiales habituales
    /// al crear una nueva cotización de un tipo de servicio determinado.
    ///
    /// NO hereda de EntidadBase: es una línea de detalle transaccional.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.CantidadEstimada).HasPrecision(18, 4);
    ///   builder.Property(x => x.PrecioUnitario).HasPrecision(18, 4);
    ///   builder.Property(x => x.ImporteTotal).HasPrecision(18, 2);
    ///   builder.HasIndex(x => new { x.CotizacionServicioId, x.ProductoId });
    /// </remarks>
    public class MaterialServicio
    {
        /// <summary>Identificador único de la línea de material.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// FK hacia la cotización de servicio a la que pertenece esta línea.
        /// Nulo si es una línea de catálogo de materiales por tipo de servicio.
        /// </summary>
        public Guid? CotizacionServicioId { get; set; }

        /// <summary>
        /// FK hacia el tipo de servicio al que aplica este material (catálogo base).
        /// Permite definir los materiales habituales de cada tipo de servicio.
        /// </summary>
        public Guid? TipoServicioId { get; set; }

        /// <summary>FK hacia el producto del catálogo incluido como material.</summary>
        public Guid ProductoId { get; set; }

        /// <summary>
        /// Cantidad estimada del producto necesaria para ejecutar el servicio.
        /// Se expresa en la unidad de medida del producto.
        /// </summary>
        public decimal CantidadEstimada { get; set; }

        /// <summary>Precio unitario del material en la cotización. Por defecto: Producto.PrecioVenta.</summary>
        public decimal PrecioUnitario { get; set; }

        /// <summary>
        /// Importe total de la línea: CantidadEstimada × PrecioUnitario.
        /// Calculado automáticamente al guardar.
        /// </summary>
        public decimal ImporteTotal { get; set; }

        /// <summary>
        /// Indica si este material es obligatorio para la ejecución del servicio.
        /// True = no se puede completar el servicio sin este material.
        /// False = material recomendado o complementario.
        /// </summary>
        public bool EsObligatorio { get; set; } = true;

        /// <summary>Notas sobre el uso del material o especificaciones especiales.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Cotización de servicio a la que pertenece esta línea.</summary>
        public virtual CotizacionServicio? CotizacionServicio { get; set; }

        /// <summary>Producto del catálogo incluido como material.</summary>
        public virtual Catalogo.Producto? Producto { get; set; }
    }
}
