using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Facturacion
{
    /// <summary>
    /// Pago individual incluido en una remesa bancaria SEPA.
    /// Relaciona un PagoFactura con una RemesaBancaria y gestiona
    /// las devoluciones mediante los códigos R-transaction del estándar SEPA.
    ///
    /// CÓDIGOS DE DEVOLUCIÓN SEPA (R-Transactions):
    /// R01: InsufficientFunds (fondos insuficientes)
    /// R02: BankAccountClosed (cuenta cerrada)
    /// R03: NoAccountOrIncorrectAccountNumber (cuenta incorrecta)
    /// R04: InvalidBankOperationCode
    /// R07: EndCustomerDeceased
    /// R08: ManDateAmendment
    /// R09: CreditorNotInformed
    /// R10: CustomerRevocation (cliente revocó el mandato)
    ///
    /// Al procesar una devolución (EsDevuelto=true), se reabre la CuentaCobrar
    /// y se registra una nueva acción de cobranza.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Importe).HasPrecision(18, 2);
    ///   builder.Property(x => x.CodigoDevolucion).HasMaxLength(10);
    ///   builder.HasIndex(x => x.RemesaBancariaId);
    ///   builder.HasIndex(x => x.PagoFacturaId).IsUnique();
    ///   builder.HasIndex(x => x.EsDevuelto);
    /// </remarks>
    public class PagoRemesa : EntidadBase
    {
        /// <summary>FK hacia la remesa bancaria a la que pertenece este pago.</summary>
        public Guid RemesaBancariaId { get; set; }

        /// <summary>FK hacia el pago de factura incluido en la remesa.</summary>
        public Guid PagoFacturaId { get; set; }

        /// <summary>FK hacia el cliente deudor.</summary>
        public Guid ClienteId { get; set; }

        /// <summary>Importe del recibo en la remesa.</summary>
        public decimal Importe { get; set; }

        /// <summary>IBAN del cliente para la domiciliación del recibo en la remesa.</summary>
        public string? IBANCliente { get; set; }

        /// <summary>BIC del banco del cliente.</summary>
        public string? BICCliente { get; set; }

        /// <summary>
        /// Referencia única del mandato SEPA firmado por el cliente.
        /// Requerida por el estándar SEPA para cada adeudo domiciliado.
        /// </summary>
        public string? ReferenciaMandato { get; set; }

        /// <summary>Fecha de firma del mandato SEPA por el cliente.</summary>
        public DateOnly? FechaFirmaMandato { get; set; }

        // ── Devolución ───────────────────────────────────────────────────────

        /// <summary>Indica si este recibo fue devuelto por el banco.</summary>
        public bool EsDevuelto { get; set; } = false;

        /// <summary>
        /// Código R-transaction del estándar SEPA que indica el motivo de la devolución.
        /// Ejemplo: "R01" (fondos insuficientes), "R10" (revocación por el cliente).
        /// </summary>
        public string? CodigoDevolucion { get; set; }

        /// <summary>Descripción del motivo de devolución en texto libre.</summary>
        public string? MotivoDevolucion { get; set; }

        /// <summary>Fecha en que el banco notificó la devolución del recibo.</summary>
        public DateOnly? FechaDevolucion { get; set; }

        /// <summary>
        /// Indica si la devolución ha sido gestionada (se reabrió la CuentaCobrar
        /// y se planificó la acción de cobranza siguiente).
        /// </summary>
        public bool DevolucionGestionada { get; set; } = false;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Remesa bancaria a la que pertenece este pago.</summary>
        public virtual RemesaBancaria? RemesaBancaria { get; set; }

        /// <summary>Pago de factura incluido en la remesa.</summary>
        public virtual PagoFactura? PagoFactura { get; set; }
    }
}
