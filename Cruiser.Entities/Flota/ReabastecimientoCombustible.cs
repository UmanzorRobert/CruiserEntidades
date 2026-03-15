using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Flota
{
    /// <summary>
    /// Registro detallado de un repostaje de combustible de un vehículo de la flota.
    /// Almacena los litros, el precio por litro y la gasolinera para calcular
    /// el coste por kilómetro y analizar el consumo real del vehículo.
    ///
    /// La integración con el análisis de consumo compara el consumo registrado
    /// (litros / km recorridos) con el consumo medio del fabricante para detectar
    /// anomalías que puedan indicar avería mecánica o uso incorrecto del vehículo.
    ///
    /// El RutaTicket permite adjuntar la fotografía del ticket de la gasolinera
    /// capturada desde la cámara de la PWA del empleado.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Litros).HasPrecision(10, 3);
    ///   builder.Property(x => x.PrecioLitro).HasPrecision(10, 4);
    ///   builder.Property(x => x.ImporteTotal).HasPrecision(18, 2);
    ///   builder.HasIndex(x => new { x.VehiculoEmpresaId, x.FechaReabastecimiento });
    ///   builder.HasIndex(x => x.EmpleadoId);
    /// </remarks>
    public class ReabastecimientoCombustible : EntidadBase
    {
        /// <summary>FK hacia el vehículo repostado.</summary>
        public Guid VehiculoEmpresaId { get; set; }

        /// <summary>FK hacia el empleado que realizó el repostaje.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>Fecha del repostaje.</summary>
        public DateOnly FechaReabastecimiento { get; set; }

        /// <summary>Litros de combustible repostados.</summary>
        public decimal Litros { get; set; }

        /// <summary>Precio por litro en el momento del repostaje.</summary>
        public decimal PrecioLitro { get; set; }

        /// <summary>Importe total del repostaje: Litros × PrecioLitro.</summary>
        public decimal ImporteTotal { get; set; }

        /// <summary>Kilómetros del contador en el momento del repostaje.</summary>
        public int KilometrosAlReabastecimiento { get; set; }

        /// <summary>Nombre o ubicación de la gasolinera donde se realizó el repostaje.</summary>
        public string? Gasolinera { get; set; }

        /// <summary>
        /// Indica si el depósito se llenó completamente.
        /// True permite calcular el consumo real entre repostajes completos consecutivos.
        /// </summary>
        public bool DepositoLleno { get; set; } = true;

        /// <summary>
        /// Consumo calculado entre este repostaje y el anterior (litros/100 km).
        /// Calculado automáticamente por IVehiculoService al registrar el repostaje.
        /// </summary>
        public decimal? ConsumoCalculadoL100km { get; set; }

        /// <summary>Ruta relativa del ticket de gasolinera adjunto (foto desde PWA).</summary>
        public string? RutaTicket { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Vehículo repostado.</summary>
        public virtual VehiculoEmpresa? VehiculoEmpresa { get; set; }

        /// <summary>Empleado que realizó el repostaje.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }
    }
}
