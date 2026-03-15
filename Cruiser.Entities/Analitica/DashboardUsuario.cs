using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Analitica
{
    /// <summary>
    /// Configuración personalizada del dashboard ejecutivo para un usuario específico.
    /// Permite a cada usuario elegir qué KPIs, gráficos y widgets ve en su dashboard,
    /// en qué orden los visualiza y qué período de tiempo muestran por defecto.
    ///
    /// LayoutJSON almacena la disposición de los widgets en formato JSONB compatible
    /// con la librería de drag-drop del dashboard Blazor Server:
    /// [
    ///   { "widgetId": "kpi-facturacion", "columna": 0, "fila": 0, "ancho": 3, "alto": 1 },
    ///   { "widgetId": "chart-ordenes", "columna": 3, "fila": 0, "ancho": 9, "alto": 2 },
    ///   { "widgetId": "tabla-alertas", "columna": 0, "fila": 1, "ancho": 12, "alto": 2 }
    /// ]
    ///
    /// FiltrosGlobalesJSON almacena los filtros que aplican a todos los widgets del dashboard:
    /// { "periodoDefecto": "mes_actual", "zonaServicioId": null, "empleadoId": null }
    ///
    /// Al no existir un registro para un usuario, el dashboard muestra la
    /// configuración por defecto definida en ConfiguracionSistema.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => x.UsuarioId).IsUnique();
    ///   builder.Property(x => x.LayoutJSON).HasColumnType("jsonb");
    ///   builder.Property(x => x.FiltrosGlobalesJSON).HasColumnType("jsonb");
    ///   builder.HasIndex(x => x.LayoutJSON).HasMethod("gin");
    /// </remarks>
    public class DashboardUsuario : EntidadBase
    {
        /// <summary>FK hacia el usuario propietario de esta configuración de dashboard. Índice único (1-a-1).</summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Disposición de los widgets del dashboard en formato JSONB.
        /// Define el tipo de widget, su posición en la cuadrícula y sus dimensiones.
        /// Compatible con la biblioteca de grid drag-drop del componente Blazor.
        /// </summary>
        public string? LayoutJSON { get; set; }

        /// <summary>
        /// Filtros globales aplicados a todos los widgets del dashboard en formato JSONB.
        /// Incluye: período de tiempo por defecto, zona de servicio, empleado o cliente filtrado.
        /// </summary>
        public string? FiltrosGlobalesJSON { get; set; }

        /// <summary>
        /// Período de tiempo por defecto mostrado en el dashboard al abrirlo.
        /// Valores: "hoy", "semana_actual", "mes_actual", "trimestre_actual", "año_actual".
        /// </summary>
        public string PeriodoDefecto { get; set; } = "mes_actual";

        /// <summary>
        /// Indica si el usuario ha personalizado su dashboard
        /// o sigue usando la configuración por defecto del sistema.
        /// </summary>
        public bool EsPersonalizado { get; set; } = false;

        /// <summary>
        /// Indica si el dashboard se actualiza automáticamente con los datos en tiempo real
        /// mediante SignalR o si el usuario actualiza manualmente.
        /// </summary>
        public bool ActualizacionAutomatica { get; set; } = true;

        /// <summary>
        /// Intervalo en segundos entre actualizaciones automáticas del dashboard.
        /// Solo aplica cuando ActualizacionAutomatica = true. Por defecto: 300 (5 minutos).
        /// </summary>
        public int IntervaloActualizacionSegundos { get; set; } = 300;

        /// <summary>Fecha y hora UTC de la última vez que el usuario guardó su configuración de dashboard.</summary>
        public DateTime? FechaUltimaPersonalizacion { get; set; }
    }
}
