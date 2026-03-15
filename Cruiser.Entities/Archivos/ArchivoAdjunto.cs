using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Archivos
{
    /// <summary>
    /// Archivo adjunto subido al sistema de gestión documental.
    /// Almacena los metadatos del archivo (nombre, ruta, tipo MIME, tamaño, hash)
    /// sin incluir el contenido binario, que se almacena en el sistema de archivos
    /// del servidor (o en almacenamiento en la nube en producción Railway).
    ///
    /// El HashMD5 permite detectar duplicados y verificar la integridad del archivo
    /// tras la subida o al descargarlo. Si dos archivos tienen el mismo hash,
    /// el sistema puede reutilizar el almacenamiento físico.
    ///
    /// Las miniaturas (RutaMiniatura) se generan automáticamente para imágenes
    /// durante el proceso de subida (ImageSharp o similar).
    ///
    /// El NombreSistema es el nombre con el que se almacena físicamente el archivo
    /// (GUID + extensión) para evitar colisiones y caracteres especiales.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NombreOriginal).IsRequired().HasMaxLength(300);
    ///   builder.Property(x => x.NombreSistema).IsRequired().HasMaxLength(300);
    ///   builder.Property(x => x.Ruta).IsRequired().HasMaxLength(1000);
    ///   builder.Property(x => x.Extension).HasMaxLength(20);
    ///   builder.Property(x => x.TipoMIME).HasMaxLength(100);
    ///   builder.Property(x => x.HashMD5).HasMaxLength(32);
    ///   builder.HasIndex(x => x.HashMD5).HasFilter("\"HashMD5\" IS NOT NULL");
    ///   builder.HasIndex(x => new { x.TipoArchivoId, x.SubidoPorId });
    ///   builder.HasIndex(x => x.FechaCreacion);
    /// </remarks>
    public class ArchivoAdjunto : EntidadBase
    {
        /// <summary>FK hacia el catálogo de tipos de archivo que define las validaciones aplicadas.</summary>
        public Guid TipoArchivoId { get; set; }

        /// <summary>FK hacia el usuario que subió el archivo al sistema.</summary>
        public Guid SubidoPorId { get; set; }

        /// <summary>Nombre original del archivo tal como lo envió el usuario desde su dispositivo.</summary>
        public string NombreOriginal { get; set; } = string.Empty;

        /// <summary>
        /// Nombre con el que se almacena el archivo en el servidor.
        /// Formato: {Guid}.{extension}. Evita colisiones y caracteres especiales.
        /// </summary>
        public string NombreSistema { get; set; } = string.Empty;

        /// <summary>Ruta relativa del archivo en el servidor desde la raíz de uploads.</summary>
        public string Ruta { get; set; } = string.Empty;

        /// <summary>Extensión del archivo sin el punto. Ejemplo: "pdf", "jpg", "xlsx".</summary>
        public string? Extension { get; set; }

        /// <summary>Tipo MIME del archivo. Ejemplo: "application/pdf", "image/jpeg".</summary>
        public string? TipoMIME { get; set; }

        /// <summary>Tamaño del archivo en bytes. Usado para mostrar el tamaño al usuario.</summary>
        public long TamanoBytes { get; set; }

        /// <summary>
        /// Hash MD5 del contenido del archivo para verificar integridad y detectar duplicados.
        /// Calculado automáticamente durante el proceso de subida.
        /// </summary>
        public string? HashMD5 { get; set; }

        // ── Imágenes ─────────────────────────────────────────────────────────

        /// <summary>Ancho de la imagen en píxeles. Nulo para archivos no imagen.</summary>
        public int? AnchoPixeles { get; set; }

        /// <summary>Alto de la imagen en píxeles. Nulo para archivos no imagen.</summary>
        public int? AltoPixeles { get; set; }

        /// <summary>
        /// Ruta relativa de la miniatura generada automáticamente durante la subida.
        /// Solo para imágenes cuando TipoArchivo.GenerarMiniatura = true.
        /// </summary>
        public string? RutaMiniatura { get; set; }

        /// <summary>Número de veces que el archivo ha sido descargado (estadístico).</summary>
        public int NumeroDescargas { get; set; } = 0;

        /// <summary>Descripción opcional del contenido del archivo.</summary>
        public string? Descripcion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Tipo de archivo del catálogo de tipos permitidos.</summary>
        public virtual Configuracion.TipoDocumento? TipoArchivo { get; set; }
    }
}
