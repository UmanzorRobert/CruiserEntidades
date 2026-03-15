using System;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Integraciones
{
    /// <summary>
    /// Webhook recibido de un sistema externo a través del endpoint de recepción de CruiserCat.
    /// Los webhooks son notificaciones HTTP enviadas por el sistema externo cuando ocurre un evento:
    /// confirmación de pago, firma completada, entrega de SMS, respuesta AEAT, etc.
    ///
    /// FLUJO DE PROCESAMIENTO:
    /// 1. El sistema externo envía un POST al endpoint /webhooks/{tipo} de CruiserCat.
    /// 2. El controlador WebhooksController valida la firma del webhook (HMAC-SHA256).
    /// 3. Si es válida, crea un registro WebhookRecibido con Estado=Pendiente.
    /// 4. El job de Hangfire procesa el webhook en segundo plano:
    ///    a. Determina el TipoEvento.
    ///    b. Actualiza la entidad de negocio correspondiente.
    ///    c. Actualiza Estado=Procesado o Estado=Error.
    ///
    /// IDEMPOTENCIA:
    /// IdExternoWebhook es el identificador del evento en el sistema externo.
    /// El controlador verifica que no exista ya un webhook con ese ID para evitar
    /// procesar el mismo evento dos veces (los sistemas externos pueden reintentar).
    ///
    /// FIRMA DE SEGURIDAD:
    /// FirmaWebhook almacena el header de firma recibido (Stripe-Signature, X-Hub-Signature).
    /// Se verifica contra el webhook secret almacenado en ConfiguracionIntegracion.Credenciales
    /// para garantizar que el webhook proviene del sistema externo legítimo.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.IdExternoWebhook).HasMaxLength(200);
    ///   builder.Property(x => x.Payload).HasColumnType("jsonb");
    ///   builder.HasIndex(x => new { x.ConfiguracionIntegracionId, x.Estado });
    ///   builder.HasIndex(x => x.IdExternoWebhook)
    ///          .HasFilter("\"IdExternoWebhook\" IS NOT NULL").IsUnique();
    ///   builder.HasIndex(x => x.FechaRecepcion);
    ///   builder.HasIndex(x => x.TipoEvento);
    ///   builder.HasIndex(x => x.Payload).HasMethod("gin");
    /// </remarks>
    public class WebhookRecibido
    {
        /// <summary>Identificador único del webhook recibido en CruiserCat.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia la configuración de integración que recibió el webhook.</summary>
        public Guid ConfiguracionIntegracionId { get; set; }

        /// <summary>Fecha y hora UTC en que CruiserCat recibió el webhook.</summary>
        public DateTime FechaRecepcion { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Identificador único del evento en el sistema externo.
        /// Ejemplo: Stripe "evt_1AbCdEfGhIjKlMnO", AEAT "SII-2026-00001".
        /// Usado para verificar idempotencia y evitar procesar el mismo evento dos veces.
        /// </summary>
        public string? IdExternoWebhook { get; set; }

        /// <summary>Tipo de evento del webhook recibido del sistema externo.</summary>
        public TipoWebhook TipoEvento { get; set; } = TipoWebhook.Desconocido;

        /// <summary>
        /// Nombre del evento tal como lo envía el sistema externo antes de mapear al enum.
        /// Ejemplo: "payment_intent.succeeded", "charge.refunded", "document.signed".
        /// </summary>
        public string? NombreEventoExterno { get; set; }

        /// <summary>
        /// Cuerpo completo del webhook recibido en formato JSONB.
        /// Permite procesar el webhook con todos los datos del sistema externo disponibles.
        /// </summary>
        public string? Payload { get; set; }

        /// <summary>
        /// Firma de seguridad del webhook recibida en los headers HTTP.
        /// Almacenada para auditoría. Verificada contra el webhook secret de la integración.
        /// </summary>
        public string? FirmaWebhook { get; set; }

        /// <summary>
        /// Indica si la firma de seguridad del webhook fue verificada correctamente.
        /// False indica un webhook posiblemente falsificado o enviado por un tercero.
        /// </summary>
        public bool FirmaVerificada { get; set; } = false;

        /// <summary>Estado de procesamiento del webhook en el flujo de Hangfire.</summary>
        public EstadoProcesadoWebhook Estado { get; set; } = EstadoProcesadoWebhook.Pendiente;

        /// <summary>Fecha y hora UTC en que el job de Hangfire procesó el webhook.</summary>
        public DateTime? FechaProcesamiento { get; set; }

        /// <summary>
        /// ID de la entidad de negocio actualizada al procesar el webhook.
        /// Ejemplo: ID del PagoFactura confirmado, ID de la FirmaElectronica completada.
        /// </summary>
        public Guid? EntidadActualizadaId { get; set; }

        /// <summary>Tipo de la entidad actualizada al procesar el webhook.</summary>
        public string? TipoEntidadActualizada { get; set; }

        /// <summary>
        /// Número de intentos de procesamiento del webhook.
        /// Se incrementa en cada reintento del job de Hangfire.
        /// Al superar MaxReintentos de la integración, Estado = Error definitivo.
        /// </summary>
        public int NumeroIntentos { get; set; } = 0;

        /// <summary>Mensaje de error del último intento de procesamiento fallido.</summary>
        public string? MensajeError { get; set; }

        /// <summary>Dirección IP del servidor externo que envió el webhook.</summary>
        public string? DireccionIPOrigen { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Configuración de integración asociada al webhook recibido.</summary>
        public virtual ConfiguracionIntegracion? ConfiguracionIntegracion { get; set; }
    }
}
