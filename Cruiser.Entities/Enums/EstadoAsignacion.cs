using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del ciclo de vida de una asignación de productos del almacén.
    /// </summary>
    public enum EstadoAsignacion
    {
        /// <summary>Asignación creada, productos reservados pero no despachados físicamente.</summary>
        Pendiente = 1,
        /// <summary>Productos despachados físicamente desde el almacén. Stock movido.</summary>
        Despachada = 2,
        /// <summary>Asignación confirmada por el destinatario (cliente firmó albarán).</summary>
        Confirmada = 3,
        /// <summary>Asignación facturada. Se generó la factura correspondiente.</summary>
        Facturada = 4,
        /// <summary>Asignación anulada antes del despacho. Las reservas son liberadas.</summary>
        Anulada = 5
    }
}
