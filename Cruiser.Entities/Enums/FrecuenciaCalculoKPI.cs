using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Frecuencia con la que se recalcula y persiste el valor de un KPI.
    /// Determina la programación del job de Hangfire que ejecuta IAnalíticaService.CalcularKPI.
    /// </summary>
    public enum FrecuenciaCalculoKPI
    {
        /// <summary>KPI recalculado cada hora (valores muy volátiles: pedidos en curso, stock).</summary>
        Horaria = 1,
        /// <summary>KPI recalculado una vez al día (la mayoría de KPIs operativos).</summary>
        Diaria = 2,
        /// <summary>KPI recalculado una vez por semana (tendencias, RFM, segmentación).</summary>
        Semanal = 3,
        /// <summary>KPI recalculado una vez por mes (nóminas, comisiones, rentabilidad mensual).</summary>
        Mensual = 4,
        /// <summary>KPI recalculado cada trimestre (análisis estratégico, evolución anual).</summary>
        Trimestral = 5
    }
}
