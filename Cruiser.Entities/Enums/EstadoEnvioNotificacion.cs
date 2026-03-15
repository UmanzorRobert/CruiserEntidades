using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del envío de una notificación por un canal específico.
    /// Cada canal (Email, Push, SMS) tiene su propio estado de envío en NotificacionUsuario.
    /// </summary>
    public enum EstadoEnvioNotificacion
    {
        /// <summary>Pendiente de envío. No procesada aún por el job de Hangfire.</summary>
        Pendiente = 1,
        /// <summary>Enviada correctamente por el canal correspondiente.</summary>
        Enviada = 2,
        /// <summary>Error al enviar. Se reintentará según la política de reintentos de Hangfire.</summary>
        Error = 3,
        /// <summary>No aplica para este usuario (sin token push, sin email configurado, etc.).</summary>
        NoAplica = 4
    }
}
