using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Empleado de la empresa de limpieza y servicios sociales.
    /// Puede estar vinculado a un Usuario del sistema (para acceso a la PWA de fichaje
    /// y ejecución de órdenes) o existir como registro RRHH puro sin acceso al sistema.
    ///
    /// El SupervisorId establece la jerarquía organizativa para aprobaciones
    /// de ausencias, vacaciones y gestión de equipos de trabajo.
    ///
    /// Soporta anonimización GDPR completa: datos personales anonimizables
    /// con preservación de registros laborales, contratos y nóminas por obligación legal.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NombreCompleto).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.NIF).HasMaxLength(20);
    ///   builder.Property(x => x.Email).HasMaxLength(256);
    ///   builder.Property(x => x.NumeroSeguridadSocial).HasMaxLength(30);
    ///   builder.HasIndex(x => x.UsuarioId).IsUnique().HasFilter("\"UsuarioId\" IS NOT NULL");
    ///   builder.HasIndex(x => x.NIF).HasFilter("\"NIF\" IS NOT NULL");
    ///   builder.HasIndex(x => x.NumeroSeguridadSocial).HasFilter("\"NumeroSeguridadSocial\" IS NOT NULL");
    ///   builder.HasIndex(x => x.EstaDisponible);
    ///   builder.HasIndex(x => x.TokenAnonimizacion).HasFilter("\"TokenAnonimizacion\" IS NOT NULL");
    /// </remarks>
    public class Empleado : EntidadBase
    {
        /// <summary>
        /// Identificador del usuario del sistema vinculado a este empleado.
        /// Nulo si el empleado no tiene acceso al sistema (solo registro RRHH).
        /// FK hacia Usuarios. Índice único (1 empleado por usuario).
        /// </summary>
        public Guid? UsuarioId { get; set; }

        /// <summary>FK hacia el supervisor directo de este empleado en la jerarquía organizativa.</summary>
        public Guid? SupervisorId { get; set; }

        // ── Datos personales (anonimizables por GDPR) ────────────────────────

        /// <summary>
        /// Nombre completo del empleado (Nombre + Apellidos).
        /// Se anonimiza a "GDPR_DEL_{token}" en operaciones de anonimización GDPR.
        /// </summary>
        public string NombreCompleto { get; set; } = string.Empty;

        /// <summary>NIF, NIE o pasaporte del empleado. Se anonimiza en GDPR.</summary>
        public string? NIF { get; set; }

        /// <summary>Email personal del empleado. Se anonimiza en GDPR.</summary>
        public string? Email { get; set; }

        /// <summary>Teléfono del empleado. Se anonimiza en GDPR.</summary>
        public string? Telefono { get; set; }

        /// <summary>Teléfono móvil del empleado. Se anonimiza en GDPR.</summary>
        public string? TelefonoMovil { get; set; }

        /// <summary>Fecha de nacimiento del empleado. Se anonimiza en GDPR.</summary>
        public DateOnly? FechaNacimiento { get; set; }

        /// <summary>Nacionalidad del empleado. FK hacia País.</summary>
        public Guid? NacionalidadId { get; set; }

        // ── Datos laborales ──────────────────────────────────────────────────

        /// <summary>Número de afiliación a la Seguridad Social española. Único por empleado.</summary>
        public string? NumeroSeguridadSocial { get; set; }

        /// <summary>
        /// Número de empleado interno asignado por la empresa.
        /// Usado en nóminas, identificación interna y control de acceso.
        /// </summary>
        public string? NumeroEmpleado { get; set; }

        /// <summary>Fecha de incorporación del empleado a la empresa.</summary>
        public DateOnly? FechaAlta { get; set; }

        /// <summary>
        /// Fecha de baja del empleado. Nulo si el empleado sigue activo.
        /// </summary>
        public DateOnly? FechaBaja { get; set; }

        /// <summary>Categoría profesional del empleado según convenio colectivo.</summary>
        public string? CategoriaProfesional { get; set; }

        /// <summary>Departamento o área funcional a la que pertenece el empleado.</summary>
        public string? Departamento { get; set; }

        /// <summary>
        /// Salario bruto mensual base del empleado en la moneda base del sistema.
        /// Se usa en el cálculo de nóminas y análisis de costos de servicio.
        /// </summary>
        public decimal? SalarioBrutoMensual { get; set; }

        /// <summary>Número de cuenta bancaria IBAN del empleado para el pago de nóminas.</summary>
        public string? IBANNomina { get; set; }

        // ── Disponibilidad operativa ─────────────────────────────────────────

        /// <summary>
        /// Indica si el empleado está disponible para ser asignado a órdenes de servicio.
        /// False durante ausencias, vacaciones o bajas médicas.
        /// Actualizado automáticamente por el servicio de ausencias y vacaciones.
        /// </summary>
        public bool EstaDisponible { get; set; } = true;

        /// <summary>
        /// Zona de servicio principal a la que está asignado el empleado.
        /// FK hacia ZonaServicio.
        /// </summary>
        public Guid? ZonaServicioPrincipalId { get; set; }

        /// <summary>
        /// Ruta relativa de la fotografía del empleado en el servidor.
        /// Mostrada en el perfil, en la PWA y en documentos de identificación.
        /// </summary>
        public string? RutaFotografia { get; set; }

        /// <summary>
        /// Nota u observación interna sobre el empleado visible solo para RRHH.
        /// No visible en la PWA ni en documentos generados.
        /// </summary>
        public string? NotasInternas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Supervisor directo de este empleado.</summary>
        public virtual Empleado? Supervisor { get; set; }

        /// <summary>Empleados supervisados directamente por este empleado.</summary>
        public virtual ICollection<Empleado> Subordinados { get; set; }
            = new List<Empleado>();

        /// <summary>Asignaciones de este empleado a equipos de trabajo.</summary>
        public virtual ICollection<EmpleadoEquipo> EquiposAsignado { get; set; }
            = new List<EmpleadoEquipo>();

        /// <summary>Asignaciones activas e históricas de este empleado a clientes.</summary>
        public virtual ICollection<AsignacionClienteEmpleado> AsignacionesCliente { get; set; }
            = new List<AsignacionClienteEmpleado>();

        /// <summary>Historial de interacciones comerciales realizadas por este empleado.</summary>
        public virtual ICollection<InteraccionComercial> Interacciones { get; set; }
            = new List<InteraccionComercial>();
    }
}
