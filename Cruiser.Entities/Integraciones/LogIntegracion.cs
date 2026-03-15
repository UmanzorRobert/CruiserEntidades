using System;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Integraciones
{
    /// <summary>
    /// Registro de log de cada llamada realizada a un sistema de integración externo.
    /// Append-only: se crea un registro por cada llamada HTTP a la API externa,
    /// tanto exitosas como fallidas, para auditoría completa y diagnóstico de problemas.
    ///
    /// El Payload almacena el cuerpo de la solicitud enviada al sistema externo.
    /// La Respuesta almacena el cuerpo de la respuesta recibida.
    /// Ambos campos son JSONB con índice GIN para búsquedas en los logs de integración.
    ///
    /// DuracionMs permite identificar llamadas lentas que impactan en el rendimiento.
    /// El dashboard de integraciones muestra el p95 de duración por tipo de operación.
    ///
    /// EntidadRelacionadaId permite vincular el log con la entidad de negocio
    /// que originó la llamada: la Factura que se envió a AEAT, el PagoFactura
    /// que se procesó en Stripe, etc.
    ///
    /// RETENCIÓN: los logs de integración se limpian automáticamente por el job de Hangfire
    /// LimpiarLogIntegracion mensualmente, conservando solo los últimos 90 días.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   NO hereda EntidadBase — log append-only con volumen alto.
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.TipoOperacion).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.Payload).HasColumnType("jsonb");
    ///   builder.Property(x => x.Respuesta).HasColumnType("jsonb");
    ///   builder.HasIndex(x => new { x.ConfiguracionIntegracionId, x.FechaOperacion });
    ///   builder.HasIndex(x => new { x.Estado, x.FechaOperacion });
    ///   builder.HasIndex(x => x.EntidadRelacionadaId).HasFilter("\"EntidadRelacionadaId\" IS NOT NULL");
    ///   builder.HasIndex(x => x.Payload).HasMethod("gin");
    ///   builder.HasIndex(x => x.Respuesta).HasMethod("gin");
    /// </remarks>
    public class LogIntegracion
    {
        /// <summary>Identificador único del registro de log.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia la configuración de integración que realizó la llamada.</summary>
        public Guid ConfiguracionIntegracionId { get; set; }

        /// <summary>Fecha y hora UTC de la llamada al sistema externo.</summary>
        public DateTime FechaOperacion { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Tipo de operación realizada en el sistema externo.
        /// Ejemplo: "EnviarFacturaSII", "ProcesarPago", "VerificarEstadoPago",
        ///          "GeocodeAddress", "EnviarWhatsApp", "ConsultarNIF".
        /// </summary>
        public string TipoOperacion { get; set; } = string.Empty;

        /// <summary>
        /// Método HTTP utilizado en la llamada: GET, POST, PUT, PATCH, DELETE.
        /// </summary>
        public string? MetodoHttp { get; set; }

        /// <summary>URL exacta llamada (sin credenciales). Para diagnóstico.</summary>
        public string? UrlLlamada { get; set; }

        /// <summary>Código HTTP de respuesta recibido: 200, 201, 400, 401, 429, 500, etc.</summary>
        public int? CodigoHttpRespuesta { get; set; }

        /// <summary>
        /// Cuerpo de la solicitud enviada al sistema externo en formato JSONB.
        /// Datos sensibles (números de tarjeta, etc.) deben estar omitidos o enmascarados.
        /// </summary>
        public string? Payload { get; set; }

        /// <summary>
        /// Cuerpo de la respuesta recibida del sistema externo en formato JSONB.
        /// Truncado si la respuesta supera los 100 KB para evitar datos excesivos en BD.
        /// </summary>
        public string? Respuesta { get; set; }

        /// <summary>Estado del resultado de la llamada.</summary>
        public EstadoLogIntegracion Estado { get; set; } = EstadoLogIntegracion.Exitoso;

        /// <summary>Duración de la llamada HTTP en milisegundos.</summary>
        public int DuracionMs { get; set; } = 0;

        /// <summary>
        /// ID de la entidad de negocio que originó la llamada.
        /// Polimórfico: puede ser una Factura, un PagoFactura, una OrdenServicio, etc.
        /// Permite filtrar todos los logs relacionados con una entidad específica.
        /// </summary>
        public Guid? EntidadRelacionadaId { get; set; }

        /// <summary>
        /// Nombre del tipo de entidad relacionada para la consulta polimórfica.
        /// Ejemplo: "Factura", "PagoFactura", "OrdenServicio".
        /// </summary>
        public string? TipoEntidadRelacionada { get; set; }

        /// <summary>
        /// Número del intento de reintento (1 = primera llamada, 2 = primer reintento, etc.).
        /// Permite identificar si la llamada exitosa requirió reintentos previos.
        /// </summary>
        public int NumeroIntento { get; set; } = 1;

        /// <summary>Descripción del error si la llamada falló.</summary>
        public string? MensajeError { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Configuración de integración que realizó la llamada.</summary>
        public virtual ConfiguracionIntegracion? ConfiguracionIntegracion { get; set; }
    }
}
