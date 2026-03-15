using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>Tipo de siniestro o incidente sufrido por un vehículo de la flota.</summary>
    public enum TipoSiniestro
    {
        /// <summary>Accidente de tráfico con otro vehículo o colisión con obstáculo.</summary>
        Accidente = 1,
        /// <summary>Robo del vehículo completo.</summary>
        Robo = 2,
        /// <summary>Robo o intento de robo del interior del vehículo.</summary>
        RoboInterior = 3,
        /// <summary>Daños por vandalismo (cristales rotos, rayadas, etc.).</summary>
        Vandalismo = 4,
        /// <summary>Daños por fenómeno meteorológico (granizo, inundación, viento).</summary>
        FenomenoMeteorologico = 5,
        /// <summary>Incendio del vehículo.</summary>
        Incendio = 6,
        /// <summary>Otro tipo de siniestro no categorizado.</summary>
        Otro = 7
    }
}
