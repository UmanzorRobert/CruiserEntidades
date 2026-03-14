using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Clasificación ABC de productos/ubicaciones según el Principio de Pareto
    /// aplicado a inventario (valor económico × rotación de stock).
    /// Determina la frecuencia de conteo en inventarios cíclicos.
    /// </summary>
    public enum ClasificacionABC
    {
        /// <summary>
        /// Categoría A: alto valor y/o alta rotación. Representan ~80% del valor total
        /// con ~20% de los artículos. Se cuentan con mayor frecuencia (mensual).
        /// </summary>
        A = 1,
        /// <summary>
        /// Categoría B: valor y rotación medios. ~15% del valor total con ~30% de artículos.
        /// Se cuentan trimestralmente.
        /// </summary>
        B = 2,
        /// <summary>
        /// Categoría C: bajo valor y/o baja rotación. ~5% del valor total con ~50% de artículos.
        /// Se cuentan semestralmente o anualmente.
        /// </summary>
        C = 3
    }
}
