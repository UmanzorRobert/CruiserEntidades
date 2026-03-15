using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del ciclo de vida de un contrato de servicio de limpieza.
    /// Determina si se pueden programar órdenes de servicio y generar facturas recurrentes.
    /// </summary>
    public enum EstadoContrato
    {
        /// <summary>Contrato en elaboración. No genera facturas ni órdenes.</summary>
        Borrador = 1,
        /// <summary>Contrato pendiente de firma por el cliente.</summary>
        PendienteFirma = 2,
        /// <summary>Contrato activo y en vigor. Genera facturas y órdenes según la programación.</summary>
        Activo = 3,
        /// <summary>Contrato temporalmente suspendido por acuerdo mutuo o incidencia.</summary>
        Suspendido = 4,
        /// <summary>Contrato finalizado normalmente al término del período pactado.</summary>
        Finalizado = 5,
        /// <summary>Contrato rescindido anticipadamente por una de las partes.</summary>
        Rescindido = 6,
        /// <summary>Contrato renovado automáticamente. Se crea un nuevo contrato sucesor.</summary>
        Renovado = 7
    }
}
