using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Categoría de cliente que define las condiciones comerciales por defecto
    /// aplicables a todos los clientes de ese tipo: descuentos, plazos de pago,
    /// límites de crédito y prioridad de atención.
    ///
    /// SEED: Cliente Particular, Pequeña Empresa, Mediana Empresa, Gran Empresa, VIP.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(30);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.DescuentoPorDefecto).HasPrecision(5, 2);
    ///   builder.Property(x => x.LimiteCredito).HasPrecision(18, 2);
    ///   builder.Property(x => x.MontoMinimoPedido).HasPrecision(18, 2);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    /// </remarks>
    public class TipoCliente : EntidadBase
    {
        /// <summary>Código único del tipo de cliente en SCREAMING_SNAKE_CASE.</summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo del tipo de cliente para la interfaz.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción de las características y beneficios de este tipo de cliente.</summary>
        public string? Descripcion { get; set; }

        /// <summary>Porcentaje de descuento aplicado por defecto a todos los clientes de este tipo.</summary>
        public decimal DescuentoPorDefecto { get; set; } = 0m;

        /// <summary>Días de plazo de pago por defecto para facturas de este tipo de cliente.</summary>
        public int DiasPlazoPago { get; set; } = 30;

        /// <summary>
        /// Límite de crédito máximo en la moneda base para este tipo de cliente.
        /// 0 = sin crédito (pago anticipado o inmediato). Nulo = sin límite.
        /// </summary>
        public decimal? LimiteCredito { get; set; }

        /// <summary>
        /// Nivel de prioridad de atención. Menor número = mayor prioridad.
        /// Afecta a la gestión de colas de atención y asignación de empleados.
        /// </summary>
        public int Prioridad { get; set; } = 5;

        /// <summary>Importe mínimo de contratación para clientes de este tipo.</summary>
        public decimal? MontoMinimoPedido { get; set; }

        /// <summary>
        /// Color hexadecimal (#RRGGBB) para identificar visualmente el tipo en listados.
        /// </summary>
        public string? Color { get; set; }

        /// <summary>Clase CSS del icono Font Awesome para representar el tipo visualmente.</summary>
        public string? Icono { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Clientes clasificados en este tipo.</summary>
        public virtual ICollection<Cliente> Clientes { get; set; }
            = new List<Cliente>();
    }
}
