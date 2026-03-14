using Cruiser.Entities.Base;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Catálogo de todos los campos JSONB del sistema con su configuración de índices GIN.
    /// Sirve como documentación técnica activa y como referencia para el módulo de
    /// administración que muestra qué campos JSON tiene el sistema y cuáles están indexados.
    ///
    /// Se pobla en el seed inicial con todos los campos JSONB definidos en las entidades.
    /// Permite al administrador técnico entender la estructura de datos JSON del sistema.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NombreTabla).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.NombreCampo).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.EstructuraEjemplo).HasColumnType("jsonb");
    ///   builder.HasIndex(x => new { x.NombreTabla, x.NombreCampo }).IsUnique();
    /// </remarks>
    public class ConfiguracionJsonbCampo : EntidadBase
    {
        /// <summary>Nombre de la tabla/entidad que contiene el campo JSONB.</summary>
        public string NombreTabla { get; set; } = string.Empty;

        /// <summary>Nombre del campo/columna JSONB en la tabla.</summary>
        public string NombreCampo { get; set; } = string.Empty;

        /// <summary>
        /// Indica si este campo JSONB tiene configurado un índice GIN en PostgreSQL.
        /// Los índices GIN permiten búsquedas eficientes dentro del contenido JSON.
        /// </summary>
        public bool TieneIndiceGIN { get; set; } = false;

        /// <summary>
        /// Ejemplo de la estructura JSON esperada en este campo.
        /// Almacenado en JSONB para poder validar y mostrar en la UI de administración.
        /// </summary>
        public string? EstructuraEjemplo { get; set; }

        /// <summary>
        /// Descripción del propósito y contenido del campo JSONB.
        /// Documenta qué información se almacena y cómo se usa.
        /// </summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Indica si el campo JSONB almacena datos personales sujetos a GDPR.
        /// Los campos con DatosPersonalesGDPR=true deben ser considerados en
        /// las operaciones de anonimización (RegistroAnonimizacionGDPR).
        /// </summary>
        public bool DatosPersonalesGDPR { get; set; } = false;
    }
}
