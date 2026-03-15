using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Nivel de severidad de una incidencia de servicio.
    /// Determina la urgencia de la respuesta y las acciones correctivas requeridas.
    /// </summary>
    public enum SeveridadIncidencia
    {
        /// <summary>Incidencia menor sin impacto económico ni reputacional significativo.</summary>
        Baja = 1,
        /// <summary>Incidencia con impacto moderado. Requiere seguimiento y respuesta al cliente.</summary>
        Media = 2,
        /// <summary>Incidencia grave con impacto económico o en la relación con el cliente.</summary>
        Alta = 3,
        /// <summary>Incidencia crítica: accidente laboral, daño grave, riesgo legal inmediato.</summary>
        Critica = 4
    }
}
