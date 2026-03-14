using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del ciclo de vida de un movimiento de inventario.
    /// Los movimientos completados afectan al stock en tiempo real.
    /// </summary>
    public enum EstadoMovimiento
    {
        /// <summary>Movimiento creado pero pendiente de revisión o autorización.</summary>
        Pendiente = 1,
        /// <summary>Movimiento autorizado y que ha afectado al stock del sistema.</summary>
        Completado = 2,
        /// <summary>Movimiento rechazado. No afecta al stock. Requiere nuevo movimiento.</summary>
        Rechazado = 3,
        /// <summary>
        /// Movimiento anulado o revertido mediante un movimiento compensatorio.
        /// El stock queda en el estado anterior al movimiento original.
        /// </summary>
        Reversado = 4
    }
}
