using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Nivel de urgencia de una alerta de vencimiento próximo.
    /// Determina el color del semáforo visual en la interfaz.
    /// </summary>
    public enum NivelAlertaVencimiento
    {
        /// <summary>Verde: vencimiento lejano, sin acción inmediata requerida. Más de 30 días.</summary>
        Verde = 1,
        /// <summary>Amarillo: vencimiento próximo, revisar en los próximos días. Entre 15 y 30 días.</summary>
        Amarillo = 2,
        /// <summary>Naranja: vencimiento inminente, acción urgente. Entre 7 y 15 días.</summary>
        Naranja = 3,
        /// <summary>Rojo: vencimiento en menos de 7 días o ya vencido. Acción inmediata requerida.</summary>
        Rojo = 4
    }
}
