using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    public enum TipoOperacionOffline
    {
        /// <summary>Registro de entrada o salida de un fichaje GPS.</summary>
        Fichaje = 1,
        /// <summary>Completar un ítem del checklist de una orden de servicio.</summary>
        ChecklistItem = 2,
        /// <summary>Subida de una fotografía del servicio (Antes/Durante/Después).</summary>
        FotografiaServicio = 3,
        /// <summary>Captura de la firma digital del cliente al finalizar el servicio.</summary>
        FirmaDigital = 4,
        /// <summary>Cambio de estado de una orden de servicio.</summary>
        CambioEstadoOrden = 5,
        /// <summary>Registro de materiales utilizados durante el servicio.</summary>
        MaterialUtilizado = 6,
        /// <summary>Registro de un gasto de la orden de servicio (taxi, material urgente).</summary>
        GastoOrden = 7,
        /// <summary>Completar el checklist completo de la orden de servicio.</summary>
        CompletarOrden = 8
    }
}
