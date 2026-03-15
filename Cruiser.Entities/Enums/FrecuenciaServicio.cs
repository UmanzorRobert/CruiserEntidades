using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Frecuencia de prestación del servicio de limpieza contratado.
    /// Determina el intervalo entre órdenes de servicio generadas automáticamente.
    /// </summary>
    public enum FrecuenciaServicio
    {
        /// <summary>Servicio puntual o bajo demanda. No genera órdenes automáticas.</summary>
        Puntual = 1,
        /// <summary>Servicio diario de lunes a viernes.</summary>
        Diaria = 2,
        /// <summary>Servicio dos o tres veces por semana.</summary>
        VariasVecesSemana = 3,
        /// <summary>Servicio una vez por semana en el día pactado.</summary>
        Semanal = 4,
        /// <summary>Servicio dos veces al mes.</summary>
        Quincenal = 5,
        /// <summary>Servicio una vez al mes.</summary>
        Mensual = 6,
        /// <summary>Servicio trimestral (limpieza a fondo estacional).</summary>
        Trimestral = 7,
        /// <summary>Servicio semestral.</summary>
        Semestral = 8,
        /// <summary>Servicio anual (limpieza general de fin de año).</summary>
        Anual = 9
    }
}
