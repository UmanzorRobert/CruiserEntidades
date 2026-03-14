using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Naturaleza o dirección del flujo de mercancía en un tipo de movimiento de inventario.
    /// Determina si el movimiento aumenta, disminuye o no afecta al stock disponible.
    /// </summary>
    public enum NaturalezaMovimiento
    {
        /// <summary>El movimiento incrementa el stock disponible en el almacén destino.</summary>
        Entrada = 1,
        /// <summary>El movimiento decrementa el stock disponible en el almacén origen.</summary>
        Salida = 2,
        /// <summary>
        /// El movimiento implica tanto entrada como salida (transferencia entre almacenes).
        /// Se generan dos registros: uno de salida y uno de entrada relacionados.
        /// </summary>
        Mixto = 3
    }
}
