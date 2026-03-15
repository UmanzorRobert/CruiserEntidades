using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Frecuencia de ejecución automática de un reporte guardado.
    /// Determina la programación del job de Hangfire que genera y envía el reporte.
    /// </summary>
    public enum PeriodicidadReporte
    {
        /// <summary>Ejecución manual bajo demanda. No se programa automáticamente.</summary>
        Puntual = 1,
        /// <summary>Ejecución automática diaria a la hora configurada.</summary>
        Diaria = 2,
        /// <summary>Ejecución automática semanal el día y hora configurados.</summary>
        Semanal = 3,
        /// <summary>Ejecución automática mensual el día del mes configurado.</summary>
        Mensual = 4,
        /// <summary>Ejecución automática trimestral al inicio de cada trimestre.</summary>
        Trimestral = 5,
        /// <summary>Ejecución automática anual al inicio de cada año.</summary>
        Anual = 6
    }
}
