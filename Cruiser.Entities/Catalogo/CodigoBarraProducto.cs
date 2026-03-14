using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Catalogo
{
    /// <summary>
    /// Código de barras o código bidimensional registrado para un producto.
    /// Un producto puede tener múltiples códigos de distintos tipos:
    /// el código EAN-13 del fabricante, un código QR interno, un código de proveedor, etc.
    ///
    /// El código marcado como EsPrincipal es el que se usa por defecto
    /// al escanear con QuaggaJS/ZXing-JS en la PWA y al generar etiquetas con QuestPDF.
    ///
    /// Solo puede haber un código EsPrincipal=true por producto.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(100);
    ///   builder.HasIndex(x => x.Codigo);
    ///   builder.HasIndex(x => new { x.ProductoId, x.EsPrincipal })
    ///          .HasFilter("\"EsPrincipal\" = true").IsUnique();
    ///
    ///   Relación:
    ///   builder.HasOne(c => c.Producto).WithMany(p => p.CodigosBarras)
    ///          .HasForeignKey(c => c.ProductoId).OnDelete(DeleteBehavior.Cascade);
    /// </remarks>
    public class CodigoBarraProducto : EntidadBase
    {
        /// <summary>
        /// Identificador del producto al que pertenece este código de barras.
        /// FK hacia Productos.
        /// </summary>
        public Guid ProductoId { get; set; }

        /// <summary>
        /// Valor del código de barras o QR. Debe ser único en el sistema.
        /// Ejemplo EAN-13: "8410208151887", QR interno: "PROD-LIM-DES-001".
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>
        /// Estándar o tipo del código de barras. Determina el algoritmo de lectura
        /// en el escáner QuaggaJS/ZXing-JS de la PWA.
        /// </summary>
        public TipoCodigoBarras Tipo { get; set; } = TipoCodigoBarras.EAN13;

        /// <summary>
        /// Indica si este es el código principal del producto.
        /// Solo puede haber un código principal por producto.
        /// Es el que aparece en etiquetas generadas y el que se busca primero al escanear.
        /// </summary>
        public bool EsPrincipal { get; set; } = false;

        /// <summary>
        /// Descripción del propósito o contexto de este código.
        /// Ejemplo: "Código del fabricante", "Código interno almacén",
        /// "Código proveedor Distribuciones García S.L.".
        /// </summary>
        public string? Descripcion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Producto al que pertenece este código de barras.</summary>
        public virtual Producto? Producto { get; set; }
    }
}