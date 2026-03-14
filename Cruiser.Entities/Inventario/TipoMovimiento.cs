using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Inventario
{
    /// <summary>
    /// Catálogo de tipos de movimiento de inventario que define el comportamiento
    /// de cada operación sobre el stock: si es entrada o salida, si afecta al costo,
    /// si requiere autorización, si genera alertas y si puede ser reversado.
    ///
    /// Este catálogo es la piedra angular del módulo de inventario: cada
    /// MovimientoInventario referencia un TipoMovimiento que define exactamente
    /// cómo debe comportarse la operación en el sistema.
    ///
    /// SEED INICIAL: Compra, Venta, Devolución Cliente, Devolución Proveedor,
    /// Ajuste Positivo, Ajuste Negativo, Transferencia Entrada, Transferencia Salida,
    /// Merma, Asignación Empleado, Ajuste Inventario Cíclico, Consumo Interno.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(50);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150);
    ///   builder.Property(x => x.Color).HasMaxLength(7);
    ///   builder.Property(x => x.Icono).HasMaxLength(100);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    /// </remarks>
    public class TipoMovimiento : EntidadBase
    {
        /// <summary>
        /// Código único del tipo de movimiento en SCREAMING_SNAKE_CASE.
        /// Ejemplo: "COMPRA", "VENTA", "AJUSTE_POSITIVO", "TRANSFERENCIA_SALIDA".
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo del tipo de movimiento para mostrar en la UI.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del propósito y uso del tipo de movimiento.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Naturaleza del movimiento: si incrementa (Entrada), decrementa (Salida)
        /// o hace ambos (Mixto, para transferencias) sobre el stock del almacén.
        /// </summary>
        public NaturalezaMovimiento Naturaleza { get; set; }

        /// <summary>
        /// Categoría funcional del movimiento para clasificación en reportes.
        /// </summary>
        public CategoriaMovimiento Categoria { get; set; }

        /// <summary>
        /// Indica si este tipo de movimiento actualiza el costo promedio ponderado del producto.
        /// True solo para movimientos de entrada de compra (con precio real de compra).
        /// </summary>
        public bool AfectaCosto { get; set; } = false;

        /// <summary>
        /// Indica si este tipo de movimiento requiere un documento soporte asociado
        /// (albarán, factura de compra, nota de crédito, etc.).
        /// </summary>
        public bool RequiereDocumento { get; set; } = false;

        /// <summary>
        /// Indica si este tipo de movimiento requiere autorización de un supervisor
        /// antes de ser completado y afectar al stock.
        /// </summary>
        public bool RequiereAutorizacion { get; set; } = false;

        /// <summary>
        /// Indica si este tipo de movimiento debe generar una notificación/alerta
        /// en el panel de alertas (campana SignalR) al completarse.
        /// </summary>
        public bool GeneraAlerta { get; set; } = false;

        /// <summary>
        /// Indica si este movimiento puede ser revertido mediante un movimiento compensatorio.
        /// Los movimientos de ajuste de inventario cíclico generalmente no son reversables.
        /// </summary>
        public bool EsReversible { get; set; } = true;

        /// <summary>
        /// Indica si este es un movimiento de sistema generado automáticamente
        /// (no puede ser creado manualmente desde la interfaz de usuario).
        /// </summary>
        public bool EsAutomatico { get; set; } = false;

        /// <summary>
        /// Color hexadecimal (#RRGGBB) para representar visualmente el tipo en la UI.
        /// Ejemplo: verde para entradas, rojo para salidas, azul para transferencias.
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// Clase CSS del icono Font Awesome para representar el tipo visualmente.
        /// Ejemplo: "fas fa-arrow-down" (entrada), "fas fa-arrow-up" (salida).
        /// </summary>
        public string? Icono { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Movimientos de inventario de este tipo registrados en el sistema.</summary>
        public virtual ICollection<MovimientoInventario> Movimientos { get; set; }
            = new List<MovimientoInventario>();
    }
}
