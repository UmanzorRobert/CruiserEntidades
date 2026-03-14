using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Configuración del motor de renderizado frontend para cada módulo del sistema.
    /// Permite definir qué tecnología (Razor, Blazor, Vue, React, PWA) usa cada módulo
    /// sin modificar el código fuente, habilitando una arquitectura frontend híbrida.
    ///
    /// Por ejemplo: Dashboard usa Blazor, listados CRUD usan Razor,
    /// módulo de fichaje usa PWA, Kanban usa React embebido en Blazor.
    ///
    /// SEED INICIAL: configuración por módulo según las recomendaciones del plan de fases.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NombreModulo).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.RutaModulo).HasMaxLength(200);
    ///   builder.HasIndex(x => x.NombreModulo).IsUnique();
    /// </remarks>
    public class ConfiguracionFrontend : EntidadBase
    {
        /// <summary>
        /// Nombre del módulo funcional al que aplica esta configuración.
        /// Debe coincidir con el Codigo del Modulo correspondiente.
        /// Ejemplo: "DASHBOARD", "INVENTARIO_PRODUCTOS", "FICHAJE", "CALENDARIO".
        /// </summary>
        public string NombreModulo { get; set; } = string.Empty;

        /// <summary>
        /// Ruta relativa del módulo en la aplicación.
        /// Ejemplo: "/dashboard", "/inventario/productos", "/fichaje".
        /// </summary>
        public string? RutaModulo { get; set; }

        /// <summary>
        /// Motor de renderizado frontend a usar para este módulo.
        /// </summary>
        public TipoFrontend TipoFrontend { get; set; } = TipoFrontend.Razor;

        /// <summary>
        /// Indica si el módulo debe funcionar sin conexión (modo offline).
        /// Solo relevante para módulos con TipoFrontend = PWA.
        /// </summary>
        public bool HabilitarOffline { get; set; } = false;

        /// <summary>
        /// Indica si el módulo usa SignalR nativo de Blazor Server.
        /// Solo relevante para módulos con TipoFrontend = Blazor.
        /// False indica que usa el cliente JavaScript de SignalR.
        /// </summary>
        public bool HabilitarSignalRNativo { get; set; } = false;

        /// <summary>
        /// Motivo técnico o funcional por el que se eligió este motor para el módulo.
        /// Documentación para el equipo de desarrollo. No se muestra en producción.
        /// </summary>
        public string? MotivoEleccion { get; set; }

        /// <summary>
        /// Indica si esta configuración de frontend está activa.
        /// Permite sobreescribir la configuración por defecto para pruebas A/B de rendimiento.
        /// </summary>
        public bool EsActiva { get; set; } = true;
    }
}
