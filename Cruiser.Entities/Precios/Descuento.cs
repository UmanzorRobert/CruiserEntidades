using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Precios
{
    /// <summary>
    /// Descuento comercial o cupón promocional aplicable en cotizaciones y facturas.
    /// Puede ser un descuento porcentual, importe fijo o precio especial.
    /// Puede limitarse a productos, categorías, clientes o períodos específicos.
    ///
    /// Los descuentos con CodigoCupon se aplican manualmente introduciendo el código
    /// en la pantalla de facturación. Los descuentos sin cupón se aplican automáticamente
    /// cuando se cumplen las condiciones de segmento, fecha y producto.
    ///
    /// LimiteUsos controla que un cupón no se use más del número de veces permitido.
    /// EsAcumulable determina si puede combinarse con otros descuentos en la misma factura.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150);
    ///   builder.Property(x => x.CodigoCupon).HasMaxLength(50);
    ///   builder.Property(x => x.ValorDescuento).HasPrecision(18, 4);
    ///   builder.Property(x => x.ImporteMinimoPedido).HasPrecision(18, 2);
    ///   builder.Property(x => x.ImporteMaximoDescuento).HasPrecision(18, 2);
    ///   builder.HasIndex(x => x.CodigoCupon).IsUnique().HasFilter("\"CodigoCupon\" IS NOT NULL");
    ///   builder.HasIndex(x => new { x.EstaActivo, x.FechaInicio, x.FechaFin });
    ///   builder.HasIndex(x => x.SegmentoCliente).HasFilter("\"SegmentoCliente\" IS NOT NULL");
    /// </remarks>
    public class Descuento : EntidadBase
    {
        /// <summary>Nombre descriptivo del descuento para la gestión interna.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del descuento y sus condiciones de aplicación.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Código del cupón de descuento que introduce el usuario en la facturación.
        /// Nulo para descuentos automáticos sin código.
        /// Ejemplo: "VERANO2026", "NUEVOCLIENTE15".
        /// </summary>
        public string? CodigoCupon { get; set; }

        /// <summary>Tipo de cálculo del descuento: porcentaje, importe fijo o precio especial.</summary>
        public TipoDescuento TipoDescuento { get; set; } = TipoDescuento.Porcentaje;

        /// <summary>
        /// Valor del descuento según el TipoDescuento:
        /// - Porcentaje: valor 0-100. Ejemplo: 10 = 10%.
        /// - ImporteFijo: importe a descontar en moneda base. Ejemplo: 5.00 €.
        /// - PrecioEspecial: precio final fijo. Ejemplo: 45.00 €.
        /// </summary>
        public decimal ValorDescuento { get; set; }

        // ── Condiciones de aplicación ────────────────────────────────────────

        /// <summary>
        /// Importe mínimo del pedido o factura para que aplique el descuento.
        /// Nulo = sin importe mínimo.
        /// </summary>
        public decimal? ImporteMinimoPedido { get; set; }

        /// <summary>
        /// Importe máximo de descuento aplicable (tope del beneficio del cupón).
        /// Solo aplica cuando TipoDescuento = Porcentaje. Nulo = sin tope.
        /// </summary>
        public decimal? ImporteMaximoDescuento { get; set; }

        /// <summary>
        /// Segmento de cliente al que aplica el descuento automáticamente.
        /// Nulo = aplica a todos los segmentos.
        /// </summary>
        public SegmentoCliente? SegmentoCliente { get; set; }

        /// <summary>
        /// FK hacia el cliente específico al que aplica el descuento.
        /// Nulo = aplica a todos los clientes del segmento indicado.
        /// </summary>
        public Guid? ClienteEspecificoId { get; set; }

        // ── Control de uso ───────────────────────────────────────────────────

        /// <summary>
        /// Indica si este descuento puede acumularse con otros descuentos en la misma factura.
        /// False = al aplicar este descuento no puede combinarse con otros cupones.
        /// </summary>
        public bool EsAcumulable { get; set; } = false;

        /// <summary>
        /// Número máximo de usos permitidos del cupón en total.
        /// Nulo = usos ilimitados.
        /// </summary>
        public int? LimiteUsos { get; set; }

        /// <summary>
        /// Número de usos realizados del cupón. Actualizado con cada aplicación.
        /// Si UsosRealizados >= LimiteUsos, el cupón se rechaza.
        /// </summary>
        public int UsosRealizados { get; set; } = 0;

        /// <summary>
        /// Límite de usos por cliente individual (evita que un mismo cliente use el cupón varias veces).
        /// Nulo = sin límite por cliente.
        /// </summary>
        public int? LimiteUsosPorCliente { get; set; }

        // ── Vigencia ─────────────────────────────────────────────────────────

        /// <summary>Fecha de inicio de validez del descuento. Nulo = válido desde su creación.</summary>
        public DateOnly? FechaInicio { get; set; }

        /// <summary>Fecha de fin de validez. Nulo = sin fecha de caducidad.</summary>
        public DateOnly? FechaFin { get; set; }

        /// <summary>Indica si el descuento está activo y puede aplicarse.</summary>
        public bool EstaActivo { get; set; } = true;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Productos específicos a los que aplica este descuento.</summary>
        public virtual ICollection<ProductoDescuento> ProductosDescuento { get; set; }
            = new List<ProductoDescuento>();

        /// <summary>Categorías específicas a cuyos productos aplica el descuento.</summary>
        public virtual ICollection<CategoriaDescuento> CategoriasDescuento { get; set; }
            = new List<CategoriaDescuento>();
    }
}
