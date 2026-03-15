using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.RRHH
{
    /// <summary>
    /// Festivo laboral que afecta a la disponibilidad de empleados y a la planificación
    /// de órdenes de servicio en una fecha determinada.
    ///
    /// Los festivos nacionales y autonómicos se importan desde el seed de datos,
    /// mientras que los festivos locales y de empresa se gestionan manualmente.
    ///
    /// La integración con ICalendarioService.VerificarConflictosHorario consulta
    /// los festivos al validar la disponibilidad de empleados para nuevas órdenes.
    ///
    /// Los festivos con EsRecurrente=true se replican automáticamente para el
    /// siguiente ejercicio mediante el job de Hangfire de inicio de año.
    ///
    /// SEED: Festivos nacionales españoles, festivos de comunidades autónomas principales,
    ///       festivos de Canarias (régimen especial IGIC).
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150);
    ///   builder.HasIndex(x => new { x.Fecha, x.PaisId });
    ///   builder.HasIndex(x => new { x.Fecha, x.ProvinciaId }).HasFilter("\"ProvinciaId\" IS NOT NULL");
    ///   builder.HasIndex(x => new { x.Fecha, x.CiudadId }).HasFilter("\"CiudadId\" IS NOT NULL");
    /// </remarks>
    public class FestivoLaboral : EntidadBase
    {
        /// <summary>Nombre descriptivo del festivo. Ejemplo: "Día de la Hispanidad", "Corpus Christi".</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Fecha del festivo laboral.</summary>
        public DateOnly Fecha { get; set; }

        /// <summary>
        /// Tipo de festivo según el ámbito geográfico de aplicación.
        /// Nacional: aplica a toda España. Autonomico: solo a la comunidad autónoma.
        /// Local: solo al municipio. Empresa: festivo interno de la empresa.
        /// </summary>
        public string TipoFestivo { get; set; } = "Nacional";

        /// <summary>FK hacia el país al que aplica el festivo.</summary>
        public Guid PaisId { get; set; }

        /// <summary>
        /// FK hacia la provincia/comunidad autónoma al que aplica.
        /// Nulo para festivos nacionales que aplican a todo el país.
        /// </summary>
        public Guid? ProvinciaId { get; set; }

        /// <summary>
        /// FK hacia el municipio al que aplica el festivo local.
        /// Nulo para festivos nacionales y autonómicos.
        /// </summary>
        public Guid? CiudadId { get; set; }

        /// <summary>
        /// Indica si el festivo se repite en la misma fecha cada año.
        /// True para festivos fijos (Navidad 25/12, Año Nuevo 1/1).
        /// False para festivos móviles (Semana Santa, Corpus Christi).
        /// </summary>
        public bool EsRecurrente { get; set; } = true;

        /// <summary>Descripción adicional sobre el festivo y su origen histórico o religioso.</summary>
        public string? Descripcion { get; set; }
    }
}
