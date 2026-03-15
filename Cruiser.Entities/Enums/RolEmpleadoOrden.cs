using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Rol que desempeña un empleado en una orden de servicio específica.
    /// </summary>
    public enum RolEmpleadoOrden
    {
        /// <summary>Responsable principal de la orden. Firma el acta de servicio.</summary>
        Responsable = 1,
        /// <summary>Empleado de apoyo. Trabaja bajo la dirección del responsable.</summary>
        Auxiliar = 2,
        /// <summary>Supervisor que inspecciona la calidad del servicio sin ejecutarlo.</summary>
        Supervisor = 3
    }
}
