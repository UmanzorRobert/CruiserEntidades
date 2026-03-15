using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Desglose del coste real de una orden de servicio completada.
    /// Se calcula automáticamente al completar la orden y puede ser ajustado
    /// manualmente por el responsable de operaciones antes de su facturación.
    ///
    /// CostoManoObra = Σ (HorasTrabajadas × CostoPorHoraEmpleado) por empleado asignado.
    /// CostoMateriales = Σ materiales utilizados en la orden × PrecioCoste del producto.
    /// CostoVehiculo = kilómetros recorridos × coste por km (basado en combustible).
    /// CostoKilometraje = km recorridos × tarifa km de la empresa.
    /// CostosImprevistos = gastos adicionales aprobados (GastoOrdenServicio).
    ///
    /// MargenBruto = PrecioFacturado - CostoTotal.
    /// PorcentajeMargen = (MargenBruto / PrecioFacturado) × 100.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => x.OrdenServicioId).IsUnique();
    ///   builder.Property(x => x.CostoManoObra).HasPrecision(18, 2);
    ///   builder.Property(x => x.CostoMateriales).HasPrecision(18, 2);
    ///   builder.Property(x => x.CostoVehiculo).HasPrecision(18, 2);
    ///   builder.Property(x => x.CostoKilometraje).HasPrecision(18, 2);
    ///   builder.Property(x => x.CostosImprevistos).HasPrecision(18, 2);
    ///   builder.Property(x => x.CostoTotal).HasPrecision(18, 2);
    ///   builder.Property(x => x.PrecioFacturado).HasPrecision(18, 2);
    ///   builder.Property(x => x.MargenBruto).HasPrecision(18, 2);
    ///   builder.Property(x => x.PorcentajeMargen).HasPrecision(7, 4);
    /// </remarks>
    public class CostoServicio : EntidadBase
    {
        /// <summary>FK hacia la orden de servicio a la que pertenece este cálculo de costes. Índice único.</summary>
        public Guid OrdenServicioId { get; set; }

        /// <summary>Coste de mano de obra: suma de horas de cada empleado × su coste/hora.</summary>
        public decimal CostoManoObra { get; set; } = 0m;

        /// <summary>Coste de materiales utilizados en el servicio según el inventario.</summary>
        public decimal CostoMateriales { get; set; } = 0m;

        /// <summary>Coste de uso del vehículo de empresa (amortización + combustible prorrateado).</summary>
        public decimal CostoVehiculo { get; set; } = 0m;

        /// <summary>Coste por kilómetros recorridos según la tarifa interna de la empresa.</summary>
        public decimal CostoKilometraje { get; set; } = 0m;

        /// <summary>Costes imprevistos aprobados: gastos no planificados durante el servicio.</summary>
        public decimal CostosImprevistos { get; set; } = 0m;

        /// <summary>Coste total: suma de todos los conceptos de coste anteriores.</summary>
        public decimal CostoTotal { get; set; } = 0m;

        /// <summary>
        /// Precio facturado al cliente por este servicio (importe de la línea de factura sin IVA).
        /// Nulo hasta que se emite la factura de la orden.
        /// </summary>
        public decimal? PrecioFacturado { get; set; }

        /// <summary>Margen bruto: PrecioFacturado - CostoTotal. Nulo hasta facturación.</summary>
        public decimal? MargenBruto { get; set; }

        /// <summary>
        /// Porcentaje de margen: (MargenBruto / PrecioFacturado) × 100.
        /// Nulo hasta facturación. Negativo indica pérdida en el servicio.
        /// </summary>
        public decimal? PorcentajeMargen { get; set; }

        /// <summary>Indica si el cálculo fue ajustado manualmente por el responsable.</summary>
        public bool EsAjusteManual { get; set; } = false;

        /// <summary>Notas sobre los ajustes realizados o la justificación de costes imprevistos.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Orden de servicio a la que pertenece este desglose de costes.</summary>
        public virtual OrdenServicio? OrdenServicio { get; set; }
    }
}
