using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de registro en el historial de stock de un producto en un almacén.
    /// Indica el contexto que generó el snapshot de stock en ese momento.
    /// </summary>
    public enum TipoRegistroStock
    {
        /// <summary>Snapshot generado tras un movimiento de entrada de mercancía.</summary>
        Entrada = 1,
        /// <summary>Snapshot generado tras un movimiento de salida de mercancía.</summary>
        Salida = 2,
        /// <summary>Snapshot generado tras un ajuste manual de stock.</summary>
        Ajuste = 3,
        /// <summary>Snapshot de cierre diario generado por job de Hangfire a medianoche.</summary>
        CierreDiario = 4,
        /// <summary>Snapshot inicial al crear el producto en el sistema o al activar el control de stock.</summary>
        StockInicial = 5,
        /// <summary>Snapshot generado tras completar un inventario cíclico.</summary>
        InventarioCiclico = 6,
        /// <summary>Snapshot generado tras una reserva o liberación de reserva de stock.</summary>
        Reserva = 7
    }
}
