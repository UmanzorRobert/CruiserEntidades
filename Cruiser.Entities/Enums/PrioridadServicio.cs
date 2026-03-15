using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Nivel de prioridad de una orden de servicio.
    /// Determina el orden de atención cuando hay conflicto de recursos
    /// y la urgencia comunicada al equipo asignado.
    /// </summary>
    public enum PrioridadServicio
    {
        /// <summary>Prioridad baja. Se atiende cuando haya disponibilidad de equipo.</summary>
        Baja = 1,
        /// <summary>Prioridad normal. Orden estándar según la planificación.</summary>
        Normal = 2,
        /// <summary>Prioridad alta. Se atiende antes que las órdenes normales.</summary>
        Alta = 3,
        /// <summary>Urgente. Requiere atención inmediata. Notificación push al equipo.</summary>
        Urgente = 4
    }
}
