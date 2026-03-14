using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Técnica de anonimización aplicada sobre los datos personales del interesado.
    /// </summary>
    public enum MetodoAnonimizacion
    {
        /// <summary>
        /// Sustitución de valores personales por un token pseudónimo.
        /// Ejemplo: Nombre → "GDPR_DEL_a3f9b2c1", Email → "gdpr_a3f9b2c1@noreply.gdpr".
        /// Método principal del sistema CruiserCat.
        /// </summary>
        Pseudonimizacion = 1,
        /// <summary>Eliminación de caracteres intermedios manteniendo estructura del campo.</summary>
        Enmascaramiento = 2,
        /// <summary>Sustitución por valores ficticios genéricos sin relación con el original.</summary>
        Generalizacion = 3
    }
}
