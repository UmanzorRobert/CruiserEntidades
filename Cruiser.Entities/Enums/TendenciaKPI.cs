using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tendencia de evolución de un KPI respecto al período anterior.
    /// Calculada automáticamente por IAnalíticaService al registrar el nuevo valor.
    /// Determina el indicador visual (flecha arriba/abajo/horizontal) en el dashboard.
    /// </summary>
    public enum TendenciaKPI
    {
        /// <summary>El valor del KPI está mejorando respecto al período anterior.</summary>
        Alza = 1,
        /// <summary>El valor del KPI está empeorando respecto al período anterior.</summary>
        Baja = 2,
        /// <summary>El valor del KPI se mantiene estable sin variación significativa.</summary>
        Estable = 3,
        /// <summary>No hay datos del período anterior para calcular la tendencia.</summary>
        SinDatos = 4
    }
}
