using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Canal o modalidad de una interacción comercial registrada entre
    /// un empleado y un cliente en el CRM del sistema.
    /// </summary>
    public enum TipoInteraccionComercial
    {
        /// <summary>Llamada telefónica entrante o saliente.</summary>
        Llamada = 1,
        /// <summary>Correo electrónico enviado o recibido.</summary>
        Email = 2,
        /// <summary>Reunión presencial o videoconferencia.</summary>
        Reunion = 3,
        /// <summary>Visita comercial en las instalaciones del cliente.</summary>
        Visita = 4,
        /// <summary>Mensaje por WhatsApp u otra aplicación de mensajería.</summary>
        Mensajeria = 5,
        /// <summary>Propuesta comercial o cotización presentada.</summary>
        Propuesta = 6,
        /// <summary>Seguimiento post-servicio o encuesta de satisfacción.</summary>
        Seguimiento = 7,
        /// <summary>Otra modalidad de interacción no contemplada.</summary>
        Otro = 99
    }
}
