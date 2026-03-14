using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Propósito o uso de una dirección registrada en el sistema.
    /// Una entidad puede tener múltiples direcciones con distintos tipos.
    /// </summary>
    public enum TipoDireccion
    {
        /// <summary>Dirección fiscal/legal registrada ante la autoridad tributaria.</summary>
        Fiscal = 1,
        /// <summary>Dirección de envío de mercancía o materiales.</summary>
        Envio = 2,
        /// <summary>Dirección del lugar donde se presta el servicio de limpieza.</summary>
        Trabajo = 3,
        /// <summary>Dirección de facturación (puede diferir de la fiscal en algunos casos).</summary>
        Facturacion = 4,
        /// <summary>Dirección particular del empleado (para RRHH y notificaciones).</summary>
        Particular = 5,
        /// <summary>Otra dirección no clasificada en los tipos anteriores.</summary>
        Otro = 99
    }
}
