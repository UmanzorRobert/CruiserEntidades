using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo o estándar del código de barras o código bidimensional de un producto.
    /// Determina el algoritmo de lectura en QuaggaJS / ZXing-JS (escáner PWA).
    /// </summary>
    public enum TipoCodigoBarras
    {
        /// <summary>EAN-13: código de barras de 13 dígitos. Estándar europeo para productos de consumo.</summary>
        EAN13 = 1,
        /// <summary>EAN-8: código de barras de 8 dígitos. Para productos pequeños con espacio reducido.</summary>
        EAN8 = 2,
        /// <summary>UPC-A: código de barras de 12 dígitos. Estándar norteamericano.</summary>
        UPCA = 3,
        /// <summary>Code 128: código de barras alfanumérico de alta densidad. Para uso interno.</summary>
        Code128 = 4,
        /// <summary>Code 39: código de barras alfanumérico legacy. Usado en almacenes y logística.</summary>
        Code39 = 5,
        /// <summary>QR Code: código bidimensional con gran capacidad de datos.</summary>
        QR = 6,
        /// <summary>Data Matrix: código bidimensional compacto para espacios reducidos.</summary>
        DataMatrix = 7,
        /// <summary>PDF417: código de barras apilado para documentos (albaranes, etiquetas).</summary>
        PDF417 = 8
    }
}
