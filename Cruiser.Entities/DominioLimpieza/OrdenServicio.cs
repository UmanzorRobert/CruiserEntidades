using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Orden de servicio de limpieza. Representa una visita concreta a un cliente
    /// para ejecutar el servicio contratado en una fecha y hora programadas.
    ///
    /// Puede originarse desde:
    /// - Un ContratoServicio (generación automática por Hangfire según la frecuencia).
    /// - Una CitaPrevia confirmada por el equipo.
    /// - Creación manual por el responsable de operaciones.
    ///
    /// MODO OFFLINE (PWA):
    /// ModoCompletado=Offline indica que el empleado completó la orden sin conexión
    /// y los datos se sincronizaron posteriormente. FechaCompletadoLocal almacena
    /// la hora real del dispositivo para trazabilidad.
    ///
    /// FIRMA DIGITAL:
    /// Al finalizar el servicio, el cliente firma digitalmente en la pantalla
    /// del empleado. FirmaDigitalId vincula la firma a la orden.
    ///
    /// VERIFACTU:
    /// Al facturar la orden, la Factura resultante incluye HashFactura encadenado.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NumeroOrden).IsRequired().HasMaxLength(30);
    ///   builder.HasIndex(x => x.NumeroOrden).IsUnique();
    ///   builder.HasIndex(x => new { x.ClienteId, x.FechaProgramada });
    ///   builder.HasIndex(x => new { x.Estado, x.FechaProgramada });
    ///   builder.HasIndex(x => x.ContratoServicioId).HasFilter("\"ContratoServicioId\" IS NOT NULL");
    ///   builder.HasIndex(x => x.EquipoTrabajoId).HasFilter("\"EquipoTrabajoId\" IS NOT NULL");
    ///   builder.HasIndex(x => x.FechaProgramada);
    /// </remarks>
    public class OrdenServicio : EntidadBase
    {
        /// <summary>
        /// Número único de la orden de servicio. Formato: "OS-2026-00001".
        /// Generado automáticamente por IOrdenServicioService.
        /// </summary>
        public string NumeroOrden { get; set; } = string.Empty;

        /// <summary>FK hacia el contrato de servicio que originó esta orden. Nulo si es manual.</summary>
        public Guid? ContratoServicioId { get; set; }

        /// <summary>FK hacia el cliente al que se presta el servicio.</summary>
        public Guid ClienteId { get; set; }

        /// <summary>FK hacia el tipo de servicio a ejecutar.</summary>
        public Guid TipoServicioId { get; set; }

        /// <summary>FK hacia el equipo de trabajo asignado a la orden.</summary>
        public Guid? EquipoTrabajoId { get; set; }

        /// <summary>FK hacia el evento del calendario vinculado a esta orden.</summary>
        public Guid? EventoCalendarioId { get; set; }

        /// <summary>FK hacia la firma digital del cliente obtenida al finalizar el servicio.</summary>
        public Guid? FirmaDigitalId { get; set; }

        /// <summary>FK hacia la factura emitida por esta orden. Nulo hasta su facturación.</summary>
        public Guid? FacturaId { get; set; }

        // ── Programación ─────────────────────────────────────────────────────

        /// <summary>Fecha y hora UTC programadas para el inicio del servicio.</summary>
        public DateTime FechaProgramada { get; set; }

        /// <summary>Hora de inicio real del servicio registrada por el empleado desde la PWA.</summary>
        public DateTime? FechaHoraInicioReal { get; set; }

        /// <summary>Hora de fin real del servicio registrada por el empleado desde la PWA.</summary>
        public DateTime? FechaHoraFinReal { get; set; }

        /// <summary>Duración estimada del servicio en minutos según el TipoServicio.</summary>
        public int DuracionEstimadaMinutos { get; set; } = 0;

        /// <summary>
        /// Horas reales trabajadas calculadas: (FechaHoraFinReal - FechaHoraInicioReal) en horas.
        /// </summary>
        public decimal? HorasRealesTrabajadas { get; set; }

        // ── Estado y prioridad ────────────────────────────────────────────────

        /// <summary>Estado actual de la orden en su ciclo de vida.</summary>
        public EstadoOrdenServicio Estado { get; set; } = EstadoOrdenServicio.Pendiente;

        /// <summary>Nivel de prioridad de la orden para la planificación del equipo.</summary>
        public PrioridadServicio PrioridadServicio { get; set; } = PrioridadServicio.Normal;

        // ── Modo de completado (PWA Offline) ──────────────────────────────────

        /// <summary>
        /// Modo en que se completó la orden: Online (con conexión) u Offline (sin conexión).
        /// Nulo hasta que la orden es completada.
        /// </summary>
        public ModoCompleto? ModoCompletado { get; set; }

        /// <summary>
        /// Fecha y hora LOCAL del dispositivo del empleado en que se completó la orden offline.
        /// Puede diferir de FechaHoraFinReal si el dispositivo tenía reloj desajustado.
        /// </summary>
        public DateTime? FechaCompletadoLocal { get; set; }

        // ── Instrucciones ────────────────────────────────────────────────────

        /// <summary>Instrucciones especiales para el equipo antes de iniciar el servicio.</summary>
        public string? InstruccionesEspeciales { get; set; }

        /// <summary>Observaciones registradas por el empleado al finalizar el servicio.</summary>
        public string? ObservacionesFinales { get; set; }

        /// <summary>FK hacia el usuario (responsable de operaciones) que creó la orden.</summary>
        public Guid CreadoPorId { get; set; }

        /// <summary>FK hacia el supervisor que validó la orden completada.</summary>
        public Guid? ValidadoPorId { get; set; }

        /// <summary>Fecha y hora UTC de la validación de la orden.</summary>
        public DateTime? FechaValidacion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Contrato de servicio que originó esta orden.</summary>
        public virtual ContratoServicio? ContratoServicio { get; set; }

        /// <summary>Cliente al que se presta el servicio.</summary>
        public virtual Comercial.Cliente? Cliente { get; set; }

        /// <summary>Tipo de servicio a ejecutar.</summary>
        public virtual TipoServicio? TipoServicio { get; set; }

        /// <summary>Empleados asignados a la orden y sus horas registradas.</summary>
        public virtual ICollection<EmpleadoOrdenServicio> EmpleadosAsignados { get; set; }
            = new List<EmpleadoOrdenServicio>();

        /// <summary>Historial de cambios de estado de la orden.</summary>
        public virtual ICollection<SeguimientoOrdenServicio> Seguimientos { get; set; }
            = new List<SeguimientoOrdenServicio>();

        /// <summary>Fotografías del servicio capturadas por el equipo.</summary>
        public virtual ICollection<FotografiaServicio> Fotografias { get; set; }
            = new List<FotografiaServicio>();

        /// <summary>Checklist de la orden instanciado a partir de la plantilla.</summary>
        public virtual ChecklistOrdenServicio? Checklist { get; set; }

        /// <summary>Costo calculado del servicio (mano de obra, materiales, vehículo).</summary>
        public virtual CostoServicio? Costo { get; set; }
    }
}
