using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>Tipo de mantenimiento o intervención técnica realizada en un vehículo.</summary>
    public enum TipoMantenimiento
    {
        /// <summary>Mantenimiento preventivo programado: aceite, filtros, neumáticos.</summary>
        Preventivo = 1,
        /// <summary>Reparación correctiva de avería no programada.</summary>
        Correctivo = 2,
        /// <summary>Inspección Técnica de Vehículos (ITV) obligatoria.</summary>
        ITV = 3,
        /// <summary>Revisión de la póliza de seguro o renovación.</summary>
        Seguro = 4,
        /// <summary>Lavado interior y exterior del vehículo.</summary>
        Lavado = 5,
        /// <summary>Revisión de neumáticos: cambio de temporada, equilibrado.</summary>
        Neumaticos = 6,
        /// <summary>Revisión de frenos y sistema de seguridad.</summary>
        Frenos = 7,
        /// <summary>Otro tipo de intervención técnica no categorizada.</summary>
        Otro = 8
    }
}
