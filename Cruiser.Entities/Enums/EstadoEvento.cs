using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del ciclo de vida de un evento en el calendario del sistema.
    /// Determina la representación visual y las acciones disponibles en FullCalendar.
    /// </summary>
    public enum EstadoEvento
    {
        /// <summary>Evento planificado y confirmado. Aparece en color normal en el calendario.</summary>
        Planificado = 1,
        /// <summary>Evento en curso actualmente. Aparece resaltado en el calendario.</summary>
        EnCurso = 2,
        /// <summary>Evento completado satisfactoriamente.</summary>
        Completado = 3,
        /// <summary>Evento cancelado. Aparece tachado o en gris en el calendario.</summary>
        Cancelado = 4,
        /// <summary>Evento reprogramado a otra fecha. El evento original queda cancelado.</summary>
        Reprogramado = 5,
        /// <summary>Evento pendiente de confirmación por alguna de las partes.</summary>
        PendienteConfirmacion = 6
    }
}
