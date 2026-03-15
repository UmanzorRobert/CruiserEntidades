using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Facturacion
{
    /// <summary>
    /// Catálogo de estados de factura que define el comportamiento de cada estado
    /// en el módulo de facturación: si permite edición, si afecta al saldo del cliente,
    /// si requiere seguimiento de cobranza y si genera alertas.
    ///
    /// SEED: Borrador, Emitida, Enviada, ParcialmentePagada, Pagada,
    ///       Vencida, EnGestionCobranza, Anulada.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(50);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.Color).HasMaxLength(7);
    ///   builder.Property(x => x.Icono).HasMaxLength(100);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    ///   builder.HasIndex(x => x.EsInicial).HasFilter("\"EsInicial\" = true");
    ///   builder.HasIndex(x => x.EsFinal);
    /// </remarks>
    public class EstadoFactura : EntidadBase
    {
        /// <summary>Código único del estado en SCREAMING_SNAKE_CASE.</summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo del estado para la interfaz.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del estado y las acciones que habilita.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Indica si este es el estado inicial al crear una nueva factura (Borrador).
        /// </summary>
        public bool EsInicial { get; set; } = false;

        /// <summary>Indica si este es un estado final (Pagada, Anulada). Sin transiciones posibles.</summary>
        public bool EsFinal { get; set; } = false;

        /// <summary>
        /// Indica si la factura puede ser editada en este estado.
        /// False para todos los estados tras la emisión (norma VeriFactu).
        /// </summary>
        public bool PermiteEdicion { get; set; } = true;

        /// <summary>
        /// Indica si este estado afecta al saldo pendiente del cliente en CuentaCobrar.
        /// True para: Emitida, Enviada, ParcialmentePagada, Vencida, EnGestionCobranza.
        /// </summary>
        public bool AfectaSaldoCliente { get; set; } = false;

        /// <summary>
        /// Indica si las facturas en este estado deben aparecer en la gestión de cobranza.
        /// True para: Vencida, EnGestionCobranza, ParcialmentePagada vencida.
        /// </summary>
        public bool RequiereSeguimiento { get; set; } = false;

        /// <summary>
        /// Indica si este estado genera alertas automáticas en el panel de notificaciones.
        /// True para: Vencida (alerta de mora), EnGestionCobranza (escalar).
        /// </summary>
        public bool GeneraAlertas { get; set; } = false;

        /// <summary>Color hexadecimal del badge de estado en la interfaz.</summary>
        public string? Color { get; set; }

        /// <summary>Clase CSS del icono Font Awesome para el estado.</summary>
        public string? Icono { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Facturas en este estado.</summary>
        public virtual ICollection<Factura> Facturas { get; set; }
            = new List<Factura>();
    }
}
