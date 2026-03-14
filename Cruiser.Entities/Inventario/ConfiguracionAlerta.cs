using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Inventario
{
    /// <summary>
    /// Configuración de umbrales y canales de alerta de stock para un producto en un almacén.
    /// Define cuándo y cómo se notifica al equipo responsable cuando el stock
    /// de un producto supera o cae por debajo de los umbrales configurados.
    ///
    /// El job de Hangfire StockAlertService verifica esta configuración cada 30 minutos
    /// y genera las Notificaciones y emails correspondientes cuando se superan los umbrales.
    ///
    /// Para evitar spam de alertas, se usa ClaveAgrupacion en Notificacion para
    /// no generar más de una alerta del mismo tipo por producto en un período configurable.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.UmbralStockMinimo).HasPrecision(18, 4);
    ///   builder.Property(x => x.UmbralStockMaximo).HasPrecision(18, 4);
    ///   builder.Property(x => x.EmailsDestinatarios).HasColumnType("jsonb");
    ///   builder.Property(x => x.UsuariosDestinatarios).HasColumnType("jsonb");
    ///   builder.HasIndex(x => new { x.ProductoId, x.AlmacenId }).IsUnique();
    ///   builder.HasIndex(x => x.EstaActiva);
    ///
    ///   Estructura JSON de EmailsDestinatarios:
    ///   ["almacen@empresa.com", "supervisor@empresa.com"]
    ///
    ///   Estructura JSON de UsuariosDestinatarios:
    ///   ["guid-usuario-1", "guid-usuario-2"]
    /// </remarks>
    public class ConfiguracionAlerta : EntidadBase
    {
        /// <summary>FK hacia el producto al que aplica esta configuración de alerta.</summary>
        public Guid ProductoId { get; set; }

        /// <summary>
        /// FK hacia el almacén específico para el que aplica esta alerta.
        /// Nulo si la alerta aplica a todos los almacenes del producto de forma consolidada.
        /// </summary>
        public Guid? AlmacenId { get; set; }

        /// <summary>
        /// Umbral de stock mínimo. Cuando el stock disponible cae por debajo de este valor,
        /// se genera la alerta de stock bajo. Sobreescribe el valor de Producto.StockMinimo.
        /// </summary>
        public decimal UmbralStockMinimo { get; set; }

        /// <summary>
        /// Umbral de stock máximo. Cuando el stock supera este valor tras una recepción,
        /// se genera una alerta de exceso de stock para evitar sobrecostos de almacenamiento.
        /// </summary>
        public decimal? UmbralStockMaximo { get; set; }

        /// <summary>
        /// Días de anticipación para alertas de caducidad de lotes.
        /// Se genera alerta cuando FechaCaducidad - Hoy &lt;= DiasAlertaCaducidad.
        /// </summary>
        public int DiasAlertaCaducidad { get; set; } = 30;

        // ── Canales de notificación ──────────────────────────────────────────

        /// <summary>Activa las notificaciones de alerta de stock en el panel del sistema (SignalR).</summary>
        public bool AlertarPorSistema { get; set; } = true;

        /// <summary>Activa el envío de alertas de stock por correo electrónico (MailKit).</summary>
        public bool AlertarPorEmail { get; set; } = false;

        /// <summary>Activa el envío de alertas de stock por notificación push (PWA).</summary>
        public bool AlertarPorPush { get; set; } = false;

        /// <summary>
        /// Lista JSON de emails adicionales que deben recibir las alertas.
        /// Puede incluir emails externos al sistema (proveedor, director, etc.).
        /// </summary>
        public string? EmailsDestinatarios { get; set; }

        /// <summary>
        /// Lista JSON de identificadores GUID de usuarios internos que deben recibir las alertas.
        /// Complementa los roles destinatarios definidos en ConfiguracionNotificacion.
        /// </summary>
        public string? UsuariosDestinatarios { get; set; }

        /// <summary>Indica si esta configuración de alerta está activa.</summary>
        public bool EstaActiva { get; set; } = true;

        /// <summary>Fecha y hora UTC de la última vez que se generó una alerta para esta configuración.</summary>
        public DateTime? FechaUltimaAlerta { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Producto al que aplica esta configuración de alerta de stock.</summary>
        public virtual Catalogo.Producto? Producto { get; set; }

        /// <summary>Almacén al que aplica esta configuración. Nulo = todos los almacenes.</summary>
        public virtual Almacen? Almacen { get; set; }
    }
}
