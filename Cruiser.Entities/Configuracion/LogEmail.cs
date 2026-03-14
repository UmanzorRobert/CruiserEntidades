using System;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Registro de cada intento de envío de email del sistema.
    /// Permite auditar todos los emails enviados, detectar fallos de entrega,
    /// gestionar reintentos y monitorear la efectividad de las comunicaciones.
    ///
    /// Se crea un registro por cada email, independientemente de si el envío fue exitoso.
    /// Los emails fallidos muestran el error en UltimoError y se reintentarán por Hangfire.
    ///
    /// NO hereda de EntidadBase: registro append-only de operaciones de envío.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.DestinatarioEmail).IsRequired().HasMaxLength(256);
    ///   builder.Property(x => x.Asunto).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.EntidadId).HasMaxLength(36);
    ///   builder.Property(x => x.TipoContexto).HasMaxLength(100);
    ///   builder.HasIndex(x => x.Estado);
    ///   builder.HasIndex(x => new { x.TipoContexto, x.EntidadId });
    ///   builder.HasIndex(x => x.FechaEnvio);
    ///   builder.HasIndex(x => x.DestinatarioEmail);
    /// </remarks>
    public class LogEmail
    {
        /// <summary>Identificador único del registro de log de email.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>Dirección de email del destinatario principal del mensaje.</summary>
        public string DestinatarioEmail { get; set; } = string.Empty;

        /// <summary>
        /// Nombre del destinatario para personalizar el saludo del email.
        /// Puede ser nulo si no se conoce el nombre.
        /// </summary>
        public string? DestinatarioNombre { get; set; }

        /// <summary>Asunto del email enviado.</summary>
        public string Asunto { get; set; } = string.Empty;

        /// <summary>Estado actual del proceso de envío del email.</summary>
        public EstadoLogEmail Estado { get; set; } = EstadoLogEmail.Pendiente;

        /// <summary>
        /// Fecha y hora UTC del último intento de envío del email.
        /// Nulo si el email está en cola pero aún no se ha intentado enviar.
        /// </summary>
        public DateTime? FechaEnvio { get; set; }

        /// <summary>
        /// Fecha y hora UTC en que se creó el registro (se puso en cola el email).
        /// </summary>
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        /// <summary>Número total de intentos de envío realizados para este email.</summary>
        public int NumeroIntentos { get; set; } = 0;

        /// <summary>
        /// Descripción del último error producido al intentar enviar el email.
        /// Nulo si el envío fue exitoso o si aún no se ha intentado.
        /// </summary>
        public string? UltimoError { get; set; }

        /// <summary>
        /// Tipo de entidad o contexto que originó el envío del email.
        /// Ejemplo: "Factura", "OrdenServicio", "RecuperacionPassword", "AlertaStock".
        /// </summary>
        public string? TipoContexto { get; set; }

        /// <summary>
        /// Identificador de la entidad específica que originó el email.
        /// Permite rastrear todos los emails relacionados con una factura, orden, etc.
        /// </summary>
        public string? EntidadId { get; set; }

        /// <summary>
        /// Prioridad de envío del email en la cola de Hangfire.
        /// Los emails críticos (seguridad, GDPR) se procesan antes que los informativos.
        /// </summary>
        public PrioridadCola Prioridad { get; set; } = PrioridadCola.Normal;

        /// <summary>
        /// Contenido HTML completo del email enviado.
        /// Se almacena para poder reenviar el email exactamente igual sin regenerarlo.
        /// </summary>
        public string? ContenidoHTML { get; set; }

        /// <summary>
        /// Identificador del job de Hangfire que gestiona el envío de este email.
        /// Permite relacionar el log con el panel de Hangfire para diagnóstico.
        /// </summary>
        public string? HangfireJobId { get; set; }
    }
}
