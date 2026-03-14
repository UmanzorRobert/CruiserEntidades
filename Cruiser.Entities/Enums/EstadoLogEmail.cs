using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del proceso de envío de un email registrado en el log de emails.
    /// </summary>
    public enum EstadoLogEmail
    {
        /// <summary>Email en cola de Hangfire, pendiente de envío.</summary>
        Pendiente = 1,
        /// <summary>Email enviado exitosamente al servidor SMTP.</summary>
        Enviado = 2,
        /// <summary>Error al intentar enviar. Se reintentará según política de reintentos.</summary>
        Error = 3,
        /// <summary>Fallido definitivamente: agotados los reintentos máximos configurados.</summary>
        Fallido = 4,
        /// <summary>Email cancelado antes de ser enviado (destinatario revocó consentimiento).</summary>
        Cancelado = 5
    }
}
