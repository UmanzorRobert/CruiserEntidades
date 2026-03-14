using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Catalogo
{
    /// <summary>
    /// Imagen de un producto en distintas resoluciones generadas automáticamente
    /// al subir la imagen original mediante el módulo de upload de la ficha de producto.
    ///
    /// Al subir una imagen, el sistema genera automáticamente 5 resoluciones:
    /// Original (sin modificar), Large (800px), Medium (400px), Small (200px), Thumbnail (100px).
    ///
    /// Solo puede haber una imagen con EsPrincipal=true por producto.
    /// La imagen principal se muestra en listados, documentos PDF y portal de cliente.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.RutaOriginal).IsRequired().HasMaxLength(500);
    ///   builder.Property(x => x.TextoAlternativo).HasMaxLength(200);
    ///   builder.HasIndex(x => new { x.ProductoId, x.EsPrincipal })
    ///          .HasFilter("\"EsPrincipal\" = true").IsUnique();
    ///   builder.HasIndex(x => new { x.ProductoId, x.Orden });
    /// </remarks>
    public class ImagenProducto : EntidadBase
    {
        /// <summary>
        /// Identificador del producto al que pertenece esta imagen.
        /// FK hacia Productos.
        /// </summary>
        public Guid ProductoId { get; set; }

        /// <summary>
        /// Ruta relativa de la imagen original sin redimensionar.
        /// Almacenada en el sistema de archivos del servidor en la carpeta /uploads/productos/.
        /// Formato recomendado: PNG o JPEG. Resolución mínima recomendada: 800×800px.
        /// </summary>
        public string RutaOriginal { get; set; } = string.Empty;

        /// <summary>
        /// Ruta de la versión grande (800px de ancho máximo, calidad 85%).
        /// Se usa en la ficha detalle del producto en la interfaz de administración.
        /// </summary>
        public string? RutaLarge { get; set; }

        /// <summary>
        /// Ruta de la versión mediana (400px de ancho máximo, calidad 80%).
        /// Se usa en listados de productos con imagen y en cotizaciones PDF.
        /// </summary>
        public string? RutaMedium { get; set; }

        /// <summary>
        /// Ruta de la versión pequeña (200px de ancho máximo, calidad 75%).
        /// Se usa en tablas de inventario y en el portal de cliente.
        /// </summary>
        public string? RutaSmall { get; set; }

        /// <summary>
        /// Ruta de la miniatura (100×100px cuadrada con recorte centrado).
        /// Se usa en resultados de búsqueda y en el escáner de productos.
        /// </summary>
        public string? RutaThumbnail { get; set; }

        /// <summary>Ancho en píxeles de la imagen original.</summary>
        public int? AnchoOriginalPx { get; set; }

        /// <summary>Alto en píxeles de la imagen original.</summary>
        public int? AltoOriginalPx { get; set; }

        /// <summary>
        /// Tamaño del archivo original en bytes.
        /// Usado para mostrar el tamaño en la interfaz y controlar el almacenamiento.
        /// </summary>
        public long? TamanoBytes { get; set; }

        /// <summary>
        /// Tipo MIME del archivo de imagen.
        /// Ejemplos: "image/jpeg", "image/png", "image/webp".
        /// </summary>
        public string? TipoMIME { get; set; }

        /// <summary>
        /// Indica si esta es la imagen principal del producto.
        /// Solo puede haber una imagen principal por producto.
        /// </summary>
        public bool EsPrincipal { get; set; } = false;

        /// <summary>
        /// Texto alternativo descriptivo para accesibilidad (atributo alt del img HTML).
        /// Describe el contenido de la imagen para lectores de pantalla y SEO.
        /// </summary>
        public string? TextoAlternativo { get; set; }

        /// <summary>
        /// Posición de ordenación de la imagen en la galería de fotos del producto.
        /// Las imágenes con menor Orden se muestran primero.
        /// </summary>
        public int Orden { get; set; } = 0;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Producto al que pertenece esta imagen.</summary>
        public virtual Producto? Producto { get; set; }
    }
}
