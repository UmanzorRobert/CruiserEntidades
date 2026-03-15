using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de ausencia laboral de un empleado según la causa que la motiva.
    /// Determina si la ausencia es remunerada, si requiere justificante y
    /// si afecta a la facturación del servicio asignado al cliente.
    /// </summary>
    public enum TipoAusencia
    {
        /// <summary>Baja médica por enfermedad común o accidente no laboral.</summary>
        BajaMedica = 1,
        /// <summary>Accidente de trabajo o enfermedad profesional (cobertura mutua).</summary>
        AccidenteTrabajo = 2,
        /// <summary>Permiso de maternidad o paternidad remunerado.</summary>
        Maternidad = 3,
        /// <summary>Permiso por asuntos propios (máximo días según convenio, no remunerado).</summary>
        AsuntosPropios = 4,
        /// <summary>Permiso retribuido por causa justificada: matrimonio, fallecimiento familiar, etc.</summary>
        PermisoRetribuido = 5,
        /// <summary>Huelga legal. No remunerada. Requiere comunicación previa a RRHH.</summary>
        Huelga = 6,
        /// <summary>Ausencia injustificada sin comunicación previa. Genera incidencia RRHH.</summary>
        Injustificada = 7,
        /// <summary>Excedencia voluntaria o forzosa. Suspende temporalmente el contrato.</summary>
        Excedencia = 8,
        /// <summary>Reducción de jornada por cuidado de familiar o lactancia.</summary>
        ReduccionJornada = 9,
        /// <summary>Suspensión disciplinaria de empleo y sueldo.</summary>
        SuspensionDisciplinaria = 10
    }
}
