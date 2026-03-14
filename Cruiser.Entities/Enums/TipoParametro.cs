using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de dato del valor almacenado en un ParametroSistema.
    /// Controla la validación y el widget de edición en la interfaz de administración.
    /// </summary>
    public enum TipoParametro
    {
        /// <summary>Valor de texto libre. Se edita con un input text.</summary>
        String = 1,
        /// <summary>Valor entero. Se valida que sea un número sin decimales.</summary>
        Integer = 2,
        /// <summary>Valor decimal. Se valida que sea un número con posibles decimales.</summary>
        Decimal = 3,
        /// <summary>Valor booleano (true/false). Se edita con un toggle/checkbox.</summary>
        Boolean = 4,
        /// <summary>Valor JSON estructurado. Se edita con un editor JSON en la UI.</summary>
        Json = 5,
        /// <summary>Valor de fecha. Se edita con un datepicker.</summary>
        Fecha = 6,
        /// <summary>Valor de color hexadecimal. Se edita con un color picker.</summary>
        Color = 7
    }
}
