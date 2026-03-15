using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Analitica
{
    /// <summary>
    /// Instancia de ejecución de un ReporteGuardado programado por Hangfire.
    /// Registra cada ejecución automática o manual de un reporte, su resultado,
    /// la duración, el archivo generado y los posibles errores.
    ///
    /// Permite al administrador revisar el historial de ejecuciones de cada reporte
    /// desde el panel de Reportes Guardados, diagnosticar fallos y monitorear
    /// el rendimiento de la generación de reportes a lo largo del tiempo.
    ///
    /// El campo HangfireJobId vincula la ejecución con el job de Hangfire correspondiente
    /// para poder relanzarlo desde el dashboard /hangfire si es necesario.
    ///
    /// SEPARACIÓN DE RESPONSABILIDADES:
    /// - ReporteGuardado: DEFINICIÓN del reporte (qué, cómo, cuándo).
    /// - ReporteProgramado: EJECUCIÓN del reporte (registro de cada vez que se ejecuta).
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.HangfireJobId).HasMaxLength(100);
    ///   builder.Property(x => x.RutaArchivoGenerado).HasMaxLength(1000);
    ///   builder.HasIndex(x => new { x.ReporteGuardadoId, x.FechaEjecucion });
    ///   builder.HasIndex(x => x.Estado);
    ///   builder.HasIndex(x => x.FechaEjecucion);
    ///   builder.HasIndex(x => x.HangfireJobId).HasFilter("\"HangfireJobId\" IS NOT NULL");
    /// </remarks>
    public class ReporteProgramado : EntidadBase
    {
        /// <summary>FK hacia el reporte guardado que originó esta ejecución.</summary>
        public Guid ReporteGuardadoId { get; set; }

        /// <summary>FK hacia el usuario que ejecutó el reporte (nulo si fue ejecución automática de Hangfire).</summary>
        public Guid? EjecutadoPorId { get; set; }

        /// <summary>Fecha y hora UTC de inicio de la ejecución del reporte.</summary>
        public DateTime FechaEjecucion { get; set; } = DateTime.UtcNow;

        /// <summary>Fecha y hora UTC en que finalizó la ejecución (exitosa o con error).</summary>
        public DateTime? FechaFinEjecucion { get; set; }

        /// <summary>
        /// Estado de la ejecución del reporte.
        /// Valores: "EnProceso", "Completado", "Error", "Cancelado".
        /// </summary>
        public string Estado { get; set; } = "EnProceso";

        /// <summary>Duración total de la ejecución en segundos.</summary>
        public int? DuracionSegundos { get; set; }

        /// <summary>
        /// Ruta relativa del archivo generado (PDF o Excel) almacenado en el servidor.
        /// Nulo si la ejecución falló o el formato es Pantalla.
        /// </summary>
        public string? RutaArchivoGenerado { get; set; }

        /// <summary>Tamaño del archivo generado en bytes. Estadístico.</summary>
        public long? TamanoArchivoBytes { get; set; }

        /// <summary>
        /// Indica si el reporte fue ejecutado automáticamente por Hangfire
        /// o manualmente por el usuario desde la interfaz.
        /// </summary>
        public bool EsEjecucionAutomatica { get; set; } = false;

        /// <summary>
        /// Identificador del job de Hangfire que ejecutó el reporte.
        /// Permite relanzar el job desde el dashboard /hangfire en caso de fallo.
        /// </summary>
        public string? HangfireJobId { get; set; }

        /// <summary>Mensaje de error detallado si la ejecución falló.</summary>
        public string? MensajeError { get; set; }

        /// <summary>
        /// Lista de emails a los que se envió el reporte generado separados por coma.
        /// Copiado de ReporteGuardado.EmailsDestinatarios en el momento de la ejecución.
        /// </summary>
        public string? EmailsEnviados { get; set; }

        /// <summary>Número de filas de datos incluidas en el reporte generado. Estadístico.</summary>
        public int? NumeroFilasDatos { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Reporte guardado que define la configuración de esta ejecución.</summary>
        public virtual ReporteGuardado? ReporteGuardado { get; set; }
    }
}
