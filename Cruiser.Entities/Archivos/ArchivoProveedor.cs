using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Archivos
{
    /// <summary>
    /// Archivo adjunto vinculado a un proveedor: certificados ISO, pólizas de seguro,
    /// fichas de seguridad de productos químicos, contratos de suministro, catálogos.
    ///
    /// Los documentos de homologación (Categoria = Homologacion) son especialmente
    /// importantes: el sistema verifica que estén vigentes antes de permitir
    /// la creación de nuevas órdenes de compra a ese proveedor.
    ///
    /// FechaVencimiento genera AlertaVencimiento automática para certificados
    /// ISO y pólizas de seguro que tienen fecha de caducidad.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => new { x.ProveedorId, x.Categoria });
    ///   builder.HasIndex(x => x.FechaVencimiento).HasFilter("\"FechaVencimiento\" IS NOT NULL");
    /// </remarks>
    public class ArchivoProveedor : EntidadBase
    {
        /// <summary>FK hacia el proveedor al que pertenece este documento.</summary>
        public Guid ProveedorId { get; set; }

        /// <summary>FK hacia el archivo adjunto con los metadatos del documento.</summary>
        public Guid ArchivoAdjuntoId { get; set; }

        /// <summary>Categoría del documento del proveedor.</summary>
        public CategoriaArchivoProveedor Categoria { get; set; } = CategoriaArchivoProveedor.Otros;

        /// <summary>
        /// Fecha de vencimiento del documento.
        /// Ejemplo: fecha de caducidad de la póliza de seguro, del certificado ISO.
        /// Genera AlertaVencimiento automática cuando se acerca la caducidad.
        /// </summary>
        public DateOnly? FechaVencimiento { get; set; }

        /// <summary>Descripción del contenido o alcance del documento adjunto.</summary>
        public string? Descripcion { get; set; }

        /// <summary>Número de certificado o referencia del documento cuando aplica.</summary>
        public string? NumeroCertificado { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Proveedor al que pertenece este documento.</summary>
        public virtual Comercial.Proveedor? Proveedor { get; set; }

        /// <summary>Metadatos del archivo adjunto.</summary>
        public virtual ArchivoAdjunto? ArchivoAdjunto { get; set; }
    }
}
