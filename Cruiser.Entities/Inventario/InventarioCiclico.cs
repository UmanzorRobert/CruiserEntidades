using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Inventario
{
    /// <summary>
    /// Proceso de inventario cíclico (conteo físico) planificado para un almacén.
    /// Permite verificar periódicamente que el stock del sistema coincide con
    /// el stock físico real y generar ajustes automáticos cuando hay diferencias.
    ///
    /// El proceso completo:
    /// 1. Planificación: crear el inventario, seleccionar alcance (Total/Parcial/ABC).
    /// 2. Asignación: asignar empleados para el conteo físico.
    /// 3. Conteo: empleados escanean y cuentan con la PWA (funciona offline).
    /// 4. Revisión: supervisor revisa diferencias con justificaciones.
    /// 5. Ajuste: se generan MovimientosInventario de ajuste automáticamente.
    /// 6. Cierre: inventario completado y stock actualizado.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(50);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    ///   builder.HasIndex(x => new { x.AlmacenId, x.Estado });
    ///   builder.HasIndex(x => x.FechaInicio);
    /// </remarks>
    public class InventarioCiclico : EntidadBase
    {
        /// <summary>
        /// Código único del proceso de inventario.
        /// Generado automáticamente: "INV-2026-001", "INV-2026-002", etc.
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>FK hacia el almacén donde se realiza el conteo físico.</summary>
        public Guid AlmacenId { get; set; }

        /// <summary>FK hacia el empleado responsable de supervisar el inventario.</summary>
        public Guid ResponsableId { get; set; }

        /// <summary>Fecha y hora UTC de inicio del proceso de conteo físico.</summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha y hora UTC de finalización del proceso.
        /// Nulo si el inventario aún está en curso.
        /// </summary>
        public DateTime? FechaFin { get; set; }

        /// <summary>Estado actual del proceso de inventario cíclico.</summary>
        public EstadoInventarioCiclico Estado { get; set; } = EstadoInventarioCiclico.Planificado;

        /// <summary>Alcance del conteo: total, parcial o por clasificación ABC.</summary>
        public TipoConteoInventario TipoConteo { get; set; } = TipoConteoInventario.Total;

        /// <summary>Número total de productos contados en el proceso.</summary>
        public int ProductosContados { get; set; } = 0;

        /// <summary>
        /// Número de productos con diferencia entre el stock del sistema y el conteo físico.
        /// </summary>
        public int DiferenciasEncontradas { get; set; } = 0;

        /// <summary>
        /// Valor económico total de las diferencias encontradas en la moneda base.
        /// Positivo = exceso de stock. Negativo = faltante de stock.
        /// </summary>
        public decimal ValorDiferenciaTotal { get; set; } = 0m;

        /// <summary>
        /// Indica si los ajustes de stock han sido generados y aplicados.
        /// True después de completar el proceso de revisión y generar los MovimientosInventario.
        /// </summary>
        public bool AjustesAplicados { get; set; } = false;

        /// <summary>Observaciones generales sobre el proceso de inventario.</summary>
        public string? Observaciones { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Almacén donde se realiza el inventario.</summary>
        public virtual Almacen? Almacen { get; set; }

        /// <summary>Detalle línea a línea de los productos contados y sus diferencias.</summary>
        public virtual ICollection<DetalleInventarioCiclico> Detalles { get; set; }
            = new List<DetalleInventarioCiclico>();
    }
}
