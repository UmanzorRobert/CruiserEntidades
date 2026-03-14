using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de atributo diferenciador de un producto.
    /// Determina el widget de selección en la interfaz y la validación del valor.
    /// </summary>
    public enum TipoAtributoProducto
    {
        /// <summary>Color representado por un código hexadecimal (#RRGGBB). Muestra color picker.</summary>
        Color = 1,
        /// <summary>Talla o tamaño (XS, S, M, L, XL, XXL o numérico).</summary>
        Talla = 2,
        /// <summary>Material o composición del producto (algodón, acero, plástico, etc.).</summary>
        Material = 3,
        /// <summary>Capacidad o volumen (litros, kg, ml, unidades).</summary>
        Capacidad = 4,
        /// <summary>Fragancia o aroma del producto (para productos de limpieza).</summary>
        Aroma = 5,
        /// <summary>Concentración o dilución del producto químico.</summary>
        Concentracion = 6,
        /// <summary>Presentación comercial (spray, gel, polvo, pastilla, rollo).</summary>
        Presentacion = 7,
        /// <summary>Otro tipo de atributo personalizado no contemplado en los anteriores.</summary>
        Otro = 99
    }
}
