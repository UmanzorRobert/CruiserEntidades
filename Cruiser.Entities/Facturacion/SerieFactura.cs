using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Facturacion
{
    /// <summary>
    /// Serie de numeración de facturas que garantiza la secuencia correlativa
    /// sin saltos dentro de cada ejercicio fiscal.
    ///
    /// La normativa fiscal española (Art. 6 RD 1619/2012) exige que las facturas
    /// tengan numeración correlativa dentro de cada serie y ejercicio.
    /// El sistema no permite saltos en la numeración ni eliminación de facturas emitidas.
    ///
    /// Una empresa puede tener múltiples series por ejercicio:
    /// serie "A" para servicios, serie "R" para rectificativas, etc.
    ///
    /// SEED: Serie "A" (Normal), Serie "R" (Rectificativa) para el ejercicio actual.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Serie).IsRequired().HasMaxLength(10);
    ///   builder.Property(x => x.Prefijo).HasMaxLength(10);
    ///   builder.HasIndex(x => new { x.Serie, x.Ejercicio }).IsUnique();
    ///   builder.HasIndex(x => x.EsPorDefecto).HasFilter("\"EsPorDefecto\" = true");
    /// </remarks>
    public class SerieFactura : EntidadBase
    {
        /// <summary>
        /// Código de la serie. Ejemplo: "A", "B", "R", "PRO".
        /// Junto con el ejercicio y el número forma el número completo de factura.
        /// </summary>
        public string Serie { get; set; } = string.Empty;

        /// <summary>
        /// Ejercicio fiscal al que corresponde esta serie (año de 4 dígitos).
        /// Ejemplo: 2026. La numeración se reinicia a 1 cada ejercicio.
        /// </summary>
        public int Ejercicio { get; set; }

        /// <summary>
        /// Último número emitido en esta serie del ejercicio.
        /// El próximo número será UltimoNumero + 1 (garantiza correlatividad).
        /// Se incrementa de forma atómica en la base de datos para evitar duplicados.
        /// </summary>
        public int UltimoNumero { get; set; } = 0;

        /// <summary>
        /// Prefijo textual opcional antes del número. Ejemplo: "FAC-", "OC-".
        /// El número completo sería: Prefijo + Serie + Ejercicio + NumeroFormateado.
        /// </summary>
        public string? Prefijo { get; set; }

        /// <summary>
        /// Número de dígitos del número correlativo con ceros a la izquierda.
        /// Ejemplo: 4 dígitos → "0001", "0042", "1000". Por defecto: 4.
        /// </summary>
        public int NumeroDigitos { get; set; } = 4;

        /// <summary>
        /// Tipo de factura que emite esta serie.
        /// Determina las validaciones y la cadena VeriFactu aplicable.
        /// </summary>
        public TipoFactura TipoFactura { get; set; } = TipoFactura.Normal;

        /// <summary>
        /// Indica si esta es la serie por defecto para el tipo de factura correspondiente.
        /// Al emitir una nueva factura, si no se especifica serie, se usa la serie por defecto.
        /// </summary>
        public bool EsPorDefecto { get; set; } = false;

        /// <summary>Descripción de la serie y su uso en la empresa.</summary>
        public string? Descripcion { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Facturas emitidas bajo esta serie y ejercicio.</summary>
        public virtual ICollection<Factura> Facturas { get; set; }
            = new List<Factura>();
    }
}
