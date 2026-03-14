using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de relación comercial o funcional entre dos productos del catálogo.
    /// Se usa en ProductoSustituto para definir cómo se relacionan los productos.
    /// </summary>
    public enum TipoRelacionProducto
    {
        /// <summary>
        /// El producto B puede reemplazar al producto A con características similares.
        /// Se propone automáticamente cuando el producto A no tiene stock.
        /// </summary>
        Sustituto = 1,
        /// <summary>
        /// El producto B complementa al producto A (se usan juntos habitualmente).
        /// Se muestra como sugerencia de venta cruzada en la interfaz.
        /// </summary>
        Complementario = 2,
        /// <summary>
        /// El producto B es un accesorio del producto A.
        /// Ejemplo: fregona (A) → cubo de fregar (B).
        /// </summary>
        Accesorio = 3,
        /// <summary>
        /// El producto B es un pack o kit que incluye al producto A.
        /// Permite gestionar kits de limpieza compuestos por varios productos.
        /// </summary>
        ParteDePack = 4
    }
}
