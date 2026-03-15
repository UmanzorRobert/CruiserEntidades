using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Archivos
{
    /// <summary>
    /// Archivo adjunto vinculado a un producto del catálogo.
    /// Incluye imágenes del producto, fichas técnicas, manuales de uso y
    /// documentos de especificación para el equipo de compras y almacén.
    ///
    /// EsPrincipal indica que este es el archivo de imagen principal del producto,
    /// el que se muestra en listados, fichas y documentos generados.
    ///
    /// El Orden determina la secuencia de visualización cuando el producto
    /// tiene múltiples imágenes en la galería de la ficha del producto.
    ///
    /// Los archivos de imagen se redimensionan automáticamente al subirse
    /// a las cinco resoluciones (Original/Large/Medium/Small/Thumbnail)
    /// almacenadas en las rutas de ArchivoAdjunto.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.TextoAlternativo).HasMaxLength(200);
    ///   builder.HasIndex(x => new { x.ProductoId, x.Orden });
    ///   builder.HasIndex(x => x.EsPrincipal)
    ///          .HasFilter("\"EsPrincipal\" = true");
    /// </remarks>
    public class ArchivoProducto : EntidadBase
    {
        /// <summary>FK hacia el producto al que pertenece este archivo.</summary>
        public Guid ProductoId { get; set; }

        /// <summary>FK hacia el archivo adjunto con los metadatos del documento o imagen.</summary>
        public Guid ArchivoAdjuntoId { get; set; }

        /// <summary>
        /// Indica si este es el archivo de imagen principal del producto.
        /// Solo puede haber un archivo principal por producto.
        /// El archivo principal se muestra en listados, tarjetas y documentos.
        /// </summary>
        public bool EsPrincipal { get; set; } = false;

        /// <summary>
        /// Orden de visualización en la galería del producto.
        /// Menor número = se muestra primero. La imagen principal tiene Orden = 0.
        /// </summary>
        public int Orden { get; set; } = 0;

        /// <summary>
        /// Texto alternativo para accesibilidad web (atributo alt de la imagen).
        /// Describe el contenido visual de la imagen para lectores de pantalla.
        /// </summary>
        public string? TextoAlternativo { get; set; }

        /// <summary>
        /// Tipo de archivo del producto: imagen, ficha técnica, manual, certificado.
        /// Permite filtrar los archivos por tipo en la ficha del producto.
        /// </summary>
        public string? TipoArchivo { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Producto al que pertenece este archivo.</summary>
        public virtual Catalogo.Producto? Producto { get; set; }

        /// <summary>Metadatos del archivo adjunto.</summary>
        public virtual ArchivoAdjunto? ArchivoAdjunto { get; set; }
    }
}
