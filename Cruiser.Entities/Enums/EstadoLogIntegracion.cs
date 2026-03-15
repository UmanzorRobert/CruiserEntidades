using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del resultado de una llamada a un sistema de integración externo.
    /// Registrado en LogIntegracion para monitoreo y diagnóstico de fallos.
    /// </summary>
    public enum EstadoLogIntegracion
    {
        /// <summary>La llamada al sistema externo se completó exitosamente con respuesta válida.</summary>
        Exitoso = 1,
        /// <summary>La llamada falló con un error HTTP o de negocio del sistema externo.</summary>
        Error = 2,
        /// <summary>La llamada superó el tiempo de espera máximo configurado sin respuesta.</summary>
        Timeout = 3,
        /// <summary>La llamada está en proceso de reintento tras un fallo previo.</summary>
        Reintentando = 4,
        /// <summary>La llamada fue rechazada por límite de tasa (rate limiting) del sistema externo.</summary>
        RateLimitado = 5
    }
}
