using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de lista de precios según el segmento de cliente al que aplica.
    /// Determina la prioridad de selección cuando un cliente tiene múltiples listas aplicables.
    /// </summary>
    public enum TipoListaPrecio
    {
        /// <summary>Lista de precios general aplicable a todos los clientes por defecto.</summary>
        General = 1,
        /// <summary>Lista de precios especial por segmento de cliente (VIP, Estándar, etc.).</summary>
        PorSegmento = 2,
        /// <summary>Lista de precios personalizada pactada con un cliente específico.</summary>
        PorCliente = 3,
        /// <summary>Lista de precios por volumen (descuento por cantidad).</summary>
        PorVolumen = 4,
        /// <summary>Lista de precios promocional con vigencia limitada (campañas comerciales).</summary>
        Promocional = 5,
        /// <summary>Lista de precios de coste (precio de compra + margen mínimo), uso interno.</summary>
        Coste = 6
    }
}
