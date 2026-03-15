using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Flota
{
    /// <summary>
    /// Registro de un siniestro sufrido por un vehículo de la flota: accidentes de
    /// tráfico, robos, vandalismos, daños meteorológicos o incendios.
    ///
    /// El NumeroExpedienteSeguro es el identificador asignado por la compañía
    /// aseguradora al declarar el siniestro. Permite hacer seguimiento de la
    /// tramitación del expediente hasta su resolución.
    ///
    /// EstaResuelto=true indica que el expediente está cerrado, la reparación
    /// completada y el vehículo devuelto al estado operativo.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NumeroExpedienteSeguro).HasMaxLength(100);
    ///   builder.Property(x => x.CostoReparacion).HasPrecision(18, 2);
    ///   builder.Property(x => x.ImporteIndemnizacion).HasPrecision(18, 2);
    ///   builder.HasIndex(x => new { x.VehiculoEmpresaId, x.FechaSiniestro });
    ///   builder.HasIndex(x => x.TipoSiniestro);
    ///   builder.HasIndex(x => x.EstaResuelto);
    /// </remarks>
    public class SiniestroVehiculo : EntidadBase
    {
        /// <summary>FK hacia el vehículo que sufrió el siniestro.</summary>
        public Guid VehiculoEmpresaId { get; set; }

        /// <summary>FK hacia el empleado conductor del vehículo en el momento del siniestro.</summary>
        public Guid? EmpleadoConductorId { get; set; }

        /// <summary>Tipo de siniestro sufrido.</summary>
        public TipoSiniestro TipoSiniestro { get; set; }

        /// <summary>Fecha en que ocurrió el siniestro.</summary>
        public DateOnly FechaSiniestro { get; set; }

        /// <summary>Descripción detallada del siniestro: circunstancias, lugar, partes implicadas.</summary>
        public string? Descripcion { get; set; }

        /// <summary>Lugar donde ocurrió el siniestro.</summary>
        public string? Lugar { get; set; }

        /// <summary>
        /// Número de expediente asignado por la compañía aseguradora al declarar el siniestro.
        /// Nulo hasta que se formaliza la declaración con la aseguradora.
        /// </summary>
        public string? NumeroExpedienteSeguro { get; set; }

        /// <summary>Coste estimado o definitivo de la reparación del vehículo.</summary>
        public decimal? CostoReparacion { get; set; }

        /// <summary>Importe de indemnización recibido de la aseguradora.</summary>
        public decimal? ImporteIndemnizacion { get; set; }

        /// <summary>Fecha de entrada del vehículo al taller para reparación.</summary>
        public DateOnly? FechaEntradaTaller { get; set; }

        /// <summary>Fecha en que el vehículo salió del taller y fue devuelto a la flota.</summary>
        public DateOnly? FechaSalidaTaller { get; set; }

        /// <summary>
        /// Indica si el expediente del siniestro está completamente resuelto:
        /// reparación completada, indemnización recibida y vehículo operativo.
        /// </summary>
        public bool EstaResuelto { get; set; } = false;

        /// <summary>Ruta relativa del informe del siniestro o del parte de accidente.</summary>
        public string? RutaInforme { get; set; }

        /// <summary>Notas sobre la tramitación del siniestro y las gestiones realizadas.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Vehículo que sufrió el siniestro.</summary>
        public virtual VehiculoEmpresa? VehiculoEmpresa { get; set; }

        /// <summary>Empleado conductor del vehículo en el momento del siniestro.</summary>
        public virtual Comercial.Empleado? EmpleadoConductor { get; set; }
    }
}
