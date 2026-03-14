using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del ciclo de vida de una solicitud GDPR desde su recepción hasta su resolución.
    /// </summary>
    public enum EstadoSolicitudGDPR
    {
        /// <summary>Solicitud recibida, pendiente de revisión por el responsable de datos.</summary>
        Pendiente = 1,
        /// <summary>Solicitud en proceso de tramitación. Plazo máximo legal: 30 días (Art. 12 RGPD).</summary>
        EnProceso = 2,
        /// <summary>Solicitud completada satisfactoriamente. Se notificó al interesado.</summary>
        Completada = 3,
        /// <summary>Solicitud rechazada con motivo justificado. Se notificó al interesado.</summary>
        Rechazada = 4,
        /// <summary>Solicitud expirada sin tramitar. Superado el plazo legal de 30 días.</summary>
        Expirada = 5
    }
}
