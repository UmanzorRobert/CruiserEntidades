using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.DominioLimpieza
{
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Incidencia registrada durante o después de la prestación de un servicio.
    /// Incluye daños, accidentes laborales, reclamaciones de clientes e incidentes de acceso.
    /// Las incidencias críticas generan notificaciones inmediatas al responsable de operaciones.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NumeroIncidencia).IsRequired().HasMaxLength(30);
    ///   builder.HasIndex(x => x.NumeroIncidencia).IsUnique();
    ///   builder.HasIndex(x => new { x.OrdenServicioId, x.Severidad });
    ///   builder.HasIndex(x => x.EstadoResolucion);
    ///   builder.Property(x => x.CostoEstimadoDano).HasPrecision(18, 2);
    /// </remarks>
    public class IncidenciaServicio : EntidadBase
    {
        public Guid OrdenServicioId { get; set; }
        public Guid EmpleadoReportaId { get; set; }

        /// <summary>Número único de la incidencia. Formato: "INC-2026-0001".</summary>
        public string NumeroIncidencia { get; set; } = string.Empty;

        public TipoIncidenciaServicio TipoIncidencia { get; set; }
        public SeveridadIncidencia Severidad { get; set; } = SeveridadIncidencia.Baja;

        /// <summary>Descripción detallada de la incidencia y sus circunstancias.</summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>Estado de la resolución: Abierta, EnInvestigacion, Resuelta, Cerrada.</summary>
        public string EstadoResolucion { get; set; } = "Abierta";

        /// <summary>Acciones correctivas adoptadas para resolver la incidencia.</summary>
        public string? AccionesCorrectivas { get; set; }

        /// <summary>Indica si la incidencia requiere informe policial (robos, accidentes graves).</summary>
        public bool RequiereInformePolicial { get; set; } = false;

        /// <summary>Coste estimado de los daños materiales causados por la incidencia.</summary>
        public decimal? CostoEstimadoDano { get; set; }

        public virtual OrdenServicio? OrdenServicio { get; set; }
    }
}
