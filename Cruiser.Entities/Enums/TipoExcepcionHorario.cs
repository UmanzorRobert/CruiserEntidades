using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de excepción al horario regular de un empleado.
    /// Las excepciones modifican puntualmente la disponibilidad del empleado
    /// sin cambiar su horario base en HorarioEmpleado.
    /// </summary>
    public enum TipoExcepcionHorario
    {
        /// <summary>El empleado está disponible en una fecha normalmente no laborable (hora extra, guardia).</summary>
        Disponible = 1,
        /// <summary>El empleado no está disponible en una fecha normalmente laborable (baja, asunto propio).</summary>
        NoDisponible = 2,
        /// <summary>El empleado tiene un horario diferente al habitual ese día específico.</summary>
        HorarioEspecial = 3
    }
}
