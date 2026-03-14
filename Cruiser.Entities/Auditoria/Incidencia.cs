using Cruiser.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Auditoria
{
    /// <summary>
    /// Registro de incidencias de seguridad del sistema.
    /// Complementa la Bitácora con información específica de eventos de seguridad:
    /// intentos de acceso fallidos, ataques de fuerza bruta, tokens inválidos,
    /// violaciones de rate limiting, accesos no autorizados, etc.
    ///
    /// Diseñado para ser procesado por sistemas de detección de amenazas (IDS)
    /// y para generar alertas automáticas cuando se superan umbrales configurables.
    ///
    /// NO hereda de EntidadBase: registro append-only de seguridad, inmutable por diseño.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.DireccionIP).HasMaxLength(45);
    ///   builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(2000);
    ///   builder.Property(x => x.RecursoAfectado).HasMaxLength(500);
    ///   builder.Property(x => x.UserAgent).HasMaxLength(500);
    ///
    ///   Índices:
    ///   builder.HasIndex(x => x.Fecha);
    ///   builder.HasIndex(x => x.DireccionIP);
    ///   builder.HasIndex(x => x.Tipo);
    ///   builder.HasIndex(x => new { x.DireccionIP, x.Fecha });  // detección fuerza bruta
    /// </remarks>
    public class Incidencia
    {
        /// <summary>Identificador único del registro de incidencia.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Fecha y hora UTC exacta en que se detectó la incidencia de seguridad.
        /// </summary>
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Tipo de incidencia de seguridad detectada.
        /// Permite clasificar y filtrar incidencias por categoría para análisis.
        /// </summary>
        public TipoIncidencia Tipo { get; set; }

        /// <summary>
        /// Descripción detallada de la incidencia.
        /// Debe incluir contexto suficiente para que el administrador pueda evaluar el riesgo.
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del usuario implicado en la incidencia.
        /// Nulo si la incidencia proviene de un usuario no autenticado o desconocido.
        /// No se define FK explícita para preservar el registro aunque el usuario sea eliminado.
        /// </summary>
        public Guid? UsuarioId { get; set; }

        /// <summary>
        /// Email o nombre de usuario implicado, capturado en el momento de la incidencia.
        /// Permite identificar el usuario aunque su cuenta sea posteriormente anonimizada.
        /// Puede ser el email introducido en un intento de login fallido.
        /// </summary>
        public string? IdentificadorUsuario { get; set; }

        /// <summary>
        /// Dirección IP desde la que se originó la incidencia.
        /// Campo clave para detección de fuerza bruta y bloqueo de IPs maliciosas.
        /// Soporta IPv4 e IPv6.
        /// </summary>
        public string? DireccionIP { get; set; }

        /// <summary>
        /// Recurso o endpoint del sistema al que se intentó acceder.
        /// Ejemplo: "/api/auth/login", "/admin/configuracion", "/fichaje".
        /// </summary>
        public string? RecursoAfectado { get; set; }

        /// <summary>
        /// User-Agent del cliente que generó la incidencia.
        /// Útil para identificar bots, scripts automatizados o herramientas de ataque.
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// Indica si la incidencia fue resuelta o gestionada por un administrador.
        /// Las incidencias no gestionadas aparecen en el dashboard de seguridad como pendientes.
        /// </summary>
        public bool EstaGestionada { get; set; } = false;

        /// <summary>
        /// Fecha y hora UTC en que la incidencia fue marcada como gestionada.
        /// </summary>
        public DateTime? FechaGestion { get; set; }

        /// <summary>
        /// Identificador del administrador que gestionó la incidencia.
        /// Nulo si aún no ha sido gestionada.
        /// </summary>
        public Guid? GestionadaPorId { get; set; }

        /// <summary>
        /// Notas o acciones tomadas por el administrador al gestionar la incidencia.
        /// Ejemplo: "IP bloqueada en firewall", "Usuario notificado por email", "Falso positivo".
        /// </summary>
        public string? NotasGestion { get; set; }

        /// <summary>
        /// Identificador de correlación de la petición HTTP que generó la incidencia.
        /// Permite cruzar este registro con Bitácora y Serilog logs para análisis forense.
        /// </summary>
        public string? TraceId { get; set; }
    }
}
