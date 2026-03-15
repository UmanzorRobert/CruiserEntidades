using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>Tipo de gasto asociado al uso o mantenimiento de un vehículo de la flota.</summary>
    public enum TipoGastoVehiculo
    {
        /// <summary>Repostaje de combustible (gasolina, diesel, GLP).</summary>
        Combustible = 1,
        /// <summary>Peaje de autopista o vía de peaje.</summary>
        Peaje = 2,
        /// <summary>Aparcamiento o parking.</summary>
        Parking = 3,
        /// <summary>Multa de tráfico. Requiere aprobación del responsable de flota.</summary>
        Multa = 4,
        /// <summary>Reparación o pieza de recambio.</summary>
        Reparacion = 5,
        /// <summary>Lavado o limpieza del vehículo.</summary>
        Lavado = 6,
        /// <summary>Accesorios o equipamiento adicional instalado en el vehículo.</summary>
        Accesorios = 7,
        /// <summary>Otro gasto no categorizado.</summary>
        Otro = 8
    }
}
