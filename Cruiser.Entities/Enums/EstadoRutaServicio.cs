using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>Estado del ciclo de vida de una ruta de servicio diaria.</summary>
    public enum EstadoRutaServicio
    {
        /// <summary>Ruta planificada pero no iniciada. El vehículo aún no ha salido.</summary>
        Planificada = 1,
        /// <summary>Ruta en curso. El vehículo está realizando las visitas programadas.</summary>
        EnCurso = 2,
        /// <summary>Ruta completada. Todas las órdenes han sido atendidas y el vehículo ha vuelto.</summary>
        Completada = 3,
        /// <summary>Ruta cancelada antes de comenzar.</summary>
        Cancelada = 4
    }
}
