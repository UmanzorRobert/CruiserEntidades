using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de evento recibido a través de un webhook desde un sistema externo.
    /// El IIntegracionService procesa el webhook según su tipo y actualiza
    /// los datos correspondientes en la base de datos.
    /// </summary>
    public enum TipoWebhook
    {
        /// <summary>Confirmación de pago procesado por la pasarela de pago (Stripe, Redsys).</summary>
        PagoConfirmado = 1,
        /// <summary>Notificación de pago fallido o rechazado por la pasarela.</summary>
        PagoFallido = 2,
        /// <summary>Devolución o reembolso procesado por la pasarela de pago.</summary>
        Devolucion = 3,
        /// <summary>Confirmación de entrega de un mensaje SMS al destinatario.</summary>
        SMSEntregado = 4,
        /// <summary>Notificación de fallo en la entrega de un mensaje SMS.</summary>
        SMSFallido = 5,
        /// <summary>Apertura de un email enviado por la empresa al destinatario.</summary>
        EmailAbierto = 6,
        /// <summary>Respuesta AEAT al envío SII de una factura (VeriFactu).</summary>
        AEATRespuestaSII = 7,
        /// <summary>Evento de firma completada desde la plataforma de firma digital.</summary>
        FirmaCompletada = 8,
        /// <summary>Evento desconocido o no categorizado recibido del sistema externo.</summary>
        Desconocido = 99
    }
}
