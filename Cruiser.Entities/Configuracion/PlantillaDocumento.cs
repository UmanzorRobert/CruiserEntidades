using Cruiser.Entities.Base;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Plantilla HTML personalizable para la generación de documentos PDF mediante QuestPDF.
    /// Permite personalizar el aspecto visual de facturas, contratos, cotizaciones,
    /// órdenes de servicio y otros documentos sin modificar el código fuente.
    ///
    /// El ContenidoHTML sirve como base que QuestPDF renderiza a PDF.
    /// Las variables {{NombreVariable}} se reemplazan con los datos reales del documento.
    ///
    /// El campo VariablesDisponibles documenta las variables que puede usar el administrador
    /// al editar la plantilla desde la interfaz de administración.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.TipoDocumento).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.Version).HasMaxLength(20);
    ///   builder.Property(x => x.VariablesDisponibles).HasColumnType("jsonb");
    ///   builder.HasIndex(x => new { x.Codigo, x.Version }).IsUnique();
    ///   builder.HasIndex(x => x.TipoDocumento);
    /// </remarks>
    public class PlantillaDocumento : EntidadBase
    {
        /// <summary>
        /// Código único de la plantilla. Ejemplo: "FACTURA_ESTANDAR", "CONTRATO_SERVICIO_V2".
        /// Es el identificador para cargar la plantilla en el servicio de generación de PDFs.
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo de la plantilla para mostrar en la interfaz de administración.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de documento al que aplica esta plantilla.
        /// Ejemplos: "Factura", "FacturaRectificativa", "ContratoServicio",
        /// "Cotizacion", "OrdenServicio", "ReporteStock".
        /// </summary>
        public string TipoDocumento { get; set; } = string.Empty;

        /// <summary>
        /// Contenido HTML de la plantilla del documento.
        /// QuestPDF lo renderiza a PDF reemplazando las variables {{NombreVariable}}.
        /// Debe incluir estilos CSS inline para máxima compatibilidad de renderizado.
        /// </summary>
        public string ContenidoHTML { get; set; } = string.Empty;

        /// <summary>
        /// Mapa JSON de variables disponibles con su descripción.
        /// Almacenado en JSONB para consultas y como documentación en la UI.
        /// Formato: { "NombreEmpresa": "Nombre de la empresa emisora",
        ///            "NumeroFactura": "Número correlativo de la factura" }
        /// </summary>
        public string? VariablesDisponibles { get; set; }

        /// <summary>
        /// Versión de la plantilla en formato semántico.
        /// Al crear una nueva versión, la anterior se mantiene para reproducir
        /// documentos históricos con el aspecto original de cuando fueron generados.
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// Indica si esta es la versión activa que se usa para generar nuevos documentos.
        /// Solo puede haber una versión activa por TipoDocumento al mismo tiempo.
        /// </summary>
        public bool EsActiva { get; set; } = true;

        /// <summary>
        /// Orientación de página por defecto de los PDFs generados con esta plantilla.
        /// "Portrait" (vertical) o "Landscape" (horizontal).
        /// </summary>
        public string OrientacionPagina { get; set; } = "Portrait";

        /// <summary>
        /// Tamaño de página por defecto. Ejemplos: "A4", "A5", "Letter".
        /// </summary>
        public string TamanoPagina { get; set; } = "A4";

        /// <summary>
        /// Descripción del propósito de la plantilla y notas de edición para el administrador.
        /// </summary>
        public string? Descripcion { get; set; }
    }
}
