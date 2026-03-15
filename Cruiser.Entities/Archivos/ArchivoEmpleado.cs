using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Archivos
{
    /// <summary>
    /// Archivo adjunto vinculado a un empleado: contrato de trabajo, nóminas,
    /// certificados de formación, documentos de identidad, partes de baja médica, etc.
    ///
    /// La Categoria clasifica el tipo de documento para facilitar el filtrado
    /// y la gestión documental del expediente del empleado.
    ///
    /// Los documentos de categoría Contrato y Nomina tienen acceso restringido
    /// al propio empleado, su supervisor directo y el rol RRHH.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.Categoria });
    ///   builder.HasIndex(x => x.FechaVencimiento).HasFilter("\"FechaVencimiento\" IS NOT NULL");
    /// </remarks>
    public class ArchivoEmpleado : EntidadBase
    {
        /// <summary>FK hacia el empleado al que pertenece este documento.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>FK hacia el archivo adjunto con los metadatos del documento.</summary>
        public Guid ArchivoAdjuntoId { get; set; }

        /// <summary>Categoría del documento del empleado para clasificación y filtrado.</summary>
        public CategoriaArchivoEmpleado Categoria { get; set; } = CategoriaArchivoEmpleado.Otros;

        /// <summary>
        /// Fecha de vencimiento del documento si aplica.
        /// Ejemplo: fecha de caducidad del carné de conducir, del certificado de formación, etc.
        /// Genera AlertaVencimiento automática cuando se acerca la fecha.
        /// </summary>
        public DateOnly? FechaVencimiento { get; set; }

        /// <summary>Descripción del contenido o contexto del documento adjunto.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Indica si el empleado tiene acceso para visualizar este documento desde la PWA.
        /// True para nóminas y contratos (el empleado puede descargar sus propios documentos).
        /// False para documentos internos de RRHH no destinados al empleado.
        /// </summary>
        public bool EsVisibleParaEmpleado { get; set; } = false;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Empleado al que pertenece este documento.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }

        /// <summary>Metadatos del archivo adjunto.</summary>
        public virtual ArchivoAdjunto? ArchivoAdjunto { get; set; }
    }
}
