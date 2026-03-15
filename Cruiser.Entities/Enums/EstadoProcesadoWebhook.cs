using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del procesamiento de un webhook recibido de un sistema externo.
    /// </summary>
    public enum EstadoProcesadoWebhook
    {
        /// <summary>Webhook recibido y pendiente de procesar por IIntegracionService.</summary>
        Pendiente = 1,
        /// <summary>Webhook procesado exitosamente. Los datos fueron actualizados en BD.</summary>
        Procesado = 2,
        /// <summary>Error al procesar el webhook. Se reintentará automáticamente por Hangfire.</summary>
        Error = 3,
        /// <summary>Webhook ignorado porque el tipo o los datos no son reconocidos.</summary>
        Ignorado = 4,
        /// <summary>Webhook descartado por duplicado (ya fue procesado anteriormente).</summary>
        Duplicado = 5
    }
}
