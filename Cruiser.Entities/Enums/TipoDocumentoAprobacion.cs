using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de documento que pasa por el flujo de aprobación centralizado.
    /// Determina las reglas, niveles y notificaciones del proceso de aprobación.
    /// </summary>
    public enum TipoDocumentoAprobacion
    {
        /// <summary>Orden de compra a proveedor que supera el umbral de importe configurado.</summary>
        OrdenCompra = 1,
        /// <summary>Factura de proveedor recibida pendiente de verificación y contabilización.</summary>
        FacturaProveedor = 2,
        /// <summary>Contrato de servicio nuevo o renovación que requiere autorización de dirección.</summary>
        ContratoServicio = 3,
        /// <summary>Cotización de servicio enviada a cliente que supera umbrales de descuento.</summary>
        CotizacionServicio = 4,
        /// <summary>Gasto de empleado (viáticos, combustible, materiales) que requiere aprobación.</summary>
        GastoEmpleado = 5,
        /// <summary>Ajuste de stock manual que supera el umbral de valor configurado.</summary>
        AjusteInventario = 6,
        /// <summary>Solicitud de ausencia o vacaciones de empleado pendiente de aprobación del supervisor.</summary>
        AusenciaEmpleado = 7,
        /// <summary>Liquidación de comisiones del período pendiente de aprobación de administración.</summary>
        LiquidacionComision = 8
    }
}
