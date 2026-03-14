using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Categoría funcional del movimiento de inventario para clasificación
    /// en reportes, análisis de costos y control de gestión.
    /// </summary>
    public enum CategoriaMovimiento
    {
        /// <summary>Recepción de mercancía de proveedor (asociado a OrdenCompra).</summary>
        Compra = 1,
        /// <summary>Salida de material hacia cliente o para servicio de limpieza.</summary>
        Venta = 2,
        /// <summary>Devolución de material desde cliente al almacén.</summary>
        DevolucionCliente = 3,
        /// <summary>Devolución de material al proveedor.</summary>
        DevolucionProveedor = 4,
        /// <summary>Ajuste manual de stock (positivo o negativo) con justificación.</summary>
        Ajuste = 5,
        /// <summary>Transferencia de stock entre almacenes internos.</summary>
        Transferencia = 6,
        /// <summary>Salida por merma, caducidad, rotura o pérdida.</summary>
        Merma = 7,
        /// <summary>Entrada por producción interna o fabricación propia.</summary>
        Produccion = 8,
        /// <summary>Asignación de material a empleado para uso en campo.</summary>
        AsignacionEmpleado = 9,
        /// <summary>Ajuste resultante de un inventario cíclico con diferencias.</summary>
        AjusteInventarioCiclico = 10,
        /// <summary>Consumo interno del material por la propia empresa.</summary>
        ConsumoInterno = 11
    }
}
