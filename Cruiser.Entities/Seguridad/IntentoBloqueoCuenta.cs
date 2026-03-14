using System;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Seguridad
{
    /// <summary>
    /// Registro de cada evento de bloqueo de cuenta de usuario y su posterior desbloqueo.
    /// Proporciona trazabilidad completa del ciclo bloqueo→desbloqueo para auditoría
    /// de seguridad y para detectar patrones de ataque recurrentes.
    ///
    /// Se crea un registro cuando una cuenta alcanza el número máximo de intentos fallidos
    /// configurado en ParametroSistema (por defecto 5 intentos consecutivos).
    ///
    /// NO hereda de EntidadBase: registro append-only de seguridad.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.DireccionIP).HasMaxLength(45);
    ///   builder.Property(x => x.MotivoBloqueo).HasMaxLength(500);
    ///   builder.HasIndex(x => new { x.UsuarioId, x.FechaIntento });
    ///   builder.HasIndex(x => x.DireccionIP);
    /// </remarks>
    public class IntentoBloqueoCuenta
    {
        /// <summary>Identificador único del registro de intento de bloqueo.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Identificador del usuario cuya cuenta fue bloqueada.
        /// FK hacia la tabla Usuarios.
        /// </summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Dirección IP desde la que se realizaron los intentos fallidos
        /// que desencadenaron el bloqueo de la cuenta.
        /// </summary>
        public string? DireccionIP { get; set; }

        /// <summary>
        /// Fecha y hora UTC en que se produjo el bloqueo de la cuenta.
        /// </summary>
        public DateTime FechaIntento { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Descripción del motivo por el que se bloqueó la cuenta.
        /// Ejemplo: "5 intentos fallidos consecutivos desde IP 192.168.1.1".
        /// </summary>
        public string? MotivoBloqueo { get; set; }

        /// <summary>
        /// Número de intentos fallidos consecutivos que provocaron el bloqueo.
        /// </summary>
        public int NumeroIntentosFallidos { get; set; }

        /// <summary>
        /// Fecha y hora UTC hasta la que la cuenta estará bloqueada automáticamente.
        /// Nulo si el bloqueo es indefinido y requiere desbloqueo manual por un admin.
        /// </summary>
        public DateTime? FechaBloqueoHasta { get; set; }

        /// <summary>
        /// Indica si la cuenta ya fue desbloqueada.
        /// </summary>
        public bool EstaDesbloqueada { get; set; } = false;

        /// <summary>
        /// Identificador del administrador que desbloqueó manualmente la cuenta.
        /// Nulo si el desbloqueo fue automático por expiración del tiempo de bloqueo.
        /// </summary>
        public Guid? DesbloqueadoPorId { get; set; }

        /// <summary>
        /// Fecha y hora UTC en que se realizó el desbloqueo de la cuenta.
        /// Nulo si la cuenta sigue bloqueada.
        /// </summary>
        public DateTime? FechaDesbloqueo { get; set; }

        /// <summary>
        /// Método por el que se desbloqueó la cuenta.
        /// </summary>
        public MetodoDesbloqueo? MetodoDesbloqueo { get; set; }

        /// <summary>
        /// Notas del administrador al desbloquear la cuenta.
        /// Ejemplo: "Usuario verificó identidad por teléfono", "Falso positivo confirmado".
        /// </summary>
        public string? NotasDesbloqueo { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Usuario cuya cuenta fue bloqueada.</summary>
        public virtual Usuario? Usuario { get; set; }
    }
}
