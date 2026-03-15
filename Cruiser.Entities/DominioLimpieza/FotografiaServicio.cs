using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Fotografía del servicio capturada por el empleado desde la cámara de la PWA.
    /// Se toman fotografías en tres momentos del servicio: Antes (estado inicial
    /// de las instalaciones), Durante (proceso de limpieza) y Después (resultado final).
    ///
    /// Las fotografías Antes/Después son especialmente importantes para demostrar
    /// al cliente la calidad del trabajo realizado y como evidencia ante reclamaciones.
    ///
    /// MODO OFFLINE: si el empleado no tiene conexión, la fotografía se almacena
    /// temporalmente en IndexedDB del dispositivo PWA y se sube al servidor
    /// al recuperar la conexión mediante ColaSincronizacionOffline.
    ///
    /// COORDENADAS GPS: se registran automáticamente al tomar la foto para verificar
    /// que el empleado estaba físicamente en las instalaciones del cliente.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Momento).HasMaxLength(20);
    ///   builder.Property(x => x.RutaArchivo).IsRequired().HasMaxLength(1000);
    ///   builder.HasIndex(x => new { x.OrdenServicioId, x.Momento });
    ///   builder.HasIndex(x => x.EsValidadaPorSupervisor);
    /// </remarks>
    public class FotografiaServicio : EntidadBase
    {
        /// <summary>FK hacia la orden de servicio a la que pertenece la fotografía.</summary>
        public Guid OrdenServicioId { get; set; }

        /// <summary>FK hacia el empleado que tomó la fotografía.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>
        /// Momento del servicio en que se tomó la fotografía.
        /// Valores: "Antes", "Durante", "Despues".
        /// </summary>
        public string Momento { get; set; } = "Antes";

        /// <summary>Ruta relativa del archivo de la fotografía en el servidor.</summary>
        public string RutaArchivo { get; set; } = string.Empty;

        /// <summary>Ruta relativa de la miniatura generada automáticamente de la fotografía.</summary>
        public string? RutaMiniatura { get; set; }

        /// <summary>Latitud GPS en que se tomó la fotografía.</summary>
        public decimal? Latitud { get; set; }

        /// <summary>Longitud GPS en que se tomó la fotografía.</summary>
        public decimal? Longitud { get; set; }

        /// <summary>Fecha y hora UTC en que se tomó la fotografía.</summary>
        public DateTime FechaCaptura { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indica si la fotografía ha sido validada por el supervisor de calidad.
        /// Las fotos no validadas aparecen pendientes en el panel de calidad.
        /// </summary>
        public bool EsValidadaPorSupervisor { get; set; } = false;

        /// <summary>FK hacia el supervisor que validó la fotografía.</summary>
        public Guid? ValidadaPorId { get; set; }

        /// <summary>Descripción o nota del empleado sobre el contenido de la fotografía.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Indica si la fotografía fue tomada en modo offline y sincronizada posteriormente.
        /// </summary>
        public bool FueTomadaOffline { get; set; } = false;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Orden de servicio a la que pertenece la fotografía.</summary>
        public virtual OrdenServicio? OrdenServicio { get; set; }

        /// <summary>Empleado que tomó la fotografía.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }
    }
}
