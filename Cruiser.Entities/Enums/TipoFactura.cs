using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de documento de facturación según la normativa fiscal española.
    /// Determina la obligatoriedad de campos, el cálculo del hash VeriFactu
    /// y las validaciones de la cadena de integridad.
    /// </summary>
    public enum TipoFactura
    {
        /// <summary>
        /// Factura ordinaria o completa. Documento principal de facturación.
        /// Requiere todos los datos fiscales del emisor y receptor.
        /// </summary>
        Normal = 1,
        /// <summary>
        /// Factura rectificativa (nota de crédito o débito).
        /// Debe referenciar la factura original que rectifica mediante FacturaOriginalId.
        /// </summary>
        Rectificativa = 2,
        /// <summary>
        /// Factura de abono completo de la factura original.
        /// Anula económicamente la factura original sin eliminarla del sistema.
        /// </summary>
        Abono = 3,
        /// <summary>
        /// Factura proforma o presupuesto no vinculante.
        /// No tiene efectos fiscales ni contables. No forma parte de la cadena VeriFactu.
        /// </summary>
        Proforma = 4,
        /// <summary>
        /// Factura simplificada (ticket) para importes inferiores a 400€.
        /// No requiere NIF del receptor. Regulada por el Art. 7 RD 1619/2012.
        /// </summary>
        Simplificada = 5
    }
}
