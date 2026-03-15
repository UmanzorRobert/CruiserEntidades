using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del proceso de aprobación de un documento en el flujo de AprobacionDocumento.
    /// </summary>
    public enum EstadoAprobacion
    {
        /// <summary>Solicitud de aprobación creada y pendiente de revisión por el aprobador.</summary>
        Pendiente = 1,
        /// <summary>Documento aprobado. Se desbloquean las acciones dependientes de la aprobación.</summary>
        Aprobado = 2,
        /// <summary>Documento rechazado con motivo. El solicitante debe corregir y reenviar.</summary>
        Rechazado = 3,
        /// <summary>
        /// Aprobación delegada a otro usuario (vacaciones, ausencia del aprobador original).
        /// La responsabilidad pasa al DelegadoAId.
        /// </summary>
        Delegado = 4,
        /// <summary>
        /// Solicitud de aprobación cancelada por el solicitante antes de ser procesada.
        /// </summary>
        Cancelado = 5
    }
}
