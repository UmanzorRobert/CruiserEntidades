using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Resultado o desenlace de una interacción comercial con el cliente.
    /// Permite medir la efectividad de las gestiones comerciales del equipo.
    /// </summary>
    public enum ResultadoInteraccion
    {
        /// <summary>Interacción positiva. Cliente interesado en contratar o renovar.</summary>
        Positivo = 1,
        /// <summary>Interacción neutral. Sin avance ni retroceso en la relación comercial.</summary>
        Neutral = 2,
        /// <summary>Interacción negativa. Cliente desinteresado, queja o amenaza de baja.</summary>
        Negativo = 3,
        /// <summary>Venta o contratación cerrada como resultado directo de la interacción.</summary>
        VentaCerrada = 4,
        /// <summary>No se pudo contactar con el cliente. Pendiente de reintento.</summary>
        SinContacto = 5,
        /// <summary>Cliente solicitó información adicional. Pendiente de enviar propuesta.</summary>
        SolicitaInformacion = 6
    }
}
