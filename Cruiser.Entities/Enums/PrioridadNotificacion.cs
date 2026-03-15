using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Nivel de prioridad de una notificación que determina su presentación visual,
    /// el sonido de alerta y el tiempo de auto-ocultado en la interfaz.
    /// </summary>
    public enum PrioridadNotificacion
    {
        /// <summary>Notificación informativa sin urgencia. Auto-ocultar en 5 segundos.</summary>
        Info = 1,
        /// <summary>Notificación de éxito de una operación. Auto-ocultar en 4 segundos.</summary>
        Exito = 2,
        /// <summary>Advertencia que requiere atención pero no es crítica. Auto-ocultar en 8 segundos.</summary>
        Advertencia = 3,
        /// <summary>Error o situación crítica que requiere acción inmediata. Persistente hasta que el usuario la cierra.</summary>
        Error = 4,
        /// <summary>Alerta de máxima urgencia (stock agotado, ITV vencida). Suena y persiste.</summary>
        Critica = 5
    }
}
