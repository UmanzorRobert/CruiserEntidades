using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estrategia de caché del Service Worker para recursos de la PWA.
    /// </summary>
    public enum EstrategiaCache
    {
        /// <summary>Intenta red primero; si falla, usa caché. Ideal para llamadas a la API.</summary>
        NetworkFirst = 1,
        /// <summary>Usa caché primero; si no existe, va a red. Ideal para assets estáticos.</summary>
        CacheFirst = 2,
        /// <summary>Solo usa caché. Sin red en ningún caso. Para recursos completamente estáticos.</summary>
        CacheOnly = 3,
        /// <summary>Solo usa red. Sin caché en ningún caso. Para datos siempre actualizados.</summary>
        NetworkOnly = 4,
        /// <summary>Responde con caché inmediatamente y actualiza en segundo plano.</summary>
        StaleWhileRevalidate = 5
    }
}
