using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Alcance del conteo físico en un inventario cíclico.
    /// Determina qué productos se incluyen en el proceso de conteo.
    /// </summary>
    public enum TipoConteoInventario
    {
        /// <summary>Conteo de todos los productos del almacén completo.</summary>
        Total = 1,
        /// <summary>Conteo de una categoría, zona o grupo de productos seleccionado.</summary>
        Parcial = 2,
        /// <summary>
        /// Conteo basado en clasificación ABC: primero los A (mayor valor/rotación),
        /// luego B y opcionalmente C. Metodología más eficiente para almacenes grandes.
        /// </summary>
        ABC = 3
    }
}
