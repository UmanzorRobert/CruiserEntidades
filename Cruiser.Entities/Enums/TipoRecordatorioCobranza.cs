using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de recordatorio de cobranza según el nivel de urgencia y el tono del mensaje.
    /// El tipo determina la plantilla de email usada y el canal de notificación.
    /// </summary>
    public enum TipoRecordatorioCobranza
    {
        /// <summary>
        /// Recordatorio amistoso preventivo. Se envía pocos días antes del vencimiento.
        /// Tono informativo y cordial. Plantilla: "Recordatorio de Pago Próximo".
        /// </summary>
        Amigable = 1,
        /// <summary>
        /// Recordatorio formal post-vencimiento. Días 1-15 de mora.
        /// Tono profesional. Recuerda el importe, la fecha de vencimiento y las condiciones.
        /// </summary>
        Formal = 2,
        /// <summary>
        /// Último aviso antes de iniciar acciones legales. Días 16-30 de mora.
        /// Tono firme. Incluye advertencia de recargo por mora y acciones adicionales.
        /// </summary>
        UltimoAviso = 3,
        /// <summary>
        /// Notificación de inicio de procedimiento de reclamación extrajudicial o judicial.
        /// Solo para clientes con mora superior a 30 días sin respuesta.
        /// </summary>
        Reclamacion = 4
    }
}
