using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Catálogo de monedas utilizadas en el sistema para facturación multi-divisa.
    /// La moneda base del sistema (generalmente EUR) tiene EsMonedaBase = true
    /// y su TipoCambio siempre es 1.00.
    ///
    /// Los tipos de cambio se actualizan manualmente o mediante integración externa (FASE 19).
    /// Todos los importes se almacenan en la moneda de la transacción y se convierten
    /// a la moneda base para reportes y analítica consolidada.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.CodigoISO).IsRequired().HasMaxLength(3);
    ///   builder.Property(x => x.Simbolo).IsRequired().HasMaxLength(5);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.TipoCambio).HasPrecision(18, 6);
    ///   builder.HasIndex(x => x.CodigoISO).IsUnique();
    ///   builder.HasIndex(x => x.EsMonedaBase).HasFilter("\"EsMonedaBase\" = true");
    /// </remarks>
    public class Moneda : EntidadBase
    {
        /// <summary>
        /// Código ISO 4217 de tres letras de la moneda. Clave de negocio única.
        /// Ejemplos: "EUR", "USD", "GBP", "MXN".
        /// </summary>
        public string CodigoISO { get; set; } = string.Empty;

        /// <summary>
        /// Símbolo visual de la moneda para mostrar en la interfaz y documentos.
        /// Ejemplos: "€", "$", "£", "¥".
        /// </summary>
        public string Simbolo { get; set; } = string.Empty;

        /// <summary>Nombre completo de la moneda en español.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Separador decimal usado para esta moneda en la interfaz.
        /// Español/Europeo: "," | Anglosajón: ".".
        /// </summary>
        public string SeparadorDecimal { get; set; } = ",";

        /// <summary>
        /// Separador de miles usado para esta moneda en la interfaz.
        /// Español/Europeo: "." | Anglosajón: ",".
        /// </summary>
        public string SeparadorMiles { get; set; } = ".";

        /// <summary>
        /// Tipo de cambio respecto a la moneda base del sistema.
        /// Para la moneda base siempre es 1.000000.
        /// Para otras monedas: 1 EUR = X moneda_extranjera.
        /// </summary>
        public decimal TipoCambio { get; set; } = 1.000000m;

        /// <summary>
        /// Fecha y hora UTC de la última actualización del tipo de cambio.
        /// </summary>
        public DateTime? FechaActualizacionTipoCambio { get; set; }

        /// <summary>
        /// Indica si esta es la moneda base del sistema (normalmente EUR).
        /// Solo puede haber una moneda base activa al mismo tiempo.
        /// </summary>
        public bool EsMonedaBase { get; set; } = false;

        /// <summary>
        /// Posición del símbolo respecto al importe en la UI y documentos.
        /// True = símbolo antes (€100.00) | False = símbolo después (100.00€).
        /// </summary>
        public bool SimboloAntes { get; set; } = false;

        /// <summary>Número de decimales a mostrar para esta moneda. Por defecto 2.</summary>
        public int NumeroDecimales { get; set; } = 2;
    }
}
