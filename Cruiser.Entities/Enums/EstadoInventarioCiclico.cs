using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del ciclo de vida de un proceso de inventario cíclico.
    /// </summary>
    public enum EstadoInventarioCiclico
    {
        /// <summary>Inventario planificado pero no iniciado. Productos asignados pero sin contar.</summary>
        Planificado = 1,
        /// <summary>Inventario en curso. Empleados están realizando el conteo físico.</summary>
        EnProgreso = 2,
        /// <summary>
        /// Conteo físico completado. Pendiente de revisión de diferencias
        /// y generación de ajustes de stock.
        /// </summary>
        PendienteRevision = 3,
        /// <summary>Diferencias revisadas y ajustes de stock generados. Proceso completado.</summary>
        Completado = 4,
        /// <summary>Inventario cancelado antes de completarse. No se generan ajustes.</summary>
        Cancelado = 5
    }
}
