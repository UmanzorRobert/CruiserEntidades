using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Nivel de severidad o importancia de una entrada en la bitácora del sistema.
    /// </summary>
    public enum NivelBitacora
    {
        /// <summary>Información general sobre operaciones normales del sistema.</summary>
        Informacion = 1,

        /// <summary>Advertencia sobre situaciones inusuales que no impiden la operación.</summary>
        Advertencia = 2,

        /// <summary>Error que impidió completar una operación.</summary>
        Error = 3,

        /// <summary>Error crítico que afecta la estabilidad o seguridad del sistema.</summary>
        Critico = 4,

        /// <summary>Evento de seguridad relevante (logins, cambios de permisos, bloqueos).</summary>
        Seguridad = 5,

        /// <summary>Operación relacionada con GDPR (consentimientos, anonimizaciones, exportaciones).</summary>
        GDPR = 6
    }
}
