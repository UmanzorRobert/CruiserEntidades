using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de jornada laboral del empleado que define la estructura de su horario.
    /// </summary>
    public enum TipoJornada
    {
        /// <summary>Jornada completa con descanso al mediodía (mañana + tarde).</summary>
        Partida = 1,
        /// <summary>Jornada sin interrupción al mediodía. Habitual en verano o por convenio.</summary>
        Continua = 2,
        /// <summary>Solo trabaja en la franja de la mañana.</summary>
        SoloManana = 3,
        /// <summary>Solo trabaja en la franja de la tarde.</summary>
        SoloTarde = 4,
        /// <summary>Jornada nocturna (servicios de limpieza nocturnos en oficinas).</summary>
        Nocturna = 5,
        /// <summary>Horario variable o flexible según las necesidades del servicio.</summary>
        Flexible = 6
    }
}
