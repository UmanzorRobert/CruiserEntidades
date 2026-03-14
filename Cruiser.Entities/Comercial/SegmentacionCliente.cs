using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Segmentación comercial RFM (Recencia, Frecuencia, Valor Monetario) asignada
    /// a un cliente. Determina el segmento del cliente (VIP, Estándar, EnRiesgo, etc.)
    /// y almacena la puntuación detallada de los tres componentes del análisis RFM.
    ///
    /// El análisis RFM se calcula automáticamente por el job de Hangfire
    /// CalcularRFMClientes (semanal) ejecutado desde ISegmentacionClienteService.
    ///
    /// Las asignaciones manuales (EsAutomatico=false) realizadas por el equipo
    /// comercial no son sobreescritas por el job automático, preservando
    /// la decisión manual del gestor de cuenta.
    ///
    /// La segmentación vigente es el último registro por cliente.
    /// Relación 1-a-1 con Cliente (siempre hay solo una segmentación activa).
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.PuntajeRFM).HasColumnType("jsonb");
    ///   builder.HasIndex(x => x.ClienteId).IsUnique();
    ///   builder.HasIndex(x => x.Segmento);
    ///   builder.HasIndex(x => x.PuntajeRFM).HasMethod("gin");
    ///
    ///   Estructura JSON de PuntajeRFM:
    ///   {
    ///     "puntajeRecencia": 4,      (1-5: 5=compró hace poco, 1=hace mucho)
    ///     "puntajeFrecuencia": 3,    (1-5: 5=muy frecuente, 1=poca frecuencia)
    ///     "puntajeMonetario": 5,     (1-5: 5=muy alto valor, 1=bajo valor)
    ///     "puntajeTotal": 12,        (suma de los tres, máx 15)
    ///     "diasDesdeUltimaCompra": 45,
    ///     "numeroPedidosUltimos12Meses": 8,
    ///     "totalFacturadoUltimos12Meses": 15000.50
    ///   }
    /// </remarks>
    public class SegmentacionCliente : EntidadBase
    {
        /// <summary>
        /// FK hacia el cliente segmentado.
        /// Índice único: solo una segmentación activa por cliente.
        /// </summary>
        public Guid ClienteId { get; set; }

        /// <summary>Segmento comercial asignado al cliente según el análisis RFM.</summary>
        public SegmentoCliente Segmento { get; set; } = SegmentoCliente.Nuevo;

        /// <summary>
        /// Datos detallados del análisis RFM en formato JSONB con índice GIN.
        /// Incluye las puntuaciones individuales de Recencia, Frecuencia y Valor Monetario,
        /// el puntaje total y los valores brutos que originaron la puntuación.
        /// </summary>
        public string? PuntajeRFM { get; set; }

        /// <summary>Fecha y hora UTC en que se realizó este análisis de segmentación.</summary>
        public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indica si la segmentación fue calculada automáticamente por el job de Hangfire
        /// o asignada manualmente por un empleado del equipo comercial.
        /// Las asignaciones manuales no se sobreescriben con el recálculo automático.
        /// </summary>
        public bool EsAutomatico { get; set; } = true;

        /// <summary>
        /// Segmento anterior antes de este cambio de segmentación.
        /// Permite analizar tendencias de mejora o empeoramiento de la cartera de clientes.
        /// </summary>
        public SegmentoCliente? SegmentoAnterior { get; set; }

        /// <summary>
        /// Motivo o justificación de la asignación manual de segmento.
        /// Solo aplica cuando EsAutomatico=false.
        /// </summary>
        public string? MotivoAsignacionManual { get; set; }

        /// <summary>FK hacia el empleado que realizó la asignación manual. Nulo si EsAutomatico=true.</summary>
        public Guid? AsignadoPorId { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Cliente al que pertenece esta segmentación RFM.</summary>
        public virtual Cliente? Cliente { get; set; }
    }
}
