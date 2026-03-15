using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de respuesta aceptada para un ítem del checklist de servicio.
    /// Determina el control de entrada mostrado al empleado en la PWA.
    /// </summary>
    public enum TipoRespuestaChecklist
    {
        /// <summary>Respuesta binaria Sí / No. Se muestra como toggle o dos botones.</summary>
        SiNo = 1,
        /// <summary>Texto libre. Se muestra un campo de texto para observaciones.</summary>
        Texto = 2,
        /// <summary>Valor numérico. Se muestra un campo numérico (cantidad, temperatura, pH).</summary>
        Numerico = 3,
        /// <summary>Fotografía obligatoria. Activa la cámara del dispositivo PWA.</summary>
        Foto = 4,
        /// <summary>Calificación numérica del 1 al 5 (estrellas o número).</summary>
        Calificacion = 5,
        /// <summary>Selección de una opción de una lista predefinida.</summary>
        Lista = 6
    }
}
