using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Analitica
{
    /// <summary>
    /// Caché de valores KPI pre-calculados para el dashboard ejecutivo.
    /// Almacena los valores más recientes de los KPIs en un formato optimizado
    /// para la carga rápida del dashboard sin tener que recalcularlos en tiempo real.
    ///
    /// El dashboard Blazor Server carga los KPIs desde esta caché (ICacheService)
    /// en lugar de calcularlos on-demand para garantizar tiempos de respuesta
    /// inferiores a 200ms incluso con gran volumen de datos.
    ///
    /// Los datos del caché se invalidan automáticamente cuando:
    /// 1. El job de Hangfire recalcula el KPI y actualiza MetricaKPI.UltimoValor.
    /// 2. Se producen operaciones críticas que afectan al KPI (nueva factura, orden completada).
    ///
    /// DatosSerializados almacena el snapshot completo del KPI en JSON incluyendo
    /// el valor actual, el objetivo, la tendencia y los últimos 30 registros históricos
    /// para renderizar los mini-gráficos sparkline del dashboard.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => x.MetricaKPIId).IsUnique();
    ///   builder.HasIndex(x => x.FechaExpiracion);
    ///   builder.Property(x => x.DatosSerializados).HasColumnType("jsonb");
    ///   builder.HasIndex(x => x.DatosSerializados).HasMethod("gin");
    /// </remarks>
    public class KpiCache : EntidadBase
    {
        /// <summary>FK hacia la métrica KPI cuyos datos están cacheados. Índice único (1-a-1).</summary>
        public Guid MetricaKPIId { get; set; }

        /// <summary>Código del KPI (desnormalizado para consultas rápidas sin JOIN).</summary>
        public string CodigoKPI { get; set; } = string.Empty;

        /// <summary>Último valor calculado del KPI (desnormalizado para consultas rápidas).</summary>
        public decimal? UltimoValor { get; set; }

        /// <summary>Valor objetivo del KPI (desnormalizado).</summary>
        public decimal? ValorObjetivo { get; set; }

        /// <summary>
        /// Snapshot completo del KPI en formato JSONB para el renderizado del dashboard.
        /// Incluye: valor actual, objetivo, tendencia, variación y últimos 30 puntos históricos.
        /// Estructura:
        /// {
        ///   "valor": 94.5, "objetivo": 95.0, "tendencia": "Alza",
        ///   "variacion": 2.3, "unidad": "%",
        ///   "historial": [{"periodo": "2026-02", "valor": 92.2}, ...]
        /// }
        /// </summary>
        public string? DatosSerializados { get; set; }

        /// <summary>
        /// Fecha y hora UTC en que expira la caché y debe ser recalculada.
        /// Determinada por la FrecuenciaCalculo del KPI.
        /// </summary>
        public DateTime FechaExpiracion { get; set; }

        /// <summary>Fecha y hora UTC de la última actualización del caché.</summary>
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

        /// <summary>Número de veces que el dashboard ha leído este caché. Estadístico.</summary>
        public int NumeroLecturas { get; set; } = 0;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Métrica KPI cuyos datos están almacenados en esta caché.</summary>
        public virtual MetricaKPI? MetricaKPI { get; set; }
    }
}
