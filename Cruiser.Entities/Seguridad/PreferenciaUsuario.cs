using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Seguridad
{
    /// <summary>
    /// Preferencias personalizadas de interfaz y comportamiento del sistema para un usuario.
    /// Relación 1-a-1 con Usuario. Se crea automáticamente al registrar un nuevo usuario
    /// con los valores por defecto del sistema.
    ///
    /// Almacena preferencias de tema visual, idioma, notificaciones, paginación
    /// y página de inicio personalizada.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.CodigoIdioma).HasMaxLength(10).HasDefaultValue("es-ES");
    ///   builder.Property(x => x.Tema).HasMaxLength(50).HasDefaultValue("light");
    ///   builder.Property(x => x.PaginaInicio).HasMaxLength(200);
    ///   builder.HasIndex(x => x.UsuarioId).IsUnique();
    ///
    ///   Relación:
    ///   builder.HasOne(p => p.Usuario).WithOne(u => u.Preferencias)
    ///          .HasForeignKey&lt;PreferenciaUsuario&gt;(p => p.UsuarioId);
    /// </remarks>
    public class PreferenciaUsuario : EntidadBase
    {
        /// <summary>
        /// Identificador del usuario al que pertenecen estas preferencias.
        /// FK hacia Usuarios. Índice único (1 preferencia por usuario).
        /// </summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Código de idioma preferido del usuario en formato BCP-47.
        /// Valores soportados: "es-ES" (español), "en-US" (inglés), "ca-ES" (catalán).
        /// Por defecto "es-ES". Controla el idioma de la UI, emails y documentos generados.
        /// </summary>
        public string CodigoIdioma { get; set; } = "es-ES";

        /// <summary>
        /// Tema visual de la interfaz de usuario.
        /// Valores: "light" (claro), "dark" (oscuro), "auto" (según SO).
        /// Basado en SB Admin 2 que soporta tema claro nativo.
        /// </summary>
        public string Tema { get; set; } = "light";

        /// <summary>
        /// Número de registros por página en listados y tablas DataTables.
        /// Valores recomendados: 10, 25, 50, 100. Por defecto 25.
        /// </summary>
        public int RegistrosPorPagina { get; set; } = 25;

        /// <summary>
        /// Ruta relativa de la página de inicio al hacer login.
        /// Nulo para usar la página de inicio por defecto del sistema (/dashboard).
        /// Ejemplo: "/ordenes-servicio", "/calendario", "/facturas".
        /// </summary>
        public string? PaginaInicio { get; set; }

        /// <summary>
        /// Indica si el usuario desea recibir notificaciones en pantalla (SignalR).
        /// Las notificaciones críticas se envían siempre independientemente de este flag.
        /// </summary>
        public bool RecibirNotificacionesSistema { get; set; } = true;

        /// <summary>
        /// Indica si el usuario desea recibir notificaciones por email.
        /// Solo afecta a notificaciones no críticas (recordatorios, resúmenes).
        /// </summary>
        public bool RecibirNotificacionesEmail { get; set; } = true;

        /// <summary>
        /// Indica si el usuario desea recibir notificaciones push en el navegador.
        /// Requiere que el usuario haya otorgado permiso de notificaciones en el navegador.
        /// </summary>
        public bool RecibirNotificacionesPush { get; set; } = false;

        /// <summary>
        /// Indica si el sidebar de navegación está en modo colapsado (solo iconos)
        /// o expandido (iconos + texto). Se persiste entre sesiones.
        /// </summary>
        public bool SidebarColapsado { get; set; } = false;

        /// <summary>
        /// Zona horaria preferida del usuario en formato IANA.
        /// Ejemplo: "Europe/Madrid", "Atlantic/Canary", "America/Mexico_City".
        /// Por defecto "Europe/Madrid". Se usa para mostrar fechas en la UI.
        /// </summary>
        public string ZonaHoraria { get; set; } = "Europe/Madrid";

        /// <summary>
        /// Formato de fecha preferido para mostrar en la interfaz.
        /// Ejemplos: "dd/MM/yyyy" (España), "MM/dd/yyyy" (USA), "yyyy-MM-dd" (ISO).
        /// </summary>
        public string FormatoFecha { get; set; } = "dd/MM/yyyy";

        /// <summary>
        /// Fecha y hora UTC de la última vez que el usuario modificó sus preferencias.
        /// </summary>
        public DateTime? FechaUltimaModificacion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Usuario propietario de estas preferencias.</summary>
        public virtual Usuario? Usuario { get; set; }
    }
}
