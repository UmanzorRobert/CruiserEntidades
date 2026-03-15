using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de cálculo del descuento en la entidad Descuento.
    /// Determina si el valor del descuento es un porcentaje o un importe fijo.
    /// </summary>
    public enum TipoDescuento
    {
        /// <summary>Descuento como porcentaje del precio base. Ejemplo: 10% de descuento.</summary>
        Porcentaje = 1,
        /// <summary>Descuento como importe fijo en la moneda base. Ejemplo: 5,00 € de descuento.</summary>
        ImporteFijo = 2,
        /// <summary>
        /// Precio fijo especial que sustituye completamente al precio base.
        /// Ejemplo: precio especial de 45,00 € independientemente del precio original.
        /// </summary>
        PrecioEspecial = 3
    }
}
