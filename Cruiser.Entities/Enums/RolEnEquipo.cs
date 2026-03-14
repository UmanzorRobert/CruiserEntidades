using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Rol funcional que desempeña un empleado dentro de un equipo de trabajo.
    /// Determina las responsabilidades y permisos en la ejecución de órdenes de servicio.
    /// </summary>
    public enum RolEnEquipo
    {
        /// <summary>Líder del equipo. Responsable de la ejecución y calidad del servicio.</summary>
        Lider = 1,
        /// <summary>Miembro auxiliar del equipo. Ejecuta tareas bajo supervisión del líder.</summary>
        Auxiliar = 2,
        /// <summary>Especialista técnico para servicios que requieren cualificación específica.</summary>
        Especialista = 3,
        /// <summary>Supervisor externo que acompaña al equipo para control de calidad.</summary>
        Supervisor = 4,
        /// <summary>Conductor del vehículo asignado al equipo.</summary>
        Conductor = 5
    }
}
