using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del ciclo de vida de una cotización de servicio enviada a un cliente potencial.
    /// </summary>
    public enum EstadoCotizacion
    {
        /// <summary>Cotización en elaboración. No enviada al cliente aún.</summary>
        Borrador = 1,
        /// <summary>Cotización enviada al cliente por email o entregada en mano.</summary>
        Enviada = 2,
        /// <summary>Cliente aceptó la cotización. Pendiente de convertir a contrato.</summary>
        Aceptada = 3,
        /// <summary>Cliente rechazó la cotización explícitamente.</summary>
        Rechazada = 4,
        /// <summary>
        /// Cotización caducada por superar la fecha límite de validez
        /// sin respuesta del cliente.
        /// </summary>
        Caducada = 5,
        /// <summary>Cotización convertida en contrato de servicio activo.</summary>
        ConvertidaContrato = 6
    }
}
