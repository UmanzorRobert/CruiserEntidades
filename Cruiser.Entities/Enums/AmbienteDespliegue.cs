using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Ambiente de despliegue en el que está corriendo la aplicación en Railway.
    /// </summary>
    public enum AmbienteDespliegue
    {
        /// <summary>Entorno de desarrollo local con Docker Compose.</summary>
        Development = 1,
        /// <summary>Entorno de staging en Railway (rama develop).</summary>
        Staging = 2,
        /// <summary>Entorno de producción en Railway (rama main).</summary>
        Production = 3
    }
}
