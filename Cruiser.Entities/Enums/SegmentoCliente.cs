using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Segmento comercial asignado a un cliente según su valor, frecuencia
    /// y recencia de compra/contratación (análisis RFM).
    /// Determina el nivel de atención, descuentos y prioridad en la gestión comercial.
    /// </summary>
    public enum SegmentoCliente
    {
        /// <summary>
        /// Cliente VIP: alto valor, alta frecuencia y reciente. Prioridad máxima.
        /// Representa el top ~10% de clientes por facturación.
        /// </summary>
        VIP = 1,
        /// <summary>
        /// Cliente estándar: buen historial de contratación y pagos regulares.
        /// </summary>
        Estandar = 2,
        /// <summary>
        /// Cliente nuevo: registrado recientemente, aún sin historial suficiente para clasificar.
        /// </summary>
        Nuevo = 3,
        /// <summary>
        /// Cliente en riesgo: bajada reciente en frecuencia o valor de contratación.
        /// Requiere acción comercial preventiva.
        /// </summary>
        EnRiesgo = 4,
        /// <summary>
        /// Cliente inactivo: sin contrataciones en los últimos N meses (configurable).
        /// Candidato para campaña de reactivación.
        /// </summary>
        Inactivo = 5,
        /// <summary>
        /// Cliente en recuperación: estaba inactivo y ha vuelto a contratar recientemente.
        /// </summary>
        EnRecuperacion = 6
    }
}
