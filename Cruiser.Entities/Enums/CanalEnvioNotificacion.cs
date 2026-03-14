using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Canal a través del cual se envía una notificación al usuario destinatario.
    /// </summary>
    public enum CanalEnvioNotificacion
    {
        /// <summary>Notificación en pantalla mediante SignalR (campana + panel desplegable).</summary>
        Sistema = 1,
        /// <summary>Correo electrónico mediante MailKit SMTP.</summary>
        Email = 2,
        /// <summary>Notificación push del navegador mediante Service Worker (PWA).</summary>
        Push = 3,
        /// <summary>Mensaje SMS (requiere integración con proveedor SMS externo).</summary>
        SMS = 4
    }
}
