using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del ciclo de vida de una liquidación de comisiones de un empleado.
    /// </summary>
    public enum EstadoLiquidacionComision
    {
        /// <summary>Liquidación calculada y pendiente de revisión por administración.</summary>
        Pendiente = 1,
        /// <summary>Aprobada por administración. Lista para su pago junto con la nómina.</summary>
        Aprobada = 2,
        /// <summary>Pagada al empleado. La comisión ha sido abonada.</summary>
        Pagada = 3,
        /// <summary>Rechazada por administración. El empleado debe revisar los datos.</summary>
        Rechazada = 4
    }
}
