using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Catalogo
{
    /// <summary>
    /// Producto o artículo del catálogo de la empresa. Puede ser un material de limpieza,
    /// un EPI (equipo de protección individual), una herramienta o un servicio de limpieza.
    ///
    /// Los campos de precios almacenan el precio base; las variaciones por cliente
    /// o por contrato se gestionan mediante ListaPrecio y DetalleListaPrecio.
    ///
    /// Los campos JSONB DatosAdicionales permiten almacenar atributos específicos
    /// de categorías especiales sin necesidad de crear columnas adicionales.
    ///
    /// El stock no se almacena directamente en Producto sino en HistorialStock
    /// (que se actualiza con cada MovimientoInventario) para mantener integridad
    /// y trazabilidad completa de todos los movimientos.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.SKU).IsRequired().HasMaxLength(50);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.PrecioCompra).HasPrecision(18, 4);
    ///   builder.Property(x => x.PrecioVenta).HasPrecision(18, 4);
    ///   builder.Property(x => x.PesoKg).HasPrecision(10, 3);
    ///   builder.Property(x => x.VolumenLitros).HasPrecision(10, 3);
    ///   builder.Property(x => x.DatosAdicionales).HasColumnType("jsonb");
    ///   builder.HasIndex(x => x.SKU).IsUnique();
    ///   builder.HasIndex(x => x.Nombre);
    ///   builder.HasIndex(x => new { x.CategoriaId, x.EstaActivo });
    ///   builder.HasIndex(x => x.DisponibleVenta);
    ///   builder.HasIndex(x => x.DatosAdicionales).HasMethod("gin");
    /// </remarks>
    public class Producto : EntidadBase
    {
        // ── Identificación ───────────────────────────────────────────────────

        /// <summary>
        /// Stock Keeping Unit: código único interno del producto para gestión de inventario.
        /// Generado automáticamente si no se proporciona (prefijo categoría + correlativo).
        /// Ejemplo: "LIM-DES-001", "EPI-GUA-NIT-L", "MAQ-FRE-001".
        /// </summary>
        public string SKU { get; set; } = string.Empty;

        /// <summary>
        /// Referencia del fabricante o proveedor principal del producto.
        /// Útil para pedidos y para cruzar con el código del proveedor (ProveedorProducto).
        /// </summary>
        public string? ReferenciaFabricante { get; set; }

        /// <summary>
        /// Nombre comercial completo del producto.
        /// Ejemplo: "Lejía desinfectante 5L concentrada", "Guantes nitrilo talla L caja 100ud".
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Descripción corta del producto para mostrar en listados y documentos.
        /// Máximo 500 caracteres. Para descripción larga usar DescripcionCompleta.
        /// </summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Descripción técnica completa del producto en formato HTML.
        /// Se muestra en la ficha detalle del producto y en documentos de cotización.
        /// </summary>
        public string? DescripcionCompleta { get; set; }

        // ── Clasificación ────────────────────────────────────────────────────

        /// <summary>
        /// Identificador de la categoría a la que pertenece este producto.
        /// FK hacia Categorias. Permite navegar el árbol de categorías.
        /// </summary>
        public Guid CategoriaId { get; set; }

        /// <summary>
        /// Identificador de la unidad de medida en la que se gestiona el stock.
        /// Ejemplo: Kilogramos para productos a granel, Unidades para artículos.
        /// FK hacia UnidadesMedida.
        /// </summary>
        public Guid UnidadMedidaId { get; set; }

        /// <summary>
        /// Identificador de la unidad de medida en la que se vende/asigna el producto.
        /// Puede diferir de la unidad de stock (ej. stock en litros, venta en botellas).
        /// Nulo si es la misma que UnidadMedidaId.
        /// </summary>
        public Guid? UnidadMedidaVentaId { get; set; }

        /// <summary>
        /// Identificador del impuesto aplicable por defecto a este producto.
        /// FK hacia Impuestos. Puede ser sobreescrito en facturas línea a línea.
        /// </summary>
        public Guid? ImpuestoId { get; set; }

        // ── Precios y costos ─────────────────────────────────────────────────

        /// <summary>
        /// Precio de compra unitario más reciente en la moneda base del sistema.
        /// Se actualiza automáticamente al recibir una orden de compra.
        /// Usado como base para el cálculo del costo promedio ponderado en inventario.
        /// </summary>
        public decimal PrecioCompra { get; set; } = 0m;

        /// <summary>
        /// Precio de venta/asignación unitario base en la moneda base del sistema.
        /// Es el precio antes de aplicar descuentos de listas de precios por cliente.
        /// </summary>
        public decimal PrecioVenta { get; set; } = 0m;

        /// <summary>
        /// Margen de beneficio objetivo en porcentaje.
        /// Ejemplo: 35.00 = 35% de margen sobre el precio de compra.
        /// Se usa como referencia para verificar que el precio de venta sea rentable.
        /// </summary>
        public decimal? MargenObjetivoPorcentaje { get; set; }

        // ── Stock y almacenamiento ───────────────────────────────────────────

        /// <summary>
        /// Stock mínimo de seguridad. Cuando el stock disponible cae por debajo de este umbral,
        /// se genera una alerta de bajo stock via SignalR y email.
        /// </summary>
        public decimal StockMinimo { get; set; } = 0m;

        /// <summary>
        /// Stock máximo recomendado. Cuando el stock supera este nivel tras una recepción,
        /// se genera una alerta de exceso de stock para evitar sobrecostos de almacenamiento.
        /// </summary>
        public decimal? StockMaximo { get; set; }

        /// <summary>
        /// Punto de reorden: nivel de stock en el que se debe generar automáticamente
        /// una orden de compra o sugerencia de reposición.
        /// </summary>
        public decimal? PuntoReorden { get; set; }

        /// <summary>
        /// Cantidad estándar por lote que se solicita al proveedor en cada pedido.
        /// Usada como cantidad por defecto en PlantillaOrdenCompra.
        /// </summary>
        public decimal? CantidadLoteEstandar { get; set; }

        // ── Dimensiones físicas ──────────────────────────────────────────────

        /// <summary>
        /// Peso del producto en kilogramos.
        /// Usado en el cálculo de capacidad de carga de vehículos y en tarifas de envío.
        /// </summary>
        public decimal? PesoKg { get; set; }

        /// <summary>
        /// Alto del producto en centímetros para cálculo de volumen de almacenamiento.
        /// </summary>
        public decimal? AltoCm { get; set; }

        /// <summary>
        /// Ancho del producto en centímetros.
        /// </summary>
        public decimal? AnchoCm { get; set; }

        /// <summary>
        /// Profundidad del producto en centímetros.
        /// </summary>
        public decimal? ProfundidadCm { get; set; }

        /// <summary>
        /// Volumen del producto en litros.
        /// Especialmente relevante para productos químicos líquidos de limpieza.
        /// </summary>
        public decimal? VolumenLitros { get; set; }

        // ── Flags de comportamiento ──────────────────────────────────────────

        /// <summary>
        /// Indica si el producto es peligroso (requiere ficha de seguridad REACH/SDS
        /// y EPPs específicos al manipularlo).
        /// Los productos peligrosos se gestionan con restricciones adicionales en el sistema.
        /// </summary>
        public bool EsPeligroso { get; set; } = false;

        /// <summary>
        /// Indica si el producto es perecedero (tiene fecha de caducidad por lote).
        /// Los productos perecederos requieren gestión FIFO y alertas de caducidad.
        /// </summary>
        public bool EsPerecedero { get; set; } = false;

        /// <summary>
        /// Indica si el producto requiere refrigeración en almacenamiento y transporte.
        /// Afecta a la asignación a almacenes con control de temperatura.
        /// </summary>
        public bool RequiereRefrigeracion { get; set; } = false;

        /// <summary>
        /// Indica si el producto puede ser asignado o vendido a clientes.
        /// False para productos internos (consumibles de empresa no facturables).
        /// </summary>
        public bool DisponibleVenta { get; set; } = true;

        /// <summary>
        /// Indica si el producto es destacado en el catálogo.
        /// Los productos destacados aparecen primero en listados y en el portal de cliente.
        /// </summary>
        public bool EsDestacado { get; set; } = false;

        /// <summary>
        /// Indica si el sistema debe controlar el stock de este producto.
        /// False para productos de tipo servicio o artículos de coste directo.
        /// </summary>
        public bool ControlaStock { get; set; } = true;

        /// <summary>
        /// Indica si el producto requiere número de lote para su trazabilidad.
        /// Obligatorio para productos peligrosos, perecederos y medicamentos.
        /// </summary>
        public bool RequiereLote { get; set; } = false;

        /// <summary>
        /// Indica si el producto requiere número de serie individual por unidad.
        /// Para maquinaria y equipos de alto valor donde se rastrea cada unidad.
        /// </summary>
        public bool RequiereNumeroSerie { get; set; } = false;

        // ── JSONB adicional ──────────────────────────────────────────────────

        /// <summary>
        /// Datos adicionales específicos del tipo de producto en formato JSONB.
        /// Permite almacenar atributos especiales sin crear columnas adicionales.
        /// Ejemplo para productos químicos: { "pH": 12.5, "densidad": 1.05,
        /// "temperaturaAlmacenamiento": "5-25ºC", "incompatibles": ["ácidos"] }
        /// </summary>
        public string? DatosAdicionales { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Categoría a la que pertenece el producto.</summary>
        public virtual Categoria? Categoria { get; set; }

        /// <summary>Unidad de medida de stock del producto.</summary>
        public virtual UnidadMedida? UnidadMedida { get; set; }

        /// <summary>Unidad de medida de venta del producto (puede diferir del stock).</summary>
        public virtual UnidadMedida? UnidadMedidaVenta { get; set; }

        /// <summary>Impuesto por defecto aplicable al producto.</summary>
        public virtual Configuracion.Impuesto? Impuesto { get; set; }

        /// <summary>Códigos de barras registrados para este producto.</summary>
        public virtual ICollection<CodigoBarraProducto> CodigosBarras { get; set; }
            = new List<CodigoBarraProducto>();

        /// <summary>Imágenes del producto en distintas resoluciones.</summary>
        public virtual ICollection<ImagenProducto> Imagenes { get; set; }
            = new List<ImagenProducto>();

        /// <summary>Valores de atributos diferenciadores asignados a este producto.</summary>
        public virtual ICollection<ValorAtributoProducto> ValoresAtributos { get; set; }
            = new List<ValorAtributoProducto>();

        /// <summary>Relaciones con productos sustitutos, complementarios y accesorios.</summary>
        public virtual ICollection<ProductoSustituto> ProductosRelacionados { get; set; }
            = new List<ProductoSustituto>();

        /// <summary>Lotes de inventario activos e históricos de este producto.</summary>
        public virtual ICollection<LoteProducto> Lotes { get; set; }
            = new List<LoteProducto>();

        /// <summary>Ficha de seguridad del producto (obligatoria si EsPeligroso=true).</summary>
        public virtual FichaSeguridad? FichaSeguridad { get; set; }
    }
}
