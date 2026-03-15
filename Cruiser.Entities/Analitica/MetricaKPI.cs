using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Analitica
{
    /// <summary>
    /// Definición de un indicador clave de rendimiento (KPI) del sistema.
    /// Almacena la configuración del KPI: su fórmula de cálculo, la unidad de medida,
    /// la frecuencia de recálculo y el valor objetivo establecido por la dirección.
    ///
    /// El job de Hangfire CalcularKPIs (frecuencia según FrecuenciaCalculo) invoca
    /// IAnalíticaService.CalcularYPersistirKPI para cada MetricaKPI activa,
    /// actualiza UltimoValor, FechaUltimoCalculo y TendenciaUltimos30Dias,
    /// y persiste el resultado histórico en HistorialKPI.
    ///
    /// SEED de KPIs base:
    /// - OPE001: Órdenes completadas en el período
    /// - OPE002: Tiempo medio de servicio (minutos)
    /// - OPE003: Tasa de órdenes completadas a tiempo
    /// - FIN001: Facturación total del período
    /// - FIN002: Importe cobrado vs facturado (%)
    /// - FIN003: Días de cobro promedio (DSO)
    /// - CLI001: NPS Score del período
    /// - CLI002: Tasa de retención de clientes (%)
    /// - CAL001: Puntuación media de evaluaciones
    /// - CAL002: Número de incidencias críticas
    /// - RHH001: Tasa de absentismo (%)
    /// - INV001: Rotación de stock
    /// - COM001: Tasa de conversión cotizaciones (%)
    ///
    /// La Formula almacena la clave del método de cálculo en IAnalíticaService
    /// (no una fórmula matemática inline) para mayor flexibilidad.
    /// Ejemplo: "ContarOrdenesPeriodo", "CalcularNPSPeriodo".
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(20);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.Formula).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.UnidadMedida).HasMaxLength(50);
    ///   builder.Property(x => x.UltimoValor).HasPrecision(18, 4);
    ///   builder.Property(x => x.ValorObjetivo).HasPrecision(18, 4);
    ///   builder.Property(x => x.ValorMinimoAceptable).HasPrecision(18, 4);
    ///   builder.Property(x => x.TendenciaUltimos30Dias).HasPrecision(10, 4);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    ///   builder.HasIndex(x => new { x.TipoMetrica, x.EstaActiva });
    ///   builder.HasIndex(x => x.FrecuenciaCalculo);
    /// </remarks>
    public class MetricaKPI : EntidadBase
    {
        /// <summary>
        /// Código único del KPI en formato AREA+NÚMERO.
        /// Ejemplo: "OPE001", "FIN002", "CLI001", "CAL003".
        /// Referenciado en el código sin depender del Guid.
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo del KPI para el dashboard y los reportes.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del KPI: qué mide, cómo se interpreta y por qué es relevante.</summary>
        public string? Descripcion { get; set; }

        /// <summary>Categoría de negocio a la que pertenece el KPI.</summary>
        public TipoMetricaKPI TipoMetrica { get; set; }

        /// <summary>
        /// Clave del método de cálculo en IAnalíticaService.
        /// Ejemplo: "ContarOrdenesPeriodo", "CalcularNPSPeriodo", "CalcularDSO".
        /// El servicio usa reflexión o un switch para invocar el método correcto.
        /// </summary>
        public string Formula { get; set; } = string.Empty;

        /// <summary>
        /// Unidad de medida del KPI para mostrar en el dashboard.
        /// Ejemplo: "órdenes", "minutos", "%", "€", "días", "puntos".
        /// </summary>
        public string? UnidadMedida { get; set; }

        /// <summary>Frecuencia de recálculo automático por el job de Hangfire.</summary>
        public FrecuenciaCalculoKPI FrecuenciaCalculo { get; set; } = FrecuenciaCalculoKPI.Diaria;

        // ── Valores ──────────────────────────────────────────────────────────

        /// <summary>Último valor calculado del KPI. Actualizado en cada ejecución del job.</summary>
        public decimal? UltimoValor { get; set; }

        /// <summary>
        /// Valor objetivo del KPI establecido por la dirección.
        /// Usado para calcular el porcentaje de cumplimiento en HistorialKPI.
        /// </summary>
        public decimal? ValorObjetivo { get; set; }

        /// <summary>
        /// Valor mínimo aceptable por debajo del cual se genera una AlertaVencimiento
        /// o una Notificacion de tipo Error/Crítica al responsable del área.
        /// </summary>
        public decimal? ValorMinimoAceptable { get; set; }

        /// <summary>Fecha y hora UTC del último cálculo exitoso del KPI.</summary>
        public DateTime? FechaUltimoCalculo { get; set; }

        /// <summary>
        /// Variación porcentual del KPI en los últimos 30 días respecto al mes anterior.
        /// Positivo = mejora. Negativo = empeoramiento. Calculado automáticamente.
        /// </summary>
        public decimal? TendenciaUltimos30Dias { get; set; }

        /// <summary>Tendencia de evolución: Alza, Baja, Estable o SinDatos.</summary>
        public TendenciaKPI Tendencia { get; set; } = TendenciaKPI.SinDatos;

        // ── Visualización ────────────────────────────────────────────────────

        /// <summary>
        /// Indica si el KPI está activo y se recalcula periódicamente.
        /// False para KPIs en desarrollo o temporalmente suspendidos.
        /// </summary>
        public bool EstaActiva { get; set; } = true;

        /// <summary>
        /// Indica si un valor más alto del KPI es mejor (por defecto: true).
        /// False para KPIs donde un valor menor es mejor: días de cobro, tasa de incidencias.
        /// Afecta al color semáforo en el dashboard: verde/rojo según el sentido.
        /// </summary>
        public bool MayorEsMejor { get; set; } = true;

        /// <summary>
        /// Orden de visualización en el dashboard ejecutivo.
        /// Los KPIs con menor número aparecen primero.
        /// </summary>
        public int OrdenDashboard { get; set; } = 99;

        /// <summary>Clase CSS del icono Font Awesome para el KPI en el dashboard.</summary>
        public string? Icono { get; set; }

        /// <summary>Color hexadecimal (#RRGGBB) de la tarjeta del KPI en el dashboard.</summary>
        public string? Color { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Historial de valores calculados de este KPI a lo largo del tiempo.</summary>
        public virtual ICollection<HistorialKPI> Historial { get; set; }
            = new List<HistorialKPI>();
    }
}
