using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Cola de procesamiento asíncrono para tareas que no pueden ejecutarse en tiempo real.
    /// Complementa Hangfire para tareas de negocio complejas que requieren estado persistente
    /// y seguimiento detallado más allá del dashboard estándar de Hangfire.
    ///
    /// Casos de uso: generación de reportes masivos, exportaciones GDPR,
    /// sincronización de datos offline, envíos masivos de campañas.
    ///
    /// El Payload JSONB almacena todos los parámetros necesarios para ejecutar la tarea
    /// de forma idempotente ante reintentos.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.TipoTarea).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.Payload).HasColumnType("jsonb");
    ///   builder.HasIndex(x => new { x.Estado, x.PrioridadCola });
    ///   builder.HasIndex(x => x.FechaCreacion);
    ///   builder.HasIndex(x => x.Payload).HasMethod("gin");
    /// </remarks>
    public class ColaProcesamiento : EntidadBase
    {
        /// <summary>
        /// Tipo de tarea a ejecutar. Identifica qué servicio procesa este elemento.
        /// Ejemplos: "GENERACION_REPORTE", "EXPORTACION_GDPR", "CAMPAÑA_EMAIL",
        /// "SYNC_OFFLINE", "CALCULO_KPI", "REMESA_SEPA".
        /// </summary>
        public string TipoTarea { get; set; } = string.Empty;

        /// <summary>
        /// Parámetros de la tarea en formato JSON.
        /// Almacenado en JSONB con índice GIN para consultas sobre el contenido.
        /// Debe contener toda la información necesaria para ejecutar la tarea de forma idempotente.
        /// Ejemplo: { "reporteId": "uuid", "formato": "PDF", "filtros": {...} }
        /// </summary>
        public string? Payload { get; set; }

        /// <summary>Estado actual de procesamiento de esta tarea en la cola.</summary>
        public EstadoColaProcesamiento Estado { get; set; } = EstadoColaProcesamiento.Pendiente;

        /// <summary>Número de intentos de procesamiento realizados.</summary>
        public int Intentos { get; set; } = 0;

        /// <summary>
        /// Número máximo de intentos antes de marcar la tarea como Fallida definitivamente.
        /// Por defecto 3 intentos.
        /// </summary>
        public int MaxIntentos { get; set; } = 3;

        /// <summary>
        /// Fecha y hora UTC en que la tarea fue recogida por el procesador y empezó a ejecutarse.
        /// Nulo si aún está pendiente.
        /// </summary>
        public DateTime? FechaProcesamiento { get; set; }

        /// <summary>
        /// Fecha y hora UTC en que la tarea fue completada o fallida definitivamente.
        /// </summary>
        public DateTime? FechaFinalizacion { get; set; }

        /// <summary>
        /// Descripción del último error producido al intentar procesar la tarea.
        /// Incluye el mensaje de excepción y el stack trace para diagnóstico.
        /// </summary>
        public string? UltimoError { get; set; }

        /// <summary>
        /// Prioridad de procesamiento en la cola.
        /// Las tareas de mayor prioridad se procesan antes independientemente del orden de creación.
        /// </summary>
        public PrioridadCola PrioridadCola { get; set; } = PrioridadCola.Normal;

        /// <summary>
        /// Identificador del usuario que originó la tarea.
        /// Nulo para tareas generadas automáticamente por Hangfire jobs.
        /// </summary>
        public Guid? SolicitadoPorId { get; set; }

        /// <summary>
        /// Identificador del job de Hangfire asociado a esta tarea.
        /// Permite correlacionar el registro con el panel de Hangfire.
        /// </summary>
        public string? HangfireJobId { get; set; }

        /// <summary>
        /// Progreso de la tarea en porcentaje (0-100).
        /// Actualizado por el procesador para mostrar barra de progreso en la UI.
        /// </summary>
        public int ProgresoPocentaje { get; set; } = 0;

        /// <summary>
        /// Resultado de la tarea completada en formato JSON.
        /// Ejemplo: { "archivoGenerado": "/reportes/reporte_2026.pdf", "registrosProcesados": 1250 }
        /// </summary>
        public string? ResultadoJSON { get; set; }
    }
}
