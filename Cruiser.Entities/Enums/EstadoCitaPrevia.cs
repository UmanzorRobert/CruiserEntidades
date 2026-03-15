using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del ciclo de vida de una cita previa solicitada por un cliente.
    /// </summary>
    public enum EstadoCitaPrevia
    {
        /// <summary>Solicitud recibida. Pendiente de revisión y confirmación por el equipo.</summary>
        Pendiente = 1,
        /// <summary>Cita confirmada por el equipo. Se ha reservado el horario del empleado.</summary>
        Confirmada = 2,
        /// <summary>Cita rechazada. Se notifica al cliente con el motivo del rechazo.</summary>
        Rechazada = 3,
        /// <summary>Cita completada. El servicio se realizó en la fecha y hora acordadas.</summary>
        Completada = 4,
        /// <summary>Cita cancelada por el cliente o por la empresa antes de realizarse.</summary>
        Cancelada = 5
    }
}
