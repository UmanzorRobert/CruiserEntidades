using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Notificaciones
{
    /// <summary>
    /// Notificación generada por el sistema para ser distribuida a uno o varios usuarios.
    /// Es la entidad central del módulo de notificaciones: contiene el contenido
    /// del mensaje y los metadatos de agrupación, sin la información de entrega
    /// por usuario (que reside en NotificacionUsuario).
    ///
    /// La ClaveAgrupacion evita duplicados: si ya existe una Notificacion no leída
    /// con la misma ClaveAgrupacion, IStockAlertService no crea una nueva.
    /// Ejemplo de clave: "stock-bajo-{ProductoId}" o "itv-vencida-{VehiculoId}".
    ///
    /// NotificacionPadreId permite agrupar notificaciones relacionadas
    /// en un hilo (thread) visible en el panel de notificaciones.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Titulo).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.Mensaje).IsRequired().HasMaxLength(1000);
    ///   builder.Property(x => x.Enlace).HasMaxLength(500);
    ///   builder.Property(x => x.ClaveAgrupacion).HasMaxLength(200);
    ///   builder.Property(x => x.DatosAdicionales).HasColumnType("jsonb");
    ///   builder.HasIndex(x => x.ClaveAgrupacion).HasFilter("\"ClaveAgrupacion\" IS NOT NULL");
    ///   builder.HasIndex(x => new { x.TipoNotificacionId, x.FechaCreacion });
    ///   builder.HasIndex(x => x.FechaExpiracion).HasFilter("\"FechaExpiracion\" IS NOT NULL");
    ///   builder.HasIndex(x => x.DatosAdicionales).HasMethod("gin");
    /// </remarks>
    public class Notificacion : EntidadBase
    {
        /// <summary>FK hacia el tipo de notificación que define su comportamiento y apariencia.</summary>
        public Guid TipoNotificacionId { get; set; }

        /// <summary>Título breve de la notificación (máximo 200 caracteres). Visible en el panel.</summary>
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// Cuerpo del mensaje de la notificación (máximo 1000 caracteres).
        /// Puede incluir valores dinámicos del evento que la originó.
        /// </summary>
        public string Mensaje { get; set; } = string.Empty;

        /// <summary>
        /// URL relativa o absoluta a la que se navega al pulsar la notificación.
        /// Ejemplo: "/inventario/productos/edit/{id}", "/facturas/detail/{id}".
        /// </summary>
        public string? Enlace { get; set; }

        /// <summary>
        /// Datos adicionales estructurados en formato JSONB relacionados con la notificación.
        /// Permite almacenar contexto específico del evento: IDs relacionados, valores, etc.
        /// Ejemplo: {"productoId": "...", "stockActual": 3, "stockMinimo": 10}.
        /// </summary>
        public string? DatosAdicionales { get; set; }

        /// <summary>
        /// Fecha y hora UTC en que la notificación deja de ser relevante.
        /// Las notificaciones expiradas son limpiadas automáticamente por Hangfire.
        /// Nulo = sin expiración.
        /// </summary>
        public DateTime? FechaExpiracion { get; set; }

        /// <summary>
        /// Clave única de agrupación para evitar notificaciones duplicadas.
        /// Si ya existe una Notificacion no leída con esta clave, no se crea una nueva.
        /// Se invalida automáticamente cuando todas las NotificacionUsuario están leídas.
        /// </summary>
        public string? ClaveAgrupacion { get; set; }

        /// <summary>
        /// FK hacia la notificación padre de la que esta es un hilo o seguimiento.
        /// Permite construir conversaciones de alertas en el panel de notificaciones.
        /// </summary>
        public Guid? NotificacionPadreId { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Tipo de notificación que define el comportamiento de esta notificación.</summary>
        public virtual TipoNotificacion? TipoNotificacion { get; set; }

        /// <summary>Notificación padre en el hilo de notificaciones relacionadas.</summary>
        public virtual Notificacion? NotificacionPadre { get; set; }

        /// <summary>Notificaciones hijo que forman parte de este hilo.</summary>
        public virtual ICollection<Notificacion> NotificacionesHijo { get; set; }
            = new List<Notificacion>();

        /// <summary>Registros de entrega de esta notificación a cada usuario destinatario.</summary>
        public virtual ICollection<NotificacionUsuario> NotificacionesUsuario { get; set; }
            = new List<NotificacionUsuario>();
    }
}
