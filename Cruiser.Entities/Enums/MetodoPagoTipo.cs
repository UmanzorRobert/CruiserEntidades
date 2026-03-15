using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo o naturaleza del método de pago utilizado para cobrar facturas.
    /// Determina el flujo de conciliación y los campos requeridos al registrar un pago.
    /// </summary>
    public enum MetodoPagoTipo
    {
        /// <summary>Transferencia bancaria SEPA o internacional.</summary>
        Transferencia = 1,
        /// <summary>Domiciliación bancaria SEPA (adeudo directo, norma 19.14).</summary>
        DomiciliacionSEPA = 2,
        /// <summary>Pago con tarjeta de crédito o débito (TPV o pasarela online).</summary>
        Tarjeta = 3,
        /// <summary>Pago en efectivo (solo para clientes presenciales).</summary>
        Efectivo = 4,
        /// <summary>Cheque bancario o pagaré.</summary>
        Cheque = 5,
        /// <summary>Pago a través de pasarela de pago online (Stripe, PayPal, Redsys).</summary>
        PasarelaOnline = 6,
        /// <summary>Compensación o cruce de deudas entre cliente y proveedor.</summary>
        Compensacion = 7,
        /// <summary>Pago aplazado mediante financiación externa.</summary>
        Financiacion = 8
    }
}
