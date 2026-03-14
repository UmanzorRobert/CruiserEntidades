using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de dispositivo desde el que se registra una sesión o dispositivo confiable.
    /// </summary>
    public enum TipoDispositivo
    {
        /// <summary>Ordenador de escritorio o portátil.</summary>
        Desktop = 1,
        /// <summary>Teléfono móvil (smartphone).</summary>
        Mobile = 2,
        /// <summary>Tablet o dispositivo de pantalla intermedia.</summary>
        Tablet = 3,
        /// <summary>Dispositivo de tipo desconocido o no clasificado.</summary>
        Desconocido = 4
    }
}
