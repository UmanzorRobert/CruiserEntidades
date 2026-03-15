using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Flota
{
    /// <summary>
    /// Registro de una intervención de mantenimiento realizada en un vehículo de la flota.
    /// Incluye mantenimientos preventivos programados, reparaciones correctivas,
    /// ITVs, revisiones de seguro y lavados.
    ///
    /// El sistema usa ProximoMantenimientoKm y ProximoMantenimientoFecha para
    /// generar AlertaVencimiento automáticas que avisan al responsable de flota
    /// cuando el vehículo está próximo a necesitar la siguiente intervención.
    ///
    /// La RutaFactura permite adjuntar el PDF de la factura del taller para
    /// el control de gastos de mantenimiento de la flota.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.CostoTotal).HasPrecision(18, 2);
    ///   builder.HasIndex(x => new { x.VehiculoEmpresaId, x.FechaMantenimiento });
    ///   builder.HasIndex(x => x.TipoMantenimiento);
    ///   builder.HasIndex(x => x.ProximoMantenimientoFecha)
    ///          .HasFilter("\"ProximoMantenimientoFecha\" IS NOT NULL");
    /// </remarks>
    public class MantenimientoVehiculo : EntidadBase
    {
        /// <summary>FK hacia el vehículo al que se realizó el mantenimiento.</summary>
        public Guid VehiculoEmpresaId { get; set; }

        /// <summary>Tipo de mantenimiento o intervención técnica realizada.</summary>
        public TipoMantenimiento TipoMantenimiento { get; set; }

        /// <summary>Fecha en que se realizó el mantenimiento.</summary>
        public DateOnly FechaMantenimiento { get; set; }

        /// <summary>Kilómetros del vehículo en el momento del mantenimiento.</summary>
        public int? KilometrosAlMantenimiento { get; set; }

        /// <summary>Nombre o razón social del taller o empresa que realizó el mantenimiento.</summary>
        public string? Taller { get; set; }

        /// <summary>Descripción de los trabajos realizados y piezas sustituidas.</summary>
        public string? DescripcionTrabajos { get; set; }

        /// <summary>
        /// Coste total del mantenimiento incluyendo piezas y mano de obra (sin IVA).
        /// </summary>
        public decimal CostoTotal { get; set; } = 0m;

        /// <summary>Ruta relativa de la factura del taller adjunta al mantenimiento.</summary>
        public string? RutaFactura { get; set; }

        /// <summary>
        /// Kilómetros previstos para el próximo mantenimiento del mismo tipo.
        /// Ejemplo: para revisión de aceite: KilometrosAlMantenimiento + 15.000 km.
        /// Genera AlertaVencimiento cuando se acerca el umbral.
        /// </summary>
        public int? ProximoMantenimientoKm { get; set; }

        /// <summary>
        /// Fecha prevista para el próximo mantenimiento del mismo tipo.
        /// Ejemplo: próxima ITV, próxima revisión anual.
        /// Genera AlertaVencimiento cuando se acerca la fecha.
        /// </summary>
        public DateOnly? ProximoMantenimientoFecha { get; set; }

        /// <summary>Notas adicionales sobre el mantenimiento o recomendaciones del taller.</summary>
        public string? Notas { get; set; }

        /// <summary>FK hacia el empleado responsable de flota que registró el mantenimiento.</summary>
        public Guid RegistradoPorId { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Vehículo al que se realizó el mantenimiento.</summary>
        public virtual VehiculoEmpresa? VehiculoEmpresa { get; set; }
    }
}
