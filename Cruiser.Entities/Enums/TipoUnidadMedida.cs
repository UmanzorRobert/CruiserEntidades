using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Categoría o dimensión física que mide una unidad de medida del sistema.
    /// Permite agrupar y convertir unidades compatibles entre sí.
    /// </summary>
    public enum TipoUnidadMedida
    {
        /// <summary>Unidades discretas sin dimensión física (piezas, unidades, cajas, rollos).</summary>
        Unidad = 1,
        /// <summary>Masa o peso (gramos, kilogramos, libras, toneladas).</summary>
        Masa = 2,
        /// <summary>Volumen de líquidos o gases (mililitros, litros, galones).</summary>
        Volumen = 3,
        /// <summary>Longitud o distancia (centímetros, metros, pies).</summary>
        Longitud = 4,
        /// <summary>Superficie o área (metros cuadrados, pies cuadrados).</summary>
        Superficie = 5,
        /// <summary>Tiempo (minutos, horas, días). Para servicios facturados por tiempo.</summary>
        Tiempo = 6,
        /// <summary>Servicio o trabajo intangible (visita, servicio, intervención).</summary>
        Servicio = 7
    }
}
