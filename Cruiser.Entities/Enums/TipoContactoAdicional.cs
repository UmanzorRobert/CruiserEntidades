using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Rol o función del contacto adicional dentro de la entidad a la que pertenece.
    /// Un contacto puede tener múltiples roles (flags booleanos en la entidad).
    /// </summary>
    public enum TipoContactoAdicional
    {
        /// <summary>Contacto para temas de facturación y gestión de pagos.</summary>
        Facturacion = 1,
        /// <summary>Contacto comercial para negociaciones y pedidos.</summary>
        Comercial = 2,
        /// <summary>Contacto técnico para soporte y coordinación de servicios.</summary>
        Tecnico = 3,
        /// <summary>Contacto de dirección o gerencia de la entidad.</summary>
        Gerencia = 4,
        /// <summary>Contacto de recursos humanos.</summary>
        RRHH = 5,
        /// <summary>Otro tipo de contacto no contemplado en los anteriores.</summary>
        Otro = 99
    }
}
