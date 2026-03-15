using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Facturacion
{
    /// <summary>
    /// Programación de generación automática de facturas periódicas vinculadas
    /// a un contrato de servicio de limpieza.
    ///
    /// El job de Hangfire GenerarFacturasRecurrentes (diario 6am) revisa todas
    /// las programaciones activas y genera automáticamente las facturas cuando
    /// se cumplen las condiciones de fecha y frecuencia.
    ///
    /// La factura generada automáticamente se crea en estado "Emitida" lista
    /// para enviar al cliente, con los importes definidos en ImporteBase más
    /// los ajustes del contrato (IVA, retención, descuentos).
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.ImporteBase).HasPrecision(18, 2);
    ///   builder.HasIndex(x => new { x.ContratoServicioId, x.EstaActiva });
    ///   builder.HasIndex(x => x.ProximaFechaEmision).HasFilter("\"EstaActiva\" = true");
    /// </remarks>
    public class ProgramacionFacturaRecurrente : EntidadBase
    {
        /// <summary>FK hacia el contrato de servicio que origina la facturación recurrente.</summary>
        public Guid ContratoServicioId { get; set; }

        /// <summary>FK hacia la serie de numeración que se usará al generar las facturas.</summary>
        public Guid SerieFacturaId { get; set; }

        /// <summary>Frecuencia de generación de facturas: mensual, trimestral, anual, etc.</summary>
        public FrecuenciaFacturacion Frecuencia { get; set; } = FrecuenciaFacturacion.Mensual;

        /// <summary>
        /// Día del mes en que se genera la factura (1-28).
        /// Ejemplo: 1 = se genera el día 1 de cada mes.
        /// Se limita a 28 para evitar problemas con febrero y meses cortos.
        /// </summary>
        public int DiaDeMesEmision { get; set; } = 1;

        /// <summary>
        /// Importe base de la factura recurrente en la moneda base del sistema.
        /// Es el importe neto (sin IVA) del período facturado según el contrato.
        /// </summary>
        public decimal ImporteBase { get; set; }

        /// <summary>
        /// Indica si las facturas se generan automáticamente por Hangfire
        /// o solo como borrador para revisión manual antes de emitir.
        /// True = generación automática directa. False = genera borrador para revisión.
        /// </summary>
        public bool GenerarAutomaticamente { get; set; } = true;

        /// <summary>Indica si la programación está activa y generará facturas.</summary>
        public bool EstaActiva { get; set; } = true;

        /// <summary>
        /// Fecha de inicio de la programación (primera factura a partir de esta fecha).
        /// </summary>
        public DateOnly FechaInicio { get; set; }

        /// <summary>
        /// Fecha de fin de la programación. Nulo si es indefinida (renovación automática).
        /// </summary>
        public DateOnly? FechaFin { get; set; }

        /// <summary>
        /// Fecha calculada de la próxima emisión de factura.
        /// Actualizada automáticamente tras cada generación exitosa.
        /// </summary>
        public DateOnly? ProximaFechaEmision { get; set; }

        /// <summary>Fecha de la última factura generada por esta programación.</summary>
        public DateOnly? FechaUltimaEmision { get; set; }

        /// <summary>Número total de facturas generadas por esta programación.</summary>
        public int TotalFacturasGeneradas { get; set; } = 0;

        /// <summary>Descripción o concepto que aparecerá en la línea de las facturas generadas.</summary>
        public string? ConceptoFactura { get; set; }

        /// <summary>Notas internas sobre la programación de facturación.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Serie de facturación usada para las facturas generadas.</summary>
        public virtual SerieFactura? SerieFactura { get; set; }
    }
}
