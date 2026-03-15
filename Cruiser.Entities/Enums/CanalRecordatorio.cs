using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Canal utilizado para enviar el recordatorio de cobranza al cliente.
    /// </summary>
    public enum CanalRecordatorio
    {
        /// <summary>Recordatorio enviado por correo electrónico mediante MailKit.</summary>
        Email = 1,
        /// <summary>Recordatorio enviado por teléfono (llamada manual del gestor de cobranzas).</summary>
        Telefono = 2,
        /// <summary>Recordatorio enviado por correo postal certificado (para documentación legal).</summary>
        Postal = 3,
        /// <summary>Recordatorio enviado por WhatsApp u otra aplicación de mensajería.</summary>
        Mensajeria = 4,
        /// <summary>Recordatorio realizado en persona durante una visita al cliente.</summary>
        Presencial = 5
    }
}
