using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del Equipo de Protección Individual (EPI) asignado a un empleado.
    /// </summary>
    public enum EstadoEPI
    {
        /// <summary>EPI entregado al empleado y en uso activo.</summary>
        Entregado = 1,
        /// <summary>EPI devuelto por el empleado en buen estado al finalizar la asignación.</summary>
        Devuelto = 2,
        /// <summary>EPI declarado perdido por el empleado. Puede generar cargo económico.</summary>
        Perdido = 3,
        /// <summary>EPI deteriorado o dañado. Debe ser reemplazado. Genera baja de inventario.</summary>
        Deteriorado = 4,
        /// <summary>EPI caducado (fecha de caducidad superada). No puede usarse.</summary>
        Caducado = 5
    }
}
