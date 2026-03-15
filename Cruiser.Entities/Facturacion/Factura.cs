using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Facturacion
{
    /// <summary>
    /// Factura de venta emitida a un cliente. Documento fiscal principal del módulo
    /// de facturación. Cumple con la normativa fiscal española vigente incluyendo
    /// los requisitos del sistema VeriFactu (RD 1007/2023) mediante cadena de hashes SHA-256.
    ///
    /// INTEGRIDAD VERIFACTU:
    /// Cada factura almacena su propio hash (HashFactura) calculado como:
    ///   SHA-256(NombreEmisor + NIF + NumeroFactura + Fecha + BaseImponible + TotalIVA + Total
    ///           + HashFacturaAnterior)
    /// Esto crea una cadena de bloques que permite detectar cualquier manipulación
    /// de facturas emitidas. El endpoint /admin/verificar-integridad-facturas verifica
    /// que la cadena esté intacta.
    ///
    /// GDPR: Si el cliente está anonimizado (EstaAnonimizado=true en Cliente),
    /// la vista muestra badge "Cliente Anonimizado (GDPR)" en lugar del nombre.
    /// Los importes de la factura permanecen intactos por obligación fiscal (Art. 255 CCom).
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NumeroFactura).IsRequired().HasMaxLength(50);
    ///   builder.Property(x => x.BaseImponible).HasPrecision(18, 2);
    ///   builder.Property(x => x.TotalIVA).HasPrecision(18, 2);
    ///   builder.Property(x => x.Total).HasPrecision(18, 2);
    ///   builder.Property(x => x.ImportePagado).HasPrecision(18, 2);
    ///   builder.Property(x => x.ImportePendiente).HasPrecision(18, 2);
    ///   builder.Property(x => x.PorcentajeRetencion).HasPrecision(5, 2);
    ///   builder.Property(x => x.HashFactura).HasMaxLength(64);
    ///   builder.Property(x => x.HashFacturaAnterior).HasMaxLength(64);
    ///   builder.HasIndex(x => x.NumeroFactura).IsUnique();
    ///   builder.HasIndex(x => new { x.ClienteId, x.EstadoFacturaId });
    ///   builder.HasIndex(x => x.FechaEmision);
    ///   builder.HasIndex(x => x.FechaVencimiento);
    ///   builder.HasIndex(x => new { x.SerieFacturaId, x.NumeroCorrelativo });
    /// </remarks>
    public class Factura : EntidadBase
    {
        // ── Identificación ───────────────────────────────────────────────────

        /// <summary>
        /// Número completo de la factura: Prefijo + Serie + Ejercicio + Número.
        /// Ejemplo: "A-2026-0001", "R-2026-0001".
        /// Único en el sistema. Generado automáticamente al emitir.
        /// </summary>
        public string NumeroFactura { get; set; } = string.Empty;

        /// <summary>FK hacia la serie de numeración de esta factura.</summary>
        public Guid SerieFacturaId { get; set; }

        /// <summary>Número correlativo dentro de la serie y ejercicio. Ejemplo: 1, 2, 42.</summary>
        public int NumeroCorrelativo { get; set; }

        /// <summary>Tipo de factura (Normal, Rectificativa, Abono, Proforma, Simplificada).</summary>
        public TipoFactura TipoFactura { get; set; } = TipoFactura.Normal;

        /// <summary>FK hacia el cliente al que se emite la factura.</summary>
        public Guid ClienteId { get; set; }

        /// <summary>FK hacia el estado actual de la factura.</summary>
        public Guid EstadoFacturaId { get; set; }

        // ── Factura rectificativa ────────────────────────────────────────────

        /// <summary>
        /// FK hacia la factura original que esta factura rectifica o anula.
        /// Solo aplica cuando TipoFactura = Rectificativa o Abono.
        /// </summary>
        public Guid? FacturaOriginalId { get; set; }

        /// <summary>
        /// Motivo legal de la rectificación según el Art. 13 RD 1619/2012.
        /// Ejemplo: "Error en precio unitario", "Devolución de servicios".
        /// </summary>
        public string? MotivoRectificacion { get; set; }

        // ── Fechas ───────────────────────────────────────────────────────────

        /// <summary>Fecha de emisión de la factura (fecha legal del documento).</summary>
        public DateOnly FechaEmision { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        /// <summary>
        /// Fecha de operación si difiere de la fecha de emisión.
        /// Usada en facturas diferidas o en facturas de servicios ya prestados.
        /// </summary>
        public DateOnly? FechaOperacion { get; set; }

        /// <summary>
        /// Fecha de vencimiento para el cobro de la factura.
        /// Calculada: FechaEmision + Cliente.DiasPlazoPago.
        /// </summary>
        public DateOnly FechaVencimiento { get; set; }

        // ── Datos fiscales del emisor (fijos al emitir) ──────────────────────

        /// <summary>Nombre/razón social de la empresa emisora al momento de emitir la factura.</summary>
        public string NombreEmisor { get; set; } = string.Empty;

        /// <summary>NIF de la empresa emisora.</summary>
        public string NIFEmisor { get; set; } = string.Empty;

        /// <summary>Dirección fiscal completa del emisor.</summary>
        public string DireccionEmisor { get; set; } = string.Empty;

        // ── Datos fiscales del receptor (fijos al emitir) ────────────────────

        /// <summary>Nombre/razón social del cliente receptor al momento de emitir.</summary>
        public string NombreReceptor { get; set; } = string.Empty;

        /// <summary>NIF del cliente receptor. Puede estar anonimizado (GDPR).</summary>
        public string? NIFReceptor { get; set; }

        /// <summary>Dirección fiscal del cliente receptor al momento de emitir.</summary>
        public string? DireccionReceptor { get; set; }

        // ── Importes ─────────────────────────────────────────────────────────

        /// <summary>Suma de importes netos de las líneas de la factura sin impuestos.</summary>
        public decimal BaseImponible { get; set; } = 0m;

        /// <summary>Importe total de IVA o impuesto indirecto (puede ser IGIC en Canarias).</summary>
        public decimal TotalIVA { get; set; } = 0m;

        /// <summary>
        /// Porcentaje de retención IRPF aplicado (para autónomos y profesionales).
        /// Ejemplo: 15% para retenciones profesionales.
        /// </summary>
        public decimal PorcentajeRetencion { get; set; } = 0m;

        /// <summary>Importe de retención IRPF: BaseImponible × PorcentajeRetencion / 100.</summary>
        public decimal ImporteRetencion { get; set; } = 0m;

        /// <summary>Total de la factura: BaseImponible + TotalIVA - ImporteRetencion.</summary>
        public decimal Total { get; set; } = 0m;

        /// <summary>Importe total cobrado hasta el momento en pagos registrados.</summary>
        public decimal ImportePagado { get; set; } = 0m;

        /// <summary>Importe pendiente de cobro: Total - ImportePagado.</summary>
        public decimal ImportePendiente { get; set; } = 0m;

        // ── VeriFactu (cadena de integridad) ─────────────────────────────────

        /// <summary>
        /// Hash SHA-256 de esta factura calculado con los datos fiscales + HashFacturaAnterior.
        /// Garantiza la integridad de la cadena de facturas (VeriFactu / RD 1007/2023).
        /// Se calcula automáticamente al emitir y no puede modificarse posteriormente.
        /// </summary>
        public string? HashFactura { get; set; }

        /// <summary>
        /// Hash SHA-256 de la factura anterior en la misma serie.
        /// "0000...0000" (64 ceros) para la primera factura de la serie.
        /// Crea la cadena de bloques que detecta manipulaciones retroactivas.
        /// </summary>
        public string? HashFacturaAnterior { get; set; }

        /// <summary>Fecha y hora UTC en que se calculó y fijó el hash de esta factura.</summary>
        public DateTime? FechaHashGenerado { get; set; }

        // ── Orígenes ─────────────────────────────────────────────────────────

        /// <summary>
        /// FK hacia el contrato de servicio que originó esta factura recurrente.
        /// Nulo para facturas manuales o de asignación puntual.
        /// </summary>
        public Guid? ContratoServicioId { get; set; }

        /// <summary>
        /// FK hacia la asignación de productos que se factura.
        /// Nulo para facturas de servicios o manuales.
        /// </summary>
        public Guid? AsignacionProductoId { get; set; }

        /// <summary>Notas o texto libre visible en el PDF de la factura.</summary>
        public string? Notas { get; set; }

        /// <summary>Ruta relativa del PDF legal generado con QuestPDF.</summary>
        public string? RutaDocumentoPDF { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Serie de numeración de la factura.</summary>
        public virtual SerieFactura? SerieFactura { get; set; }

        /// <summary>Estado actual de la factura.</summary>
        public virtual EstadoFactura? EstadoFactura { get; set; }

        /// <summary>Cliente al que se emite la factura.</summary>
        public virtual Comercial.Cliente? Cliente { get; set; }

        /// <summary>Factura original rectificada por esta factura (si aplica).</summary>
        public virtual Factura? FacturaOriginal { get; set; }

        /// <summary>Líneas de detalle con servicios y productos facturados.</summary>
        public virtual ICollection<DetalleFactura> Detalles { get; set; }
            = new List<DetalleFactura>();

        /// <summary>Pagos registrados contra esta factura.</summary>
        public virtual ICollection<PagoFactura> Pagos { get; set; }
            = new List<PagoFactura>();

        /// <summary>Cuenta por cobrar generada para esta factura.</summary>
        public virtual CuentaCobrar? CuentaCobrar { get; set; }
    }
}
