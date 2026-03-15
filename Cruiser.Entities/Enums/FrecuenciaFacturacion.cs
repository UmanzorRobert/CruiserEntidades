using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Frecuencia de generación automática de facturas en una programación recurrente.
    /// Usada en ProgramacionFacturaRecurrente para determinar el intervalo entre emisiones.
    /// </summary>
    public enum FrecuenciaFacturacion
    {
        /// <summary>Factura generada una vez a la semana.</summary>
        Semanal = 1,
        /// <summary>Factura generada una vez cada dos semanas.</summary>
        Quincenal = 2,
        /// <summary>Factura generada una vez al mes. La más habitual en contratos de limpieza.</summary>
        Mensual = 3,
        /// <summary>Factura generada una vez cada dos meses.</summary>
        Bimestral = 4,
        /// <summary>Factura generada una vez cada tres meses.</summary>
        Trimestral = 5,
        /// <summary>Factura generada una vez cada seis meses.</summary>
        Semestral = 6,
        /// <summary>Factura generada una vez al año.</summary>
        Anual = 7
    }
}
