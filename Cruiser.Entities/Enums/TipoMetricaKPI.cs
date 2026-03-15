using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Categoría de negocio a la que pertenece la métrica KPI.
    /// Permite agrupar y filtrar KPIs en el dashboard por área funcional.
    /// </summary>
    public enum TipoMetricaKPI
    {
        /// <summary>KPI de operaciones: órdenes completadas, tiempos de servicio, incidencias.</summary>
        Operaciones = 1,
        /// <summary>KPI financiero: facturación, cobros, morosidad, margen, rentabilidad.</summary>
        Financiero = 2,
        /// <summary>KPI de clientes: satisfacción, NPS, retención, segmentación, campañas.</summary>
        Clientes = 3,
        /// <summary>KPI de RRHH: absentismo, horas trabajadas, formaciones, EPIs.</summary>
        RecursosHumanos = 4,
        /// <summary>KPI de inventario: rotación de stock, caducidades, diferencias de inventario.</summary>
        Inventario = 5,
        /// <summary>KPI de calidad: inspecciones, evaluaciones, puntuaciones de servicio.</summary>
        Calidad = 6,
        /// <summary>KPI de flota: kilómetros, consumo, costes de mantenimiento, disponibilidad.</summary>
        Flota = 7,
        /// <summary>KPI comercial: ventas, contratos nuevos, cotizaciones convertidas.</summary>
        Comercial = 8
    }
}
