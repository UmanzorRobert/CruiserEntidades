using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>Estado del ciclo de vida de la nómina mensual de un empleado.</summary>
    public enum EstadoNomina
    {
        /// <summary>Nómina generada automáticamente o manualmente. Pendiente de revisión.</summary>
        Borrador = 1,
        /// <summary>Nómina cerrada y bloqueada. No puede modificarse. Lista para pago.</summary>
        Cerrada = 2,
        /// <summary>Nómina pagada. El importe ha sido transferido al empleado.</summary>
        Pagada = 3
    }
}
