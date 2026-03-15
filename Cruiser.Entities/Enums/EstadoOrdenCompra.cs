using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del ciclo de vida de una orden de compra emitida a un proveedor.
    /// Los estados siguen un flujo lineal con posibilidad de anulación en cualquier punto
    /// previo a la recepción total de la mercancía.
    /// </summary>
    public enum EstadoOrdenCompra
    {
        /// <summary>Orden creada pero pendiente de revisión y aprobación interna.</summary>
        Borrador = 1,
        /// <summary>Orden pendiente de aprobación por el nivel jerárquico requerido.</summary>
        PendienteAprobacion = 2,
        /// <summary>Orden aprobada internamente y pendiente de envío al proveedor.</summary>
        Aprobada = 3,
        /// <summary>Orden enviada al proveedor por email. Esperando confirmación.</summary>
        Enviada = 4,
        /// <summary>Proveedor confirmó el pedido. Esperando la entrega.</summary>
        Confirmada = 5,
        /// <summary>
        /// Mercancía recibida parcialmente. Recepción incompleta.
        /// Permanece en este estado hasta recibir el resto o cerrar el pedido.
        /// </summary>
        RecepcionParcial = 6,
        /// <summary>Toda la mercancía recibida y verificada. Orden completamente cerrada.</summary>
        Completada = 7,
        /// <summary>Orden anulada antes de recibir mercancía. No genera movimientos de inventario.</summary>
        Anulada = 8
    }
}
