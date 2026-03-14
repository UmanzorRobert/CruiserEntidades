using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Cotización de servicio de limpieza enviada a un cliente potencial o existente.
    /// Contiene el presupuesto detallado de servicios y materiales antes de
    /// formalizar un ContratoServicio.
    ///
    /// Al aceptarla, el empleado ejecuta la acción "Convertir a Contrato" que crea
    /// automáticamente un ContratoServicio con los datos de la cotización.
    ///
    /// Las cotizaciones caducadas (superada FechaValidez sin respuesta) son
    /// marcadas automáticamente por el job de Hangfire de verificación de caducidad.
    ///
    /// El PDF de la cotización se genera con QuestPDF e incluye logo, datos fiscales
    /// completos, tabla de servicios con IVA desglosado y condiciones de pago.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NumeroCotizacion).IsRequired().HasMaxLength(30);
    ///   builder.Property(x => x.BaseImponible).HasPrecision(18, 2);
    ///   builder.Property(x => x.TotalIVA).HasPrecision(18, 2);
    ///   builder.Property(x => x.Total).HasPrecision(18, 2);
    ///   builder.HasIndex(x => x.NumeroCotizacion).IsUnique();
    ///   builder.HasIndex(x => new { x.ClienteId, x.Estado });
    ///   builder.HasIndex(x => x.FechaValidez);
    /// </remarks>
    public class CotizacionServicio : EntidadBase
    {
        /// <summary>
        /// Número único de la cotización generado automáticamente.
        /// Formato: "COT-2026-0001". Correlativo por año sin saltos.
        /// </summary>
        public string NumeroCotizacion { get; set; } = string.Empty;

        /// <summary>FK hacia el cliente al que va dirigida la cotización.</summary>
        public Guid ClienteId { get; set; }

        /// <summary>FK hacia el empleado comercial que elaboró y envió la cotización.</summary>
        public Guid EmpleadoPorId { get; set; }

        /// <summary>Estado actual del ciclo de vida de la cotización.</summary>
        public EstadoCotizacion Estado { get; set; } = EstadoCotizacion.Borrador;

        /// <summary>Fecha de emisión de la cotización.</summary>
        public DateOnly FechaEmision { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        /// <summary>
        /// Fecha límite de validez de la cotización.
        /// Pasada esta fecha sin respuesta del cliente, el estado cambia a Caducada.
        /// Por defecto: 30 días desde FechaEmision.
        /// </summary>
        public DateOnly FechaValidez { get; set; }

        /// <summary>Fecha en que el cliente aceptó o rechazó la cotización.</summary>
        public DateOnly? FechaRespuestaCliente { get; set; }

        // ── Importes ─────────────────────────────────────────────────────────

        /// <summary>Suma de importes de líneas antes de impuestos.</summary>
        public decimal BaseImponible { get; set; } = 0m;

        /// <summary>Importe total de IVA o impuesto indirecto aplicable.</summary>
        public decimal TotalIVA { get; set; } = 0m;

        /// <summary>Importe total de la cotización: BaseImponible + TotalIVA.</summary>
        public decimal Total { get; set; } = 0m;

        /// <summary>Porcentaje de descuento global aplicado sobre el total de la cotización.</summary>
        public decimal PorcentajeDescuentoGlobal { get; set; } = 0m;

        // ── Conversión a contrato ────────────────────────────────────────────

        /// <summary>
        /// FK hacia el contrato de servicio creado a partir de esta cotización.
        /// Nulo mientras la cotización no haya sido aceptada y convertida.
        /// </summary>
        public Guid? ConvertidaAContratoId { get; set; }

        /// <summary>Fecha y hora UTC en que se convirtió la cotización en contrato.</summary>
        public DateTime? FechaConversionContrato { get; set; }

        // ── Contenido ────────────────────────────────────────────────────────

        /// <summary>Descripción general de los servicios incluidos en la cotización.</summary>
        public string? DescripcionServicios { get; set; }

        /// <summary>Condiciones de pago propuestas en la cotización.</summary>
        public string? CondicionesPago { get; set; }

        /// <summary>Notas o cláusulas adicionales incluidas en la cotización.</summary>
        public string? Notas { get; set; }

        /// <summary>Motivo del rechazo por parte del cliente cuando Estado = Rechazada.</summary>
        public string? MotivoRechazo { get; set; }

        /// <summary>Ruta relativa del PDF generado de la cotización. Nulo hasta que se genere.</summary>
        public string? RutaDocumentoPDF { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Cliente al que va dirigida la cotización.</summary>
        public virtual Cliente? Cliente { get; set; }

        /// <summary>Líneas de servicio y materiales incluidas en la cotización.</summary>
        public virtual ICollection<MaterialServicio> Materiales { get; set; }
            = new List<MaterialServicio>();
    }
}