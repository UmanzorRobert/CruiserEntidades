using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del ciclo de vida de una ausencia o vacación solicitada por el empleado.
    /// </summary>
    public enum EstadoAusencia
    {
        /// <summary>Solicitud enviada por el empleado. Pendiente de revisión del supervisor.</summary>
        Solicitada = 1,
        /// <summary>Aprobada por el supervisor. El empleado puede ausentarse en las fechas indicadas.</summary>
        Aprobada = 2,
        /// <summary>Rechazada por el supervisor con motivo. El empleado debe buscar fechas alternativas.</summary>
        Rechazada = 3,
        /// <summary>Cancelada por el propio empleado antes de la fecha de inicio.</summary>
        Cancelada = 4,
        /// <summary>Ausencia ya completada (fecha fin superada).</summary>
        Completada = 5
    }
}
