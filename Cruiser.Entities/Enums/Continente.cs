using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Continente geográfico al que pertenece un país.
    /// Se utiliza para agrupar y filtrar países en catálogos y reportes.
    /// </summary>
    public enum Continente
    {
        /// <summary>Europa.</summary>
        Europa = 1,
        /// <summary>Asia.</summary>
        Asia = 2,
        /// <summary>África.</summary>
        Africa = 3,
        /// <summary>América del Norte (incluye Centroamérica y Caribe).</summary>
        AmericaNorte = 4,
        /// <summary>América del Sur.</summary>
        AmericaSur = 5,
        /// <summary>Oceanía (incluye Australia y Pacífico).</summary>
        Oceania = 6,
        /// <summary>Antártida.</summary>
        Antartida = 7
    }
}
