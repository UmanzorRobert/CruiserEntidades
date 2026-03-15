using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del ciclo de vida de una orden de servicio de limpieza.
    /// Sigue el flujo desde la programación hasta la facturación.
    /// </summary>
    public enum EstadoOrdenServicio
    {
        /// <summary>Orden creada y pendiente de asignar equipo y programar fecha.</summary>
        Pendiente = 1,
        /// <summary>Orden programada con fecha, equipo y vehículo asignados.</summary>
        Programada = 2,
        /// <summary>Equipo confirmado y en desplazamiento hacia las instalaciones del cliente.</summary>
        EnDesplazamiento = 3,
        /// <summary>Servicio iniciado. El equipo está trabajando en las instalaciones.</summary>
        EnEjecucion = 4,
        /// <summary>Servicio completado por el equipo. Pendiente de validación del supervisor.</summary>
        Completada = 5,
        /// <summary>Servicio validado por el supervisor y aprobado para facturación.</summary>
        Validada = 6,
        /// <summary>Factura emitida para esta orden de servicio.</summary>
        Facturada = 7,
        /// <summary>Orden cancelada antes de ejecutarse. No genera coste ni factura.</summary>
        Cancelada = 8,
        /// <summary>Orden reprogramada a otra fecha. La orden original queda cerrada.</summary>
        Reprogramada = 9
    }
}
