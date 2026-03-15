using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.RRHH
{
    /// <summary>
    /// Operación individual realizada en modo offline que forma parte de un lote
    /// de sincronización SincronizacionOffline.
    ///
    /// Cada operación contiene el tipo de acción (fichaje, checklist, foto, firma...),
    /// los datos en formato JSONB y el resultado del intento de sincronización.
    ///
    /// GESTIÓN DE CONFLICTOS:
    /// Un conflicto ocurre cuando el mismo registro fue modificado en el servidor
    /// mientras el empleado estaba offline. La política por defecto es "servidor gana",
    /// pero se registra el conflicto para que el supervisor pueda revisarlo.
    ///
    /// PAYLOAD JSON (ejemplo para TipoOperacion = Fichaje):
    /// {
    ///   "empleadoId": "...",
    ///   "clienteId": "...",
    ///   "tipoFichaje": "Entrada",
    ///   "latitud": 40.416775,
    ///   "longitud": -3.703790,
    ///   "fechaHoraLocal": "2026-03-13T08:32:15",
    ///   "precisionMetros": 12
    /// }
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Payload).HasColumnType("jsonb");
    ///   builder.Property(x => x.ResultadoServidor).HasColumnType("jsonb");
    ///   builder.HasIndex(x => new { x.SincronizacionOfflineId, x.Orden });
    ///   builder.HasIndex(x => x.TipoOperacion);
    ///   builder.HasIndex(x => x.EsConflicto).HasFilter("\"EsConflicto\" = true");
    ///   builder.HasIndex(x => x.Payload).HasMethod("gin");
    /// </remarks>
    public class ColaSincronizacionOffline : EntidadBase
    {
        /// <summary>FK hacia el lote de sincronización al que pertenece esta operación.</summary>
        public Guid SincronizacionOfflineId { get; set; }

        /// <summary>Tipo de operación offline realizada por el empleado.</summary>
        public TipoOperacionOffline TipoOperacion { get; set; }

        /// <summary>
        /// Orden de ejecución de la operación dentro del lote.
        /// Las operaciones deben procesarse en orden para mantener la coherencia.
        /// Ejemplo: primero el fichaje de entrada, luego los ítems del checklist.
        /// </summary>
        public int Orden { get; set; }

        /// <summary>
        /// Datos de la operación offline en formato JSONB.
        /// Contiene todos los campos necesarios para reproducir la operación en el servidor.
        /// </summary>
        public string? Payload { get; set; }

        /// <summary>
        /// Fecha y hora LOCAL del dispositivo en que se realizó la operación.
        /// Puede diferir de la hora UTC del servidor si el dispositivo tenía un reloj desajustado.
        /// </summary>
        public DateTime FechaHoraLocal { get; set; }

        // ── Resultado de la sincronización ───────────────────────────────────

        /// <summary>Indica si la operación fue procesada exitosamente por el servidor.</summary>
        public bool EsProcesada { get; set; } = false;

        /// <summary>Fecha y hora UTC en que el servidor procesó esta operación.</summary>
        public DateTime? FechaProcesamiento { get; set; }

        /// <summary>
        /// Indica si esta operación tiene un conflicto de datos con el estado del servidor.
        /// True = el dato fue modificado en el servidor mientras el empleado estaba offline.
        /// </summary>
        public bool EsConflicto { get; set; } = false;

        /// <summary>Descripción del conflicto detectado para facilitar la resolución manual.</summary>
        public string? DescripcionConflicto { get; set; }

        /// <summary>
        /// Respuesta del servidor tras procesar la operación en formato JSONB.
        /// Contiene el resultado, el ID del registro creado o el detalle del error.
        /// </summary>
        public string? ResultadoServidor { get; set; }

        /// <summary>Mensaje de error del servidor si la operación falló.</summary>
        public string? MensajeError { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Lote de sincronización al que pertenece esta operación.</summary>
        public virtual SincronizacionOffline? SincronizacionOffline { get; set; }
    }
}