using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del proceso de sincronización de operaciones offline realizadas
    /// por un empleado desde la PWA en campo sin conexión a internet.
    /// </summary>
    public enum EstadoSincronizacion
    {
        /// <summary>Operaciones offline pendientes de sincronizar con el servidor.</summary>
        Pendiente = 1,
        /// <summary>Sincronización en curso. No interrumpir.</summary>
        EnProceso = 2,
        /// <summary>Sincronización completada exitosamente. Todas las operaciones procesadas.</summary>
        Completada = 3,
        /// <summary>Sincronización completada con conflictos que requieren resolución manual.</summary>
        ConConflictos = 4,
        /// <summary>Sincronización fallida por error de red o del servidor. Reintentará automáticamente.</summary>
        Error = 5
    }
}
