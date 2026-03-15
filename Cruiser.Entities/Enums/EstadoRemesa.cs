using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del ciclo de vida de una remesa bancaria SEPA de domiciliación de recibos.
    /// Sigue el flujo de presentación al banco hasta la confirmación o devolución.
    /// </summary>
    public enum EstadoRemesa
    {
        /// <summary>Remesa en preparación. Facturas seleccionadas pero XML no generado.</summary>
        EnPreparacion = 1,
        /// <summary>Fichero XML SEPA generado y listo para presentar al banco.</summary>
        Generada = 2,
        /// <summary>Remesa presentada al banco para su procesamiento.</summary>
        Presentada = 3,
        /// <summary>Banco procesó la remesa. Todos los recibos cobrados exitosamente.</summary>
        Cobrada = 4,
        /// <summary>
        /// Remesa con devoluciones parciales (algunos recibos devueltos con códigos R-transaction).
        /// Requiere gestión individual de los recibos devueltos.
        /// </summary>
        ConDevoluciones = 5,
        /// <summary>Remesa cancelada antes de presentar al banco.</summary>
        Cancelada = 6
    }
}
