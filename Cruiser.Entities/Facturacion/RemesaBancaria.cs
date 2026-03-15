using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Facturacion
{
    /// <summary>
    /// Remesa bancaria SEPA de domiciliación de recibos (Core Direct Debit).
    /// Agrupa múltiples PagoFactura pendientes de domiciliar y genera el fichero XML
    /// en formato SEPA ISO 20022 (pain.008.003.02) para presentar al banco.
    ///
    /// NORMA SEPA 19.14: El fichero XML se genera siguiendo las especificaciones
    /// del Cuaderno 19.14 de la AEB (Asociación Española de Banca).
    ///
    /// GESTIÓN DE DEVOLUCIONES: Los recibos devueltos por el banco (códigos R-transaction:
    /// R01-InsufficientFunds, R02-BankAccountClosed, etc.) se gestionan como PagoRemesa
    /// con EsDevuelto=true y se reabren las CuentaCobrar correspondientes.
    ///
    /// REQUISITO LEGAL: El mandato SEPA (autorización de domiciliación firmada por el
    /// cliente) debe estar registrado antes de incluir su recibo en la remesa.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Referencia).IsRequired().HasMaxLength(35);
    ///   builder.Property(x => x.ImporteTotal).HasPrecision(18, 2);
    ///   builder.Property(x => x.ImporteDevuelto).HasPrecision(18, 2);
    ///   builder.HasIndex(x => x.Referencia).IsUnique();
    ///   builder.HasIndex(x => x.Estado);
    ///   builder.HasIndex(x => x.FechaPresentacion);
    /// </remarks>
    public class RemesaBancaria : EntidadBase
    {
        /// <summary>
        /// Referencia única de la remesa. Formato: "REM-2026-001".
        /// Aparece en el fichero XML SEPA como MessageIdentification (máx. 35 chars).
        /// </summary>
        public string Referencia { get; set; } = string.Empty;

        /// <summary>Descripción de la remesa para identificación interna.</summary>
        public string? Descripcion { get; set; }

        /// <summary>Estado actual de la remesa en su ciclo de vida.</summary>
        public EstadoRemesa Estado { get; set; } = EstadoRemesa.EnPreparacion;

        // ── Fechas SEPA ──────────────────────────────────────────────────────

        /// <summary>
        /// Fecha de cobro solicitada al banco (RequestedCollectionDate en XML SEPA).
        /// Debe ser al menos 5 días laborables después de la presentación para adeudos CORE First.
        /// </summary>
        public DateOnly FechaCobroSolicitada { get; set; }

        /// <summary>Fecha en que se presentó el fichero XML al banco.</summary>
        public DateOnly? FechaPresentacion { get; set; }

        /// <summary>Fecha en que el banco confirmó el procesamiento de la remesa.</summary>
        public DateOnly? FechaConfirmacionBanco { get; set; }

        // ── Importes ─────────────────────────────────────────────────────────

        /// <summary>Número de recibos incluidos en la remesa.</summary>
        public int NumeroRecibos { get; set; } = 0;

        /// <summary>Importe total de todos los recibos de la remesa.</summary>
        public decimal ImporteTotal { get; set; } = 0m;

        /// <summary>
        /// Importe total de los recibos devueltos por el banco.
        /// Actualizado al procesar las notificaciones de devolución.
        /// </summary>
        public decimal ImporteDevuelto { get; set; } = 0m;

        /// <summary>
        /// Importe neto cobrado: ImporteTotal - ImporteDevuelto.
        /// </summary>
        public decimal ImporteCobrado { get; set; } = 0m;

        // ── Fichero XML ──────────────────────────────────────────────────────

        /// <summary>
        /// Ruta relativa del fichero XML SEPA generado para presentar al banco.
        /// El fichero sigue el esquema ISO 20022 pain.008.003.02.
        /// </summary>
        public string? RutaArchivoSEPA { get; set; }

        /// <summary>Creditor Identifier (Identificador del Acreedor SEPA) de la empresa.</summary>
        public string? CreditorIdentifier { get; set; }

        /// <summary>IBAN de la cuenta bancaria de la empresa donde se abonarán los cobros.</summary>
        public string? IBANEmpresa { get; set; }

        /// <summary>BIC del banco de la empresa.</summary>
        public string? BICEmpresa { get; set; }

        /// <summary>Notas o incidencias de la gestión de la remesa.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Pagos de facturas incluidos en esta remesa bancaria.</summary>
        public virtual ICollection<PagoRemesa> Pagos { get; set; }
            = new List<PagoRemesa>();
    }
}
