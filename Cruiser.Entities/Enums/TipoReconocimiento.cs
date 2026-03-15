using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>Tipo de reconocimiento otorgado a un empleado por su desempeño o conducta.</summary>
    public enum TipoReconocimiento
    {
        /// <summary>Empleado del mes elegido por votación del equipo o decisión de dirección.</summary>
        EmpleadoDelMes = 1,
        /// <summary>Felicitación por superar los objetivos de ventas o calidad del período.</summary>
        SuperacionObjetivos = 2,
        /// <summary>Reconocimiento por antigüedad en la empresa (5, 10, 15, 20 años).</summary>
        Antiguedad = 3,
        /// <summary>Felicitación por una actuación ejemplar o resolución excelente de una incidencia.</summary>
        ActuacionEjemplar = 4,
        /// <summary>Reconocimiento por proponer una mejora implementada en la empresa.</summary>
        InovacionMejora = 5,
        /// <summary>Otra distinción especial no categorizada.</summary>
        Otro = 6
    }
}
