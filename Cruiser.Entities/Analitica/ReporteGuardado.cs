using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Analitica
{
    /// <summary>
    /// Definición guardada de un reporte personalizado que puede ser ejecutado
    /// bajo demanda o de forma automática según una periodicidad configurada.
    ///
    /// FiltrosJSON almacena los parámetros del reporte (fechas, filtros, agrupaciones)
    /// en formato JSONB para poder reproducir exactamente la misma consulta en
    /// futuras ejecuciones sin que el usuario tenga que reconfigurar los filtros.
    ///
    /// EJEMPLO DE FILTROSJSON para un reporte de facturación mensual:
    /// {
    ///   "periodo": "mensual",
    ///   "clienteIds": ["...", "..."],
    ///   "tipoServicioIds": null,
    ///   "incluirIVA": false,
    ///   "agruparPor": "cliente",
    ///   "ordenarPor": "importe_desc"
    /// }
    ///
    /// EsCompartido=true permite que los usuarios con el mismo rol puedan
    /// ejecutar el reporte sin necesidad de que cada uno lo configure individualmente.
    ///
    /// RutaArchivoGenerado almacena la ruta del último archivo generado para
    /// permitir su descarga sin tener que regenerar el reporte.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.TipoReporte).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.FiltrosJSON).HasColumnType("jsonb");
    ///   builder.Property(x => x.RutaArchivoGenerado).HasMaxLength(1000);
    ///   builder.HasIndex(x => new { x.UsuarioId, x.EsCompartido });
    ///   builder.HasIndex(x => x.TipoReporte);
    ///   builder.HasIndex(x => x.UltimaEjecucion).HasFilter("\"UltimaEjecucion\" IS NOT NULL");
    ///   builder.HasIndex(x => x.FiltrosJSON).HasMethod("gin");
    /// </remarks>
    public class ReporteGuardado : EntidadBase
    {
        /// <summary>FK hacia el usuario que creó y es propietario del reporte guardado.</summary>
        public Guid UsuarioId { get; set; }

        /// <summary>Nombre descriptivo del reporte para identificarlo en la lista de reportes.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del contenido del reporte y para qué sirve.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Clave del tipo de reporte que determina qué método de IReportService se invoca.
        /// Ejemplo: "FacturacionMensual", "StockAlmacen", "ProductividadEmpleado", "NPSPERSONA".
        /// </summary>
        public string TipoReporte { get; set; } = string.Empty;

        /// <summary>
        /// Filtros y parámetros del reporte en formato JSONB.
        /// Permiten reproducir exactamente la misma consulta en futuras ejecuciones.
        /// </summary>
        public string? FiltrosJSON { get; set; }

        /// <summary>Formato de salida del reporte: PDF, Excel o visualización en pantalla.</summary>
        public FormatoSalidaReporte FormatoSalida { get; set; } = FormatoSalidaReporte.PDF;

        /// <summary>
        /// Indica si el reporte es compartido con todos los usuarios del mismo rol.
        /// True permite que cualquier usuario con el rol adecuado pueda ejecutarlo.
        /// </summary>
        public bool EsCompartido { get; set; } = false;

        /// <summary>
        /// Frecuencia de ejecución automática del reporte por el job de Hangfire.
        /// Puntual = solo bajo demanda manual.
        /// </summary>
        public PeriodicidadReporte Periodicidad { get; set; } = PeriodicidadReporte.Puntual;

        /// <summary>
        /// Hora del día en que se ejecuta automáticamente el reporte (formato HH:mm).
        /// Solo aplica cuando Periodicidad ≠ Puntual.
        /// </summary>
        public string? HoraEjecucionAutomatica { get; set; }

        /// <summary>
        /// Lista de emails separados por coma a los que se envía el reporte generado.
        /// Solo aplica para ejecuciones automáticas con Periodicidad ≠ Puntual.
        /// </summary>
        public string? EmailsDestinatarios { get; set; }

        /// <summary>Fecha y hora UTC de la última ejecución del reporte (manual o automática).</summary>
        public DateTime? UltimaEjecucion { get; set; }

        /// <summary>
        /// Ruta relativa del último archivo generado (PDF o Excel) almacenado en el servidor.
        /// Permite descargarlo sin regenerar el reporte hasta que expire.
        /// </summary>
        public string? RutaArchivoGenerado { get; set; }

        /// <summary>
        /// Fecha y hora UTC en que el archivo generado expira y debe regenerarse en la próxima descarga.
        /// </summary>
        public DateTime? FechaExpiracionArchivo { get; set; }

        /// <summary>Número total de ejecuciones del reporte desde su creación.</summary>
        public int NumeroEjecuciones { get; set; } = 0;

        /// <summary>Duración en segundos de la última ejecución del reporte. Estadístico.</summary>
        public int? DuracionUltimaEjecucionSegundos { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────
        // Sin navegación directa al Usuario para evitar dependencia circular con Seguridad.
    }
}
