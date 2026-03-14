using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Motor de renderizado frontend utilizado para un módulo específico del sistema.
    /// Permite que diferentes módulos usen diferentes tecnologías en la misma aplicación.
    /// </summary>
    public enum TipoFrontend
    {
        /// <summary>Razor Pages o MVC clásico. Para CRUD estándar con Bootstrap 5 / SB Admin 2.</summary>
        Razor = 1,
        /// <summary>Blazor Server. Para módulos interactivos con SignalR nativo (Dashboard, Calendario).</summary>
        Blazor = 2,
        /// <summary>Componente Vue.js embebido en Razor/Blazor. Para sub-componentes muy interactivos.</summary>
        Vue = 3,
        /// <summary>Componente React embebido. Para widgets de alta interactividad (Kanban, Charts).</summary>
        React = 4,
        /// <summary>PWA standalone. Para páginas de campo accesibles offline (Fichaje, Órdenes).</summary>
        PWA = 5
    }
}
