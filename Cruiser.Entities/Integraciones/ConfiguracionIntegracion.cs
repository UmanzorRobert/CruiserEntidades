using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Integraciones
{
    /// <summary>
    /// Configuración de una integración con un sistema externo.
    /// Almacena los datos de conexión (URL, credenciales, timeouts) de forma
    /// centralizada para que IIntegracionService pueda invocar APIs externas.
    ///
    /// SEGURIDAD DE CREDENCIALES:
    /// ApiKey y Credenciales se almacenan cifrados con AES-256 usando la clave
    /// maestra del sistema (variable de entorno). Se descifran en memoria
    /// únicamente durante la llamada HTTP y no se exponen en logs ni en la interfaz.
    ///
    /// La entidad permite gestionar múltiples configuraciones del mismo TipoIntegracion:
    /// por ejemplo, dos pasarelas de pago (Stripe para online + Redsys para TPV físico)
    /// o dos configuraciones AEAT (producción y sandbox/pruebas).
    ///
    /// PRUEBA DE CONEXIÓN:
    /// El endpoint /admin/integraciones/test/{id} invoca IIntegracionService.TestConexion
    /// que registra el resultado en LogIntegracion y devuelve el estado al usuario.
    ///
    /// VARIABLES DE ENTORNO (Railway):
    /// Las credenciales de producción se configuran como variables de entorno
    /// en el Dashboard de Railway y se referencian desde la UI de administración.
    /// La BD almacena solo la referencia al nombre de la variable, no el valor.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150);
    ///   builder.Property(x => x.UrlEndpoint).HasMaxLength(500);
    ///   builder.Property(x => x.ApiKey).HasMaxLength(1000);
    ///   builder.Property(x => x.Credenciales).HasColumnType("jsonb");
    ///   builder.HasIndex(x => new { x.TipoIntegracion, x.EstaActiva });
    ///   builder.HasIndex(x => x.Credenciales).HasMethod("gin");
    /// </remarks>
    public class ConfiguracionIntegracion : EntidadBase
    {
        /// <summary>Nombre descriptivo de la integración para identificarla en el panel de administración.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Tipo de sistema externo con el que se integra.</summary>
        public TipoIntegracion TipoIntegracion { get; set; }

        /// <summary>
        /// URL base del endpoint del sistema externo.
        /// Ejemplo: "https://api.stripe.com/v1", "https://www7.aeat.es/wlpl/SSII-FACT/ws".
        /// </summary>
        public string? UrlEndpoint { get; set; }

        /// <summary>
        /// API Key o token de autenticación del sistema externo, cifrado con AES-256.
        /// Se descifra en memoria durante la llamada HTTP. Nunca se expone en logs.
        /// </summary>
        public string? ApiKey { get; set; }

        /// <summary>
        /// Credenciales adicionales en formato JSONB cifrado.
        /// Permite almacenar múltiples credenciales de autenticación:
        /// {
        ///   "clientId": "...", "clientSecret": "...", "webhookSecret": "...",
        ///   "certificadoPath": "...", "certificadoPassword": "..."
        /// }
        /// </summary>
        public string? Credenciales { get; set; }

        /// <summary>
        /// Ambiente de la integración: "produccion", "sandbox", "test".
        /// Las configuraciones de sandbox se usan en el entorno de staging de Railway.
        /// </summary>
        public string Ambiente { get; set; } = "produccion";

        /// <summary>Indica si esta configuración de integración está activa y disponible.</summary>
        public bool EstaActiva { get; set; } = true;

        /// <summary>
        /// Timeout máximo en segundos para las llamadas HTTP al sistema externo.
        /// Por defecto: 30 segundos. AEAT puede requerir hasta 60 segundos.
        /// </summary>
        public int TimeoutSegundos { get; set; } = 30;

        /// <summary>Número máximo de reintentos automáticos ante un error o timeout.</summary>
        public int MaxReintentos { get; set; } = 3;

        /// <summary>Fecha y hora UTC de la última conexión exitosa al sistema externo.</summary>
        public DateTime? FechaUltimaConexion { get; set; }

        /// <summary>
        /// Estado de la última conexión: "Exitosa", "Error", "SinProbar".
        /// Actualizado tras cada llamada al sistema externo.
        /// </summary>
        public string EstadoUltimaConexion { get; set; } = "SinProbar";

        /// <summary>
        /// Descripción del último error registrado en la conexión.
        /// Nulo si la última conexión fue exitosa.
        /// </summary>
        public string? UltimoError { get; set; }

        /// <summary>
        /// URL de recepción de webhooks del sistema externo para esta integración.
        /// Configurada en el sistema externo para recibir eventos de pago, firma, etc.
        /// Ejemplo: "https://cruiser.railway.app/webhooks/stripe".
        /// </summary>
        public string? UrlWebhookRecepcion { get; set; }

        /// <summary>
        /// Notas internas sobre la integración: configuración específica, limitaciones conocidas,
        /// persona de contacto en el proveedor, documentación de referencia.
        /// </summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Registros de log de las llamadas realizadas a través de esta integración.</summary>
        public virtual ICollection<LogIntegracion> Logs { get; set; }
            = new List<LogIntegracion>();

        /// <summary>Webhooks recibidos del sistema externo a través de esta integración.</summary>
        public virtual ICollection<WebhookRecibido> Webhooks { get; set; }
            = new List<WebhookRecibido>();
    }
}
