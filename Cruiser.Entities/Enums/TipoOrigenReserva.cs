using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de entidad u operación que originó una reserva de stock.
    /// Determina qué módulo gestionó la reserva y qué proceso debe liberarla.
    /// </summary>
    public enum TipoOrigenReserva
    {
        /// <summary>Stock reservado para una asignación de producto a cliente pendiente de despachar.</summary>
        AsignacionProducto = 1,
        /// <summary>Stock reservado para una orden de servicio que requiere materiales específicos.</summary>
        OrdenServicio = 2,
        /// <summary>Stock reservado para una cotización de servicio enviada al cliente.</summary>
        CotizacionServicio = 3,
        /// <summary>Reserva manual realizada por un administrador de almacén.</summary>
        ReservaManual = 4,
        /// <summary>Stock reservado para la preparación de un kit o pack de productos.</summary>
        Pack = 5
    }
}
