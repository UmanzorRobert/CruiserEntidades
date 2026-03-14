using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de operación registrada en la auditoría de cambios por campo.
    /// </summary>
    public enum TipoOperacionAuditoria
    {
        /// <summary>Inserción de un nuevo registro.</summary>
        Insercion = 1,

        /// <summary>Actualización de uno o más campos de un registro existente.</summary>
        Actualizacion = 2,

        /// <summary>Eliminación lógica (soft-delete) del registro.</summary>
        EliminacionLogica = 3,

        /// <summary>Restauración de un registro previamente eliminado de forma lógica.</summary>
        Restauracion = 4,

        /// <summary>Anonimización GDPR de los datos personales del registro.</summary>
        AnonimizacionGDPR = 5
    }
}
