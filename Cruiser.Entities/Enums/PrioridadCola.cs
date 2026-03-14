using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Nivel de prioridad de una tarea en la cola de procesamiento.
    /// Las tareas con mayor prioridad se procesan antes.
    /// </summary>
    public enum PrioridadCola
    {
        /// <summary>Prioridad crítica: emails de seguridad, alertas de sistema. Se procesa inmediatamente.</summary>
        Critica = 1,
        /// <summary>Prioridad alta: emails de facturas, notificaciones importantes.</summary>
        Alta = 2,
        /// <summary>Prioridad normal: notificaciones estándar, reportes bajo demanda.</summary>
        Normal = 3,
        /// <summary>Prioridad baja: emails de marketing, cálculos analíticos periódicos.</summary>
        Baja = 4
    }
}
