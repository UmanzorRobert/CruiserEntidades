using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Clasificación funcional de una zona postal para la planificación logística
    /// y el cálculo de tarifas de envío y desplazamiento.
    /// </summary>
    public enum TipoZonaPostal
    {
        /// <summary>Zona urbana de alta densidad (capital de provincia, ciudad grande).</summary>
        Urbana = 1,
        /// <summary>Zona semiurbana (municipios medianos, extrarradio urbano).</summary>
        Semiurbana = 2,
        /// <summary>Zona rural (pueblos pequeños, áreas de baja densidad).</summary>
        Rural = 3,
        /// <summary>Zona industrial (polígonos, parques empresariales, naves).</summary>
        Industrial = 4,
        /// <summary>Zona costera o turística con alta estacionalidad.</summary>
        CosteraTuristica = 5,
        /// <summary>Zona insular con condiciones especiales de acceso (Canarias, Baleares).</summary>
        Insular = 6
    }
}
