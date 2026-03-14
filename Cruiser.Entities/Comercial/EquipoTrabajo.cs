using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Equipo de trabajo compuesto por empleados que ejecutan conjuntamente
    /// las órdenes de servicio de limpieza.
    ///
    /// Cada equipo tiene un líder (EmpleadoId del líder definido en EmpleadoEquipo
    /// con RolEnEquipo = Lider), una zona de servicio principal y puede tener
    /// un vehículo de empresa asignado.
    ///
    /// La CapacidadMaxima define el número máximo de empleados simultáneos
    /// que puede integrar el equipo, usado en la planificación de órdenes.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(20);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    ///   builder.HasIndex(x => x.ZonaServicioId);
    ///   builder.HasIndex(x => x.EstaActivo);
    /// </remarks>
    public class EquipoTrabajo : EntidadBase
    {
        /// <summary>Código único del equipo. Ejemplo: "EQ-01", "EQ-NORTE-A".</summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo del equipo para la interfaz y reportes.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// FK hacia la zona de servicio principal a la que está asignado el equipo.
        /// El equipo puede atender órdenes fuera de su zona, pero es su área habitual.
        /// </summary>
        public Guid? ZonaServicioId { get; set; }

        /// <summary>
        /// FK hacia el vehículo de empresa asignado habitualmente a este equipo.
        /// Puede cambiar día a día mediante AsignacionVehiculo.
        /// </summary>
        public Guid? VehiculoEmpresaId { get; set; }

        /// <summary>
        /// Número máximo de empleados que puede integrar el equipo simultáneamente.
        /// Usado para validar la disponibilidad al asignar empleados a nuevas órdenes.
        /// </summary>
        public int CapacidadMaxima { get; set; } = 4;

        /// <summary>Notas o descripción sobre el equipo y su especialización.</summary>
        public string? Descripcion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Zona de servicio principal del equipo.</summary>
        public virtual ZonaServicio? ZonaServicio { get; set; }

        /// <summary>Miembros actuales e históricos del equipo.</summary>
        public virtual ICollection<EmpleadoEquipo> Miembros { get; set; }
            = new List<EmpleadoEquipo>();
    }
}
