using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.RRHH
{
    /// <summary>
    /// Política interna de RRHH que define las reglas de gestión del personal
    /// en áreas como vacaciones, horas extra, ausencias, comisiones o disciplina.
    ///
    /// Las reglas se almacenan en ReglasJSON (JSONB con índice GIN) como un objeto
    /// estructurado que IOrdenServicioService y IAusenciaService interpretan
    /// dinámicamente al evaluar solicitudes del empleado.
    ///
    /// Solo puede haber una política activa por TipoPolitica.
    /// Al activar una nueva versión, la anterior se desactiva automáticamente.
    ///
    /// ESTRUCTURA DE REGLASJSON (ejemplo para Vacaciones):
    /// {
    ///   "diasMinimoPreaviso": 15,
    ///   "maxDiasConsecutivos": 30,
    ///   "periodoDisfrute": { "inicio": "06-01", "fin": "09-30" },
    ///   "diasMaximoCongelados": 5,
    ///   "requiereAprobacionSupervisor": true
    /// }
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.ReglasJSON).HasColumnType("jsonb");
    ///   builder.HasIndex(x => new { x.TipoPolitica, x.EsActiva }).HasFilter("\"EsActiva\" = true");
    ///   builder.HasIndex(x => x.ReglasJSON).HasMethod("gin");
    /// </remarks>
    public class PoliticaRRHH : EntidadBase
    {
        /// <summary>Tipo de política de RRHH que define el área de aplicación.</summary>
        public TipoPoliticaRRHH TipoPolitica { get; set; }

        /// <summary>Nombre descriptivo de la versión de la política.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción general de la política y su fundamento (convenio, ley, acuerdo).</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Reglas de la política en formato JSONB.
        /// La estructura varía según el TipoPolitica.
        /// Interpretadas dinámicamente por los servicios de negocio.
        /// </summary>
        public string? ReglasJSON { get; set; }

        /// <summary>
        /// Fecha a partir de la cual esta versión de la política está vigente.
        /// </summary>
        public DateOnly FechaVigencia { get; set; }

        /// <summary>
        /// Indica si esta es la versión activa de la política para su tipo.
        /// Solo puede haber una política activa por TipoPolitica.
        /// </summary>
        public bool EsActiva { get; set; } = true;

        /// <summary>FK hacia el empleado de dirección o RRHH que aprobó la política.</summary>
        public Guid? AprobadaPorId { get; set; }

        /// <summary>Referencia legal que fundamenta la política (artículo de ley, convenio colectivo).</summary>
        public string? ReferenciaLegal { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────
        // Sin navegación directa para mantener la entidad desacoplada de otras capas.
    }
}
