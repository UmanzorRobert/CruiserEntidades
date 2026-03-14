using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de solicitud GDPR que puede realizar un interesado sobre sus datos personales.
    /// Basado en los derechos reconocidos en el Capítulo III del RGPD.
    /// </summary>
    public enum TipoSolicitudGDPR
    {
        /// <summary>Art. 20 – Derecho a la portabilidad: exportar todos sus datos en formato legible.</summary>
        Exportacion = 1,
        /// <summary>Art. 17 – Derecho al olvido: anonimización de datos personales (nunca hard-delete).</summary>
        Anonimizacion = 2,
        /// <summary>Art. 16 – Derecho de rectificación: corrección de datos incorrectos.</summary>
        Rectificacion = 3,
        /// <summary>Art. 18 – Derecho de limitación del tratamiento.</summary>
        LimitacionTratamiento = 4,
        /// <summary>Art. 15 – Derecho de acceso: obtener confirmación de qué datos se tratan.</summary>
        Acceso = 5
    }
}
