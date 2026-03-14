using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Registro del estado de despliegue actual de la aplicación en Railway.
    /// Se alimenta automáticamente de las variables de entorno que Railway inyecta
    /// en el contenedor en cada deploy: RAILWAY_SERVICE_NAME, RAILWAY_ENVIRONMENT_NAME,
    /// RAILWAY_DEPLOYMENT_ID, RAILWAY_PROJECT_ID, RAILWAY_SERVICE_ID.
    ///
    /// Permite al administrador ver desde la interfaz web exactamente qué versión
    /// está desplegada, en qué environment de Railway, y el estado de salud del servicio.
    ///
    /// IConfiguracionDespliegueService.RegistrarNuevoDespliegue() se invoca desde
    /// el entrypoint.sh al arrancar el contenedor para persistir los valores Railway.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NombreServicioRailway).HasMaxLength(200);
    ///   builder.Property(x => x.RailwayEnvironmentName).HasMaxLength(100);
    ///   builder.Property(x => x.RailwayDeploymentId).HasMaxLength(200);
    ///   builder.Property(x => x.RailwayProjectId).HasMaxLength(200);
    ///   builder.Property(x => x.RailwayServiceId).HasMaxLength(200);
    ///   builder.Property(x => x.VersionImagenDesplegada).HasMaxLength(200);
    ///   builder.Property(x => x.UrlServicioRailway).HasMaxLength(500);
    ///   builder.Property(x => x.UrlHealthCheck).HasMaxLength(500);
    ///   builder.HasIndex(x => x.RailwayDeploymentId);
    ///   builder.HasIndex(x => x.FechaUltimoDespliegue);
    /// </remarks>
    public class ConfiguracionDespliegue : EntidadBase
    {
        // ── Variables inyectadas por Railway ────────────────────────────────

        /// <summary>
        /// Ambiente de despliegue actual (Development, Staging, Production).
        /// Leído de ASPNETCORE_ENVIRONMENT y mapeado al enum.
        /// </summary>
        public AmbienteDespliegue AmbienteDespliegue { get; set; } = AmbienteDespliegue.Development;

        /// <summary>
        /// Nombre del servicio en Railway (variable RAILWAY_SERVICE_NAME).
        /// Ejemplo: "cruiser-web", "cruiser-api".
        /// </summary>
        public string? NombreServicioRailway { get; set; }

        /// <summary>
        /// URL pública del servicio en Railway (dominio .railway.app o dominio personalizado).
        /// Ejemplo: "https://cruiser.railway.app" o "https://app.cruiser.com".
        /// </summary>
        public string? UrlServicioRailway { get; set; }

        /// <summary>
        /// Nombre del environment de Railway (variable RAILWAY_ENVIRONMENT_NAME).
        /// Valores: "production", "staging", "development".
        /// </summary>
        public string? RailwayEnvironmentName { get; set; }

        /// <summary>
        /// ID del proyecto Railway (variable RAILWAY_PROJECT_ID).
        /// Identificador único del proyecto en la plataforma Railway.
        /// </summary>
        public string? RailwayProjectId { get; set; }

        /// <summary>
        /// ID del servicio Railway (variable RAILWAY_SERVICE_ID).
        /// Identificador único del servicio (instancia) dentro del proyecto Railway.
        /// </summary>
        public string? RailwayServiceId { get; set; }

        /// <summary>
        /// ID del deployment específico (variable RAILWAY_DEPLOYMENT_ID).
        /// Cambia en cada deploy. Permite identificar exactamente qué despliegue está activo.
        /// </summary>
        public string? RailwayDeploymentId { get; set; }

        // ── Información de la imagen desplegada ──────────────────────────────

        /// <summary>
        /// Versión o tag de la imagen Docker desplegada.
        /// Puede ser el hash del commit de Git o una versión semántica.
        /// </summary>
        public string? VersionImagenDesplegada { get; set; }

        /// <summary>
        /// Fecha y hora UTC del último despliegue exitoso registrado.
        /// </summary>
        public DateTime? FechaUltimoDespliegue { get; set; }

        // ── Estado de salud del servicio ─────────────────────────────────────

        /// <summary>
        /// Estado actual del servicio según el último health check ejecutado.
        /// Ejemplo: "Healthy", "Degraded", "Unhealthy".
        /// </summary>
        public string? EstadoServicio { get; set; }

        /// <summary>
        /// URL del endpoint de health check del servicio.
        /// Por defecto "/health". Railway lo configura en Settings → Deploy → Health Check Path.
        /// </summary>
        public string UrlHealthCheck { get; set; } = "/health";

        /// <summary>
        /// Fecha y hora UTC del último health check ejecutado.
        /// </summary>
        public DateTime? FechaUltimoHealthCheck { get; set; }

        /// <summary>
        /// Resultado detallado del último health check en formato JSON.
        /// Incluye el estado de cada componente: BD, SMTP, Hangfire, SignalR.
        /// </summary>
        public string? ResultadoHealthCheckJSON { get; set; }

        /// <summary>
        /// Tiempo de actividad del servicio en segundos desde el último arranque.
        /// </summary>
        public long? UptimeSegundos { get; set; }

        /// <summary>
        /// Observaciones o notas sobre el despliegue actual.
        /// Ejemplo: "Deploy de hotfix para bug #123", "Versión con migración DB incluida".
        /// </summary>
        public string? Observaciones { get; set; }
    }
}
