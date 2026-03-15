using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Plataforma del dispositivo receptor de las push notifications.
    /// Determina el protocolo de envío: Web Push API, FCM (Android) o APNs (iOS).
    /// </summary>
    public enum PlataformaPush
    {
        /// <summary>Navegador web de escritorio o móvil (Web Push API con VAPID).</summary>
        Web = 1,
        /// <summary>Aplicación Android (Firebase Cloud Messaging - FCM).</summary>
        Android = 2,
        /// <summary>Aplicación iOS (Apple Push Notification service - APNs).</summary>
        iOS = 3
    }
}
