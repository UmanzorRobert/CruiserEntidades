using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Formato de salida de un reporte generado por el sistema.
    /// Determina la librería usada para la generación: QuestPDF, ClosedXML o visualización web.
    /// </summary>
    public enum FormatoSalidaReporte
    {
        /// <summary>Reporte en formato PDF generado con QuestPDF. Descargable e imprimible.</summary>
        PDF = 1,
        /// <summary>Reporte en formato Excel (.xlsx) generado con ClosedXML. Editable.</summary>
        Excel = 2,
        /// <summary>Visualización en pantalla dentro de la interfaz web. Sin descarga.</summary>
        Pantalla = 3
    }
}
