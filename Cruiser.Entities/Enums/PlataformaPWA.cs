using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Plataforma o sistema operativo desde el que se instaló o usa la PWA.
    /// </summary>
    public enum PlataformaPWA
    {
        /// <summary>Navegador web de escritorio (no instalada como app).</summary>
        Web = 1,
        /// <summary>PWA instalada en dispositivo Android (Chrome, Edge, Samsung Internet).</summary>
        Android = 2,
        /// <summary>PWA instalada en dispositivo iOS/iPadOS (Safari).</summary>
        iOS = 3,
        /// <summary>PWA instalada en Windows 10/11 (Edge).</summary>
        Windows = 4,
        /// <summary>PWA instalada en macOS (Chrome, Edge, Safari).</summary>
        macOS = 5,
        /// <summary>Plataforma no identificada.</summary>
        Desconocida = 6
    }
}
