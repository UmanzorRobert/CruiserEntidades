using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>Nivel de dificultad técnica de un protocolo o servicio de limpieza.</summary>
    public enum NivelDificultad
    {
        /// <summary>Servicio básico que puede realizar cualquier empleado con formación estándar.</summary>
        Basico = 1,
        /// <summary>Servicio que requiere experiencia previa y conocimiento de productos específicos.</summary>
        Intermedio = 2,
        /// <summary>Servicio especializado que requiere formación certificada y experiencia avanzada.</summary>
        Avanzado = 3,
        /// <summary>Servicio altamente especializado con riesgo (altura, productos peligrosos, industria).</summary>
        Experto = 4
    }
}
