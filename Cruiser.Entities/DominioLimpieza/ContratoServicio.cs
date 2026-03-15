using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Contrato de prestación de servicios de limpieza firmado con un cliente.
    /// Es el eje central del módulo de dominio de limpieza: a partir del contrato
    /// se generan las órdenes de servicio recurrentes y las facturas periódicas.
    ///
    /// FRECUENCIA Y PROGRAMACIÓN:
    /// DiaServicioJSON almacena los días de la semana en que se presta el servicio:
    /// Para semanal: [1, 3, 5] (lunes, miércoles, viernes).
    /// Para mensual: [1, 15] (días 1 y 15 del mes).
    ///
    /// RENOVACIÓN AUTOMÁTICA:
    /// Si RenovacionAutomatica=true, Hangfire genera un nuevo contrato (sucesor)
    /// 30 días antes del vencimiento y notifica al responsable para su revisión.
    ///
    /// CLÁUSULA DE PENALIZACIÓN:
    /// Texto legal de la penalización por rescisión anticipada del contrato.
    /// Incluido en el PDF del contrato generado con QuestPDF.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NumeroContrato).IsRequired().HasMaxLength(30);
    ///   builder.Property(x => x.ImporteMensual).HasPrecision(18, 2);
    ///   builder.Property(x => x.DiaServicioJSON).HasColumnType("jsonb");
    ///   builder.HasIndex(x => x.NumeroContrato).IsUnique();
    ///   builder.HasIndex(x => new { x.ClienteId, x.EstadoContrato });
    ///   builder.HasIndex(x => x.FechaFin).HasFilter("\"FechaFin\" IS NOT NULL");
    ///   builder.HasIndex(x => x.DiaServicioJSON).HasMethod("gin");
    /// </remarks>
    public class ContratoServicio : EntidadBase
    {
        /// <summary>
        /// Número único del contrato. Formato: "CON-2026-0001".
        /// Generado automáticamente por IContratoServicioService al crear el contrato.
        /// </summary>
        public string NumeroContrato { get; set; } = string.Empty;

        /// <summary>FK hacia el cliente con el que se firma el contrato.</summary>
        public Guid ClienteId { get; set; }

        /// <summary>FK hacia el tipo de servicio principal contratado.</summary>
        public Guid TipoServicioId { get; set; }

        /// <summary>FK hacia el equipo de trabajo asignado por defecto para este contrato.</summary>
        public Guid? EquipoTrabajoId { get; set; }

        /// <summary>FK hacia el empleado responsable comercial del contrato.</summary>
        public Guid? EmpleadoResponsableId { get; set; }

        /// <summary>Estado del ciclo de vida del contrato.</summary>
        public EstadoContrato EstadoContrato { get; set; } = EstadoContrato.Borrador;

        // ── Fechas ───────────────────────────────────────────────────────────

        /// <summary>Fecha de inicio de la vigencia del contrato.</summary>
        public DateOnly FechaInicio { get; set; }

        /// <summary>
        /// Fecha de fin de la vigencia del contrato.
        /// Nulo para contratos indefinidos sin fecha de vencimiento pactada.
        /// </summary>
        public DateOnly? FechaFin { get; set; }

        /// <summary>Fecha de firma del contrato por ambas partes.</summary>
        public DateOnly? FechaFirma { get; set; }

        // ── Condiciones económicas ────────────────────────────────────────────

        /// <summary>Modalidad de pago: mensual, trimestral, semestral, anual.</summary>
        public string ModalidadPago { get; set; } = "Mensual";

        /// <summary>Importe mensual del servicio sin IVA según el contrato.</summary>
        public decimal ImporteMensual { get; set; } = 0m;

        // ── Programación del servicio ─────────────────────────────────────────

        /// <summary>Frecuencia de prestación del servicio contratado.</summary>
        public FrecuenciaServicio FrecuenciaServicio { get; set; } = FrecuenciaServicio.Semanal;

        /// <summary>
        /// Días en que se presta el servicio en formato JSONB.
        /// Semanal: array de int con números de día ISO (1=Lunes ... 7=Domingo).
        /// Mensual: array de int con días del mes (1-28).
        /// Ejemplo semanal: [1, 3, 5] para Lunes, Miércoles y Viernes.
        /// </summary>
        public string? DiaServicioJSON { get; set; }

        /// <summary>Hora preferida de inicio del servicio en las instalaciones del cliente.</summary>
        public TimeOnly? HoraServicioPreferida { get; set; }

        /// <summary>Duración estimada de cada visita en minutos.</summary>
        public int DuracionEstimadaMinutos { get; set; } = 120;

        // ── Renovación y cláusulas ────────────────────────────────────────────

        /// <summary>
        /// Indica si el contrato se renueva automáticamente al vencer.
        /// Hangfire genera el contrato sucesor 30 días antes del vencimiento.
        /// </summary>
        public bool RenovacionAutomatica { get; set; } = false;

        /// <summary>
        /// Período de preaviso en días que debe dar el cliente para rescindir el contrato
        /// sin incurrir en penalización.
        /// </summary>
        public int DiasPreaviso { get; set; } = 30;

        /// <summary>Texto de la cláusula de penalización por rescisión anticipada.</summary>
        public string? ClausulaPenalizacion { get; set; }

        /// <summary>
        /// FK hacia el contrato predecesor del que este es una renovación.
        /// Nulo para contratos originales.
        /// </summary>
        public Guid? ContratoPredecesorId { get; set; }

        /// <summary>Notas internas sobre el contrato no visibles para el cliente.</summary>
        public string? NotasInternas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Cliente con el que se firma el contrato.</summary>
        public virtual Comercial.Cliente? Cliente { get; set; }

        /// <summary>Tipo de servicio principal contratado.</summary>
        public virtual TipoServicio? TipoServicio { get; set; }

        /// <summary>Historial de cambios de estado del contrato.</summary>
        public virtual ICollection<HistorialEstadoContrato> HistorialEstados { get; set; }
            = new List<HistorialEstadoContrato>();

        /// <summary>Órdenes de servicio generadas a partir de este contrato.</summary>
        public virtual ICollection<OrdenServicio> OrdenesServicio { get; set; }
            = new List<OrdenServicio>();
    }
}
