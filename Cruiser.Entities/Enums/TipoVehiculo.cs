using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>Tipo de vehículo de la flota de empresa.</summary>
    public enum TipoVehiculo
    {
        /// <summary>Furgoneta de reparto o transporte de personal y material.</summary>
        Furgoneta = 1,
        /// <summary>Camión para transporte de maquinaria de limpieza pesada.</summary>
        Camion = 2,
        /// <summary>Turismo o berlina para desplazamiento de supervisores.</summary>
        Turismo = 3,
        /// <summary>Furgón pequeño para rutas urbanas.</summary>
        Furgon = 4,
        /// <summary>Motocicleta para supervisores en entornos urbanos.</summary>
        Motocicleta = 5,
        /// <summary>Vehículo eléctrico de cualquier tipo.</summary>
        Electrico = 6,
        /// <summary>Maquinaria de limpieza autopropulsada (fregadora industrial, barredora).</summary>
        MaquinariaLimpieza = 7
    }
}
