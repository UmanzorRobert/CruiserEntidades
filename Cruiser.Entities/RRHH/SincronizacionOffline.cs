using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.RRHH
{
    /// <summary>
    /// Registro de un lote de sincronización de operaciones offline realizadas
    /// por un empleado desde la PWA en campo sin conexión a internet.
    ///
    /// Cuando el dispositivo PWA recupera la conexión, envía todas las operaciones
    /// realizadas offline como un lote a ISincronizacionOfflineService.ProcesarLoteSincronizacion.
    ///
    /// Cada lote tiene un identificador único generado en el dispositivo (IdDispositivo)
    /// y contiene múltiples ColaSincronizacionOffline (operaciones individuales).
    ///
    /// La resolución de conflictos sigue la política "servidor gana" por defecto:
    /// si un dato fue modificado en el servidor mientras el empleado estaba offline,
    /// el servidor mantiene su versión y se notifica al empleado del conflicto.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.IdDispositivoOrigen).HasMaxLength(100);
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.Estado });
    ///   builder.HasIndex(x => x.FechaInicioPeriodoOffline);
    ///   builder.HasIndex(x => x.DispositivoPWAId).HasFilter("\"DispositivoPWAId\" IS NOT NULL");
    /// </remarks>
    public class SincronizacionOffline : EntidadBase
    {
        /// <summary>FK hacia el empleado que realizó las operaciones offline.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>
        /// FK hacia el dispositivo PWA desde el que se enviaron las operaciones offline.
        /// Nulo si el dispositivo no está registrado en DispositivoPWA.
        /// </summary>
        public Guid? DispositivoPWAId { get; set; }

        /// <summary>
        /// Identificador único del dispositivo generado en el cliente PWA.
        /// Permite rastrear el lote al dispositivo concreto aunque no esté registrado.
        /// </summary>
        public string? IdDispositivoOrigen { get; set; }

        /// <summary>Estado actual del proceso de sincronización de este lote.</summary>
        public EstadoSincronizacion Estado { get; set; } = EstadoSincronizacion.Pendiente;

        /// <summary>Fecha y hora UTC en que el empleado entró en modo offline.</summary>
        public DateTime FechaInicioPeriodoOffline { get; set; }

        /// <summary>Fecha y hora UTC en que el dispositivo recuperó la conexión.</summary>
        public DateTime? FechaRecuperacionConexion { get; set; }

        /// <summary>Fecha y hora UTC en que el servidor procesó el lote completo.</summary>
        public DateTime? FechaProcesamiento { get; set; }

        /// <summary>Número total de operaciones offline en el lote.</summary>
        public int TotalOperaciones { get; set; } = 0;

        /// <summary>Número de operaciones procesadas exitosamente.</summary>
        public int OperacionesExitosas { get; set; } = 0;

        /// <summary>Número de operaciones con conflicto que requieren resolución manual.</summary>
        public int OperacionesConConflicto { get; set; } = 0;

        /// <summary>Número de operaciones fallidas por error del servidor.</summary>
        public int OperacionesFallidas { get; set; } = 0;

        /// <summary>Descripción del error si el lote no pudo procesarse completamente.</summary>
        public string? DescripcionError { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Empleado que realizó las operaciones offline.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }

        /// <summary>Operaciones individuales incluidas en este lote de sincronización.</summary>
        public virtual ICollection<ColaSincronizacionOffline> Operaciones { get; set; }
            = new List<ColaSincronizacionOffline>();
    }
}
