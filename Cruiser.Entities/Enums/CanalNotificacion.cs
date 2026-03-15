using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Canal de envío de una notificación al usuario destinatario.
    /// Una notificación puede enviarse simultáneamente por múltiples canales
    /// según la configuración del TipoNotificacion y las preferencias del usuario.
    /// </summary>
    public enum CanalNotificacion
    {
        /// <summary>Notificación interna en la campana de la interfaz web (SignalR).</summary>
        Sistema = 1,
        /// <summary>Email enviado mediante MailKit al email del usuario.</summary>
        Email = 2,
        /// <summary>Push notification en el navegador o dispositivo móvil (Service Worker PWA).</summary>
        Push = 3,
        /// <summary>Mensaje SMS al teléfono móvil del usuario (integración externa).</summary>
        SMS = 4
    }
}
