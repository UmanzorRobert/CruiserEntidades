using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Modo en que se completó una orden de servicio o un fichaje.
    /// Permite distinguir las operaciones realizadas con conexión a internet
    /// de las sincronizadas posteriormente desde el modo offline de la PWA.
    /// </summary>
    public enum ModoCompleto
    {
        /// <summary>Operación completada con conexión activa al servidor.</summary>
        Online = 1,
        /// <summary>Operación completada sin conexión y sincronizada posteriormente.</summary>
        Offline = 2
    }
}
