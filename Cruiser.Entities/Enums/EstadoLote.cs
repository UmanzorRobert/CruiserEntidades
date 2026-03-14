using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado sanitario o de calidad de un lote de producto en el sistema de inventario.
    /// Controla si el lote puede ser utilizado, asignado o vendido.
    /// </summary>
    public enum EstadoLote
    {
        /// <summary>Lote aprobado para uso normal. Stock disponible para asignación.</summary>
        Aprobado = 1,
        /// <summary>Lote bloqueado temporalmente. No disponible hasta nueva revisión.</summary>
        Bloqueado = 2,
        /// <summary>Lote caducado. Fecha de caducidad superada. No puede ser usado.</summary>
        Caducado = 3,
        /// <summary>
        /// Lote en cuarentena pendiente de análisis de calidad.
        /// Recibido pero no disponible hasta superar el control de calidad.
        /// </summary>
        EnCuarentena = 4,
        /// <summary>Lote agotado. Stock = 0. Se conserva por trazabilidad histórica.</summary>
        Agotado = 5,
        /// <summary>Lote devuelto al proveedor. Ya no está físicamente en el almacén.</summary>
        Devuelto = 6
    }
}
