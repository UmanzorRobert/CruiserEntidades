using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Flota
{
    /// <summary>
    /// Vehículo de la flota de empresa utilizado para el transporte del personal
    /// y material de limpieza a las instalaciones de los clientes.
    ///
    /// Gestiona el ciclo de vida del vehículo: estado operativo, kilómetros actuales,
    /// fechas de vencimiento de ITV y seguro, y el conductor habitual asignado.
    ///
    /// Las AlertaVencimiento automáticas se generan cuando la ITV o el seguro
    /// están próximos a vencer (configurables en ConfiguracionNotificacion).
    ///
    /// El CodigoQR o código de barras impreso en el vehículo permite al empleado
    /// escanearlo desde la PWA para confirmar la asignación al inicio de la ruta.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Matricula).IsRequired().HasMaxLength(15);
    ///   builder.Property(x => x.NumeroPolizaSeguro).HasMaxLength(100);
    ///   builder.Property(x => x.NumeroVIN).HasMaxLength(17);
    ///   builder.HasIndex(x => x.Matricula).IsUnique();
    ///   builder.HasIndex(x => x.Estado);
    ///   builder.HasIndex(x => x.FechaVencimientoITV);
    ///   builder.HasIndex(x => x.FechaVencimientoSeguro);
    /// </remarks>
    public class VehiculoEmpresa : EntidadBase
    {
        /// <summary>
        /// Matrícula del vehículo. Identificador único legal del vehículo en España.
        /// Formato: "1234 ABC" o equivalente europeo.
        /// </summary>
        public string Matricula { get; set; } = string.Empty;

        /// <summary>Tipo de vehículo: furgoneta, camión, turismo, etc.</summary>
        public TipoVehiculo TipoVehiculo { get; set; }

        /// <summary>Marca del vehículo. Ejemplo: "Ford", "Renault", "Mercedes-Benz".</summary>
        public string? Marca { get; set; }

        /// <summary>Modelo del vehículo. Ejemplo: "Transit", "Trafic", "Sprinter".</summary>
        public string? Modelo { get; set; }

        /// <summary>Año de fabricación del vehículo.</summary>
        public int? AnoFabricacion { get; set; }

        /// <summary>
        /// Número de Identificación del Vehículo (VIN/Bastidor).
        /// 17 caracteres alfanuméricos. Identificador único universal del vehículo.
        /// </summary>
        public string? NumeroVIN { get; set; }

        /// <summary>Color de la carrocería del vehículo.</summary>
        public string? Color { get; set; }

        // ── Capacidad ────────────────────────────────────────────────────────

        /// <summary>Número de plazas de pasajeros del vehículo (incluido el conductor).</summary>
        public int NumeroPlazas { get; set; } = 2;

        /// <summary>Capacidad máxima de carga en kilogramos.</summary>
        public decimal CapacidadCargaKg { get; set; } = 0m;

        /// <summary>Volumen de carga en metros cúbicos (para furgonetas y camiones).</summary>
        public decimal? VolumenCargaM3 { get; set; }

        // ── Estado y asignación ──────────────────────────────────────────────

        /// <summary>Estado operativo actual del vehículo.</summary>
        public EstadoVehiculo Estado { get; set; } = EstadoVehiculo.Disponible;

        /// <summary>
        /// FK hacia el empleado designado como conductor habitual del vehículo.
        /// Puede cambiar día a día mediante AsignacionVehiculo para rutas concretas.
        /// </summary>
        public Guid? ConductorHabitualId { get; set; }

        /// <summary>Kilómetros actuales del vehículo según el último registro.</summary>
        public int KilometrosActuales { get; set; } = 0;

        // ── Documentación legal ──────────────────────────────────────────────

        /// <summary>Fecha de la próxima ITV obligatoria. Genera AlertaVencimiento automática.</summary>
        public DateOnly FechaVencimientoITV { get; set; }

        /// <summary>Fecha de vencimiento de la póliza de seguro. Genera AlertaVencimiento automática.</summary>
        public DateOnly FechaVencimientoSeguro { get; set; }

        /// <summary>Número de póliza del seguro del vehículo.</summary>
        public string? NumeroPolizaSeguro { get; set; }

        /// <summary>Compañía aseguradora del vehículo.</summary>
        public string? CompaniaAseguradora { get; set; }

        /// <summary>Tipo de seguro contratado: terceros, todo riesgo, etc.</summary>
        public string? TipoSeguro { get; set; }

        // ── Consumo ──────────────────────────────────────────────────────────

        /// <summary>Consumo medio de combustible en litros por cada 100 kilómetros.</summary>
        public decimal? ConsumoCombustibleL100km { get; set; }

        /// <summary>Tipo de combustible: Gasolina, Diesel, GLP, Eléctrico, Híbrido.</summary>
        public string? TipoCombustible { get; set; }

        /// <summary>Ruta relativa de la fotografía del vehículo en el servidor.</summary>
        public string? RutaFotografia { get; set; }

        /// <summary>Notas o información adicional sobre el vehículo.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Conductor habitual del vehículo.</summary>
        public virtual Comercial.Empleado? ConductorHabitual { get; set; }

        /// <summary>Registros de mantenimiento del vehículo.</summary>
        public virtual ICollection<MantenimientoVehiculo> Mantenimientos { get; set; }
            = new List<MantenimientoVehiculo>();

        /// <summary>Asignaciones del vehículo a rutas y órdenes de servicio.</summary>
        public virtual ICollection<AsignacionVehiculo> Asignaciones { get; set; }
            = new List<AsignacionVehiculo>();

        /// <summary>Gastos asociados al vehículo (combustible, peajes, multas).</summary>
        public virtual ICollection<GastoVehiculo> Gastos { get; set; }
            = new List<GastoVehiculo>();

        /// <summary>Siniestros sufridos por el vehículo.</summary>
        public virtual ICollection<SiniestroVehiculo> Siniestros { get; set; }
            = new List<SiniestroVehiculo>();
    }
}