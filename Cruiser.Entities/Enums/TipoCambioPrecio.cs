using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Motivo o causa del cambio de precio de un producto registrado en HistorialPrecioProducto.
    /// </summary>
    public enum TipoCambioPrecio
    {
        /// <summary>Cambio por incremento de costos del proveedor.</summary>
        IncrementoCoste = 1,
        /// <summary>Cambio por reducción de costos o mejora en condiciones de compra.</summary>
        ReduccionCoste = 2,
        /// <summary>Ajuste por inflación o revisión periódica de precios.</summary>
        AjusteInflacion = 3,
        /// <summary>Cambio por política de precios de la empresa (nueva estrategia de margen).</summary>
        PoliticaEmpresa = 4,
        /// <summary>Precio especial de campaña o promoción temporal.</summary>
        Promocion = 5,
        /// <summary>Corrección de un error en el precio previamente registrado.</summary>
        Correccion = 6,
        /// <summary>Cambio por actualización de la lista de precios activa.</summary>
        ActualizacionLista = 7
    }
}
