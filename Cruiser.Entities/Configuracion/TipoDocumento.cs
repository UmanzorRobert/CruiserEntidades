using Cruiser.Entities.Base;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Catálogo de tipos de documentos de identidad soportados por el sistema.
    /// Incluye validaciones mediante expresiones regulares y máscaras de formato
    /// para NIF, CIF, NIE y pasaporte español/europeo.
    ///
    /// Se utiliza en la validación de documentos de Clientes, Empleados y Proveedores
    /// mediante FluentValidation en los DTOs correspondientes.
    ///
    /// SEED: NIF, CIF, NIE, Pasaporte UE, Pasaporte No-UE.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(20);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.PatronValidacion).HasMaxLength(200);
    ///   builder.Property(x => x.MascaraFormato).HasMaxLength(50);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    /// </remarks>
    public class TipoDocumento : EntidadBase
    {
        /// <summary>
        /// Código único del tipo de documento. Ejemplos: "NIF", "CIF", "NIE", "PASAPORTE".
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo para mostrar en formularios y listados.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Expresión regular para validar el formato del número de documento.
        /// Ejemplo NIF: ^[0-9]{8}[TRWAGMYFPDXBNJZSQVHLCKE]$
        /// Ejemplo CIF: ^[ABCDEFGHJKLMNPQRSUVW][0-9]{7}[0-9A-J]$
        /// </summary>
        public string? PatronValidacion { get; set; }

        /// <summary>
        /// Máscara de formato visual para el input de la UI (jQuery Mask Plugin o similar).
        /// Ejemplo NIF: "99999999A", CIF: "A9999999A".
        /// </summary>
        public string? MascaraFormato { get; set; }

        /// <summary>Longitud mínima del número de documento (sin separadores).</summary>
        public int? LongitudMinima { get; set; }

        /// <summary>Longitud máxima del número de documento (sin separadores).</summary>
        public int? LongitudMaxima { get; set; }

        /// <summary>
        /// Indica si este tipo de documento tiene validez fiscal en España.
        /// Los documentos fiscales (NIF, CIF, NIE) son requeridos para emitir facturas.
        /// </summary>
        public bool EsDocumentoFiscal { get; set; } = false;

        /// <summary>
        /// Descripción del tipo de documento y sus requisitos de validación.
        /// Útil como tooltip en los formularios de la interfaz.
        /// </summary>
        public string? Descripcion { get; set; }
    }
}
