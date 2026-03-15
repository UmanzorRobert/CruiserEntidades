using System;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Analitica
{
    /// <summary>
    /// Registro histórico del valor de un KPI en un período específico.
    /// Append-only: se crea un registro cada vez que IAnalíticaService calcula
    /// el KPI y persiste el resultado para análisis de tendencias y comparativas.
    ///
    /// La granularidad del período depende de la FrecuenciaCalculo de la MetricaKPI:
    /// - Horaria: Periodo = "2026-03-13T08:00" (hora exacta)
    /// - Diaria: Periodo = "2026-03-13" (día)
    /// - Semanal: Periodo = "2026-W11" (semana ISO)
    /// - Mensual: Periodo = "2026-03" (mes)
    /// - Trimestral: Periodo = "2026-Q1" (trimestre)
    ///
    /// Este historial alimenta los gráficos de tendencia Chart.js del dashboard
    /// y los reportes de analítica KPIs (PDF y Excel) del módulo de reportes.
    ///
    /// PorcentajeCumplimiento = (Valor / ValorObjetivo) × 100.
    /// Variacion = ((Valor - ValorPeriodoAnterior) / ValorPeriodoAnterior) × 100.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   NO hereda EntidadBase — log append-only.
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.Periodo).IsRequired().HasMaxLength(20);
    ///   builder.Property(x => x.Valor).HasPrecision(18, 4);
    ///   builder.Property(x => x.ValorObjetivo).HasPrecision(18, 4);
    ///   builder.Property(x => x.PorcentajeCumplimiento).HasPrecision(10, 4);
    ///   builder.Property(x => x.Variacion).HasPrecision(10, 4);
    ///   builder.HasIndex(x => new { x.MetricaKPIId, x.Periodo }).IsUnique();
    ///   builder.HasIndex(x => new { x.MetricaKPIId, x.FechaRegistro });
    ///   builder.HasIndex(x => x.Tendencia);
    /// </remarks>
    public class HistorialKPI
    {
        /// <summary>Identificador único del registro histórico.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia la métrica KPI a la que pertenece este registro histórico.</summary>
        public Guid MetricaKPIId { get; set; }

        /// <summary>
        /// Identificador del período de cálculo según la granularidad de la frecuencia.
        /// Ejemplo: "2026-03-13", "2026-03", "2026-W11", "2026-Q1".
        /// </summary>
        public string Periodo { get; set; } = string.Empty;

        /// <summary>Valor calculado del KPI en este período.</summary>
        public decimal Valor { get; set; }

        /// <summary>Valor objetivo del KPI en este período (copiado de MetricaKPI.ValorObjetivo).</summary>
        public decimal? ValorObjetivo { get; set; }

        /// <summary>
        /// Porcentaje de cumplimiento del objetivo: (Valor / ValorObjetivo) × 100.
        /// Nulo si ValorObjetivo no está definido.
        /// </summary>
        public decimal? PorcentajeCumplimiento { get; set; }

        /// <summary>
        /// Variación porcentual respecto al período anterior del mismo KPI.
        /// Positivo = mejora respecto al período anterior.
        /// Nulo si no hay datos del período anterior.
        /// </summary>
        public decimal? Variacion { get; set; }

        /// <summary>Tendencia del KPI en este período respecto al anterior.</summary>
        public TendenciaKPI Tendencia { get; set; } = TendenciaKPI.SinDatos;

        /// <summary>Fecha y hora UTC en que se calculó y registró este valor del KPI.</summary>
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Notas sobre circunstancias especiales que explican una variación inusual del KPI.
        /// Ejemplo: "Semana de Navidad - volumen reducido", "Contrato nuevo cliente X".
        /// </summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Métrica KPI a la que pertenece este registro histórico.</summary>
        public virtual MetricaKPI? MetricaKPI { get; set; }
    }
}
