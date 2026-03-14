using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado de procesamiento de una tarea en la cola de procesamiento asíncrono.
    /// </summary>
    public enum EstadoColaProcesamiento
    {
        /// <summary>Tarea creada y esperando ser recogida por el procesador.</summary>
        Pendiente = 1,
        /// <summary>Tarea actualmente siendo procesada por un worker de Hangfire.</summary>
        EnProceso = 2,
        /// <summary>Tarea completada exitosamente.</summary>
        Completada = 3,
        /// <summary>Tarea fallida tras agotar el número máximo de reintentos.</summary>
        Fallida = 4,
        /// <summary>Tarea cancelada manualmente antes de ser procesada.</summary>
        Cancelada = 5,
        /// <summary>Tarea en espera de reintento tras un fallo temporal.</summary>
        EsperandoReintento = 6
    }
}
