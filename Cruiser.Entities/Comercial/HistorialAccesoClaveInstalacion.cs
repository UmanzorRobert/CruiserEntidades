using System;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Registro de auditoría de cada consulta o uso de una clave de acceso a instalaciones.
    /// Cada vez que un empleado solicita ver el valor de una ClavesAccesoInstalacion,
    /// se crea un registro inmutable en este historial.
    ///
    /// Permite a la empresa saber quién accedió a qué credencial y cuándo,
    /// cumpliendo con los requisitos de auditoría y seguridad de la información.
    ///
    /// NO hereda de EntidadBase: log append-only, no modificable.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.TipoAccion).HasMaxLength(50);
    ///   builder.HasIndex(x => x.ClavesAccesoInstalacionId);
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.FechaAcceso });
    /// </remarks>
    public class HistorialAccesoClaveInstalacion
    {
        /// <summary>Identificador único del registro de acceso.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia la clave de acceso que fue consultada o usada.</summary>
        public Guid ClavesAccesoInstalacionId { get; set; }

        /// <summary>FK hacia el empleado que realizó la consulta o uso de la clave.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>Fecha y hora UTC en que se realizó el acceso a la clave.</summary>
        public DateTime FechaAcceso { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Tipo de acción realizada sobre la clave.
        /// Ejemplos: "Consulta", "Uso en Servicio", "Devolución", "Cambio de Responsable".
        /// </summary>
        public string TipoAccion { get; set; } = "Consulta";

        /// <summary>
        /// FK hacia la orden de servicio en la que se usó la clave.
        /// Nulo para consultas sin orden asociada.
        /// </summary>
        public Guid? OrdenServicioId { get; set; }

        /// <summary>Dirección IP desde la que se realizó la consulta (trazabilidad de seguridad).</summary>
        public string? DireccionIP { get; set; }

        /// <summary>Observaciones sobre el uso de la clave en este acceso.</summary>
        public string? Observaciones { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Clave de acceso que fue consultada o usada.</summary>
        public virtual ClavesAccesoInstalacion? ClavesAccesoInstalacion { get; set; }

        /// <summary>Empleado que realizó el acceso.</summary>
        public virtual Empleado? Empleado { get; set; }
    }
}
