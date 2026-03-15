using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Canal a través del cual el cliente solicitó la cita previa.
    /// Permite analizar qué canales de contacto son más efectivos.
    /// </summary>
    public enum CanalSolicitudCita
    {
        /// <summary>Solicitud realizada desde el portal web del cliente.</summary>
        PortalWeb = 1,
        /// <summary>Solicitud recibida por llamada telefónica y registrada manualmente.</summary>
        Telefono = 2,
        /// <summary>Solicitud recibida por email y registrada manualmente.</summary>
        Email = 3,
        /// <summary>Solicitud recibida por WhatsApp u otra mensajería.</summary>
        Mensajeria = 4,
        /// <summary>Solicitud registrada directamente en la aplicación de gestión.</summary>
        Presencial = 5
    }
}
