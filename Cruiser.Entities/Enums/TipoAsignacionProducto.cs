using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de asignación de productos desde el almacén hacia el destino.
    /// Determina si los productos se asignan a un cliente, a un empleado
    /// para uso en campo, o se consumen internamente.
    /// </summary>
    public enum TipoAsignacionProducto
    {
        /// <summary>Asignación de productos a un cliente (genera factura o albarán).</summary>
        AsignacionCliente = 1,
        /// <summary>Asignación de materiales de trabajo a un empleado para uso en campo.</summary>
        AsignacionEmpleado = 2,
        /// <summary>Consumo interno de productos por la empresa (no facturable).</summary>
        ConsumoInterno = 3,
        /// <summary>Muestra o donación de producto a cliente potencial sin cargo.</summary>
        Muestra = 4
    }
}
