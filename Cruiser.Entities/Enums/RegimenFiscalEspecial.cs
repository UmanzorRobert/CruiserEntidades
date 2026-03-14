using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Régimen fiscal especial aplicable a una provincia o región española.
    /// Afecta al tipo de impuesto aplicable en facturas con clientes de esa región.
    /// </summary>
    public enum RegimenFiscalEspecial
    {
        /// <summary>Régimen general de IVA (Art. 68 LIVA). Aplica a la Península y Baleares.</summary>
        IVAGeneral = 1,
        /// <summary>
        /// IGIC - Impuesto General Indirecto Canario (Ley 20/1991).
        /// Aplica en las Islas Canarias. Tipo general 7%, reducido 3%, 0%.
        /// </summary>
        IGICCanarias = 2,
        /// <summary>
        /// IPSI - Impuesto sobre la Producción, los Servicios e Importación.
        /// Aplica en Ceuta (Ley 8/1991) y Melilla (Ley 8/1991).
        /// </summary>
        IPSICeutaMelilla = 3,
        /// <summary>Sin régimen especial (territorio fuera del ámbito fiscal español).</summary>
        SinRegimenEspecial = 4
    }
}
