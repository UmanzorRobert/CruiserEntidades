using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Área funcional del sistema a la que pertenece un módulo de menú.
    /// Permite agrupar módulos en el sidebar por sección lógica.
    /// </summary>
    public enum AreaModulo
    {
        /// <summary>Panel principal con KPIs y resumen ejecutivo.</summary>
        Dashboard = 1,
        /// <summary>Gestión de inventario, productos, almacenes y movimientos.</summary>
        Inventario = 2,
        /// <summary>Compras, proveedores y órdenes de compra.</summary>
        Compras = 3,
        /// <summary>Clientes, empleados, cotizaciones y asignaciones.</summary>
        Comercial = 4,
        /// <summary>Facturación, cobros, remesas y precios.</summary>
        Facturacion = 5,
        /// <summary>Calendario, horarios, eventos y citas.</summary>
        Calendario = 6,
        /// <summary>Contratos, órdenes de servicio y dominio de limpieza.</summary>
        Operaciones = 7,
        /// <summary>Checklists, inspecciones, calidad y NPS.</summary>
        Calidad = 8,
        /// <summary>Recursos humanos: fichajes, ausencias, nóminas, EPIs.</summary>
        RRHH = 9,
        /// <summary>Gestión de flota de vehículos y rutas.</summary>
        Flota = 10,
        /// <summary>Reportes, KPIs y analítica de negocio.</summary>
        Analitica = 11,
        /// <summary>Configuración del sistema, parámetros y administración.</summary>
        Administracion = 12,
        /// <summary>Módulos relacionados con GDPR y cumplimiento normativo.</summary>
        GDPR = 13,
        /// <summary>Integraciones externas (AEAT, pasarelas de pago, etc.).</summary>
        Integraciones = 14
    }
}
