using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Objetivo principal de una campaña comercial.
    /// Determina las métricas de éxito y el contenido del mensaje.
    /// </summary>
    public enum TipoObjetivoCampana
    {
        /// <summary>Captación de nuevos clientes no registrados.</summary>
        Captacion = 1,
        /// <summary>Fidelización de clientes existentes con servicios adicionales.</summary>
        Fidelizacion = 2,
        /// <summary>Reactivación de clientes inactivos.</summary>
        Reactivacion = 3,
        /// <summary>Upselling: propuesta de servicios de mayor valor al cliente.</summary>
        Upselling = 4,
        /// <summary>Cross-selling: propuesta de servicios complementarios.</summary>
        CrossSelling = 5,
        /// <summary>Comunicación informativa (cambios de tarifas, nueva normativa, etc.).</summary>
        Informativa = 6,
        /// <summary>Encuesta de satisfacción o NPS.</summary>
        Encuesta = 7
    }
}
