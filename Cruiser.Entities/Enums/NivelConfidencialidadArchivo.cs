using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Nivel de confidencialidad de un archivo adjunto que determina
    /// quién puede acceder a él y si requiere cifrado en el almacenamiento.
    /// </summary>
    public enum NivelConfidencialidadArchivo
    {
        /// <summary>Archivo público. Puede ser visto por cualquier usuario del sistema.</summary>
        Publico = 1,
        /// <summary>Archivo interno. Solo visible para empleados con el permiso correspondiente.</summary>
        Interno = 2,
        /// <summary>Archivo confidencial. Solo visible para el rol Supervisor o superior.</summary>
        Confidencial = 3,
        /// <summary>Archivo secreto. Solo visible para Administrador o el propietario del registro.</summary>
        Secreto = 4
    }
}
