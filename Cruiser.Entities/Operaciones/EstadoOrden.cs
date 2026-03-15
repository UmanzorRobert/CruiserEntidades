using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Operaciones
{
    /// <summary>
    /// Catálogo de estados posibles para las órdenes de compra del sistema.
    /// Define el comportamiento de cada estado: si permite edición, si afecta al inventario,
    /// si requiere aprobación y la representación visual en la interfaz.
    ///
    /// Este catálogo permite configurar el flujo de estados sin modificar código,
    /// adaptando el sistema a distintos flujos de aprobación y recepción.
    ///
    /// SEED: Borrador, PendienteAprobacion, Aprobada, Enviada, Confirmada,
    ///       RecepcionParcial, Completada, Anulada.
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
    public class EstadoOrden : EntidadBase
    {
        /// <summary>
        /// Código único del estado en SCREAMING_SNAKE_CASE.
        /// Ejemplo: "BORRADOR", "PENDIENTE_APROBACION", "COMPLETADA".
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo del estado para mostrar en la interfaz.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del estado y las acciones que habilita o bloquea.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Indica si este es el estado inicial al crear una nueva orden de compra.
        /// Solo puede haber un estado inicial activo.
        /// </summary>
        public bool EsInicial { get; set; } = false;

        /// <summary>
        /// Indica si este es un estado final (Completada, Anulada).
        /// Los estados finales no permiten transiciones a otros estados.
        /// </summary>
        public bool EsFinal { get; set; } = false;

        /// <summary>
        /// Indica si la orden de compra puede ser editada cuando está en este estado.
        /// False para estados Enviada, Confirmada, Completada y Anulada.
        /// </summary>
        public bool PermiteEdicion { get; set; } = true;

        /// <summary>
        /// Indica si las recepciones en este estado generan MovimientosInventario de entrada.
        /// True solo para estados RecepcionParcial y Completada.
        /// </summary>
        public bool AfectaInventario { get; set; } = false;

        /// <summary>
        /// Indica si la transición a este estado requiere una AprobacionDocumento aprobada.
        /// True para el estado Aprobada (requiere aprobación del nivel jerárquico).
        /// </summary>
        public bool RequiereAprobacion { get; set; } = false;

        /// <summary>
        /// Color hexadecimal (#RRGGBB) del badge de estado en la interfaz.
        /// Ejemplo: "#6C757D" (gris/Borrador), "#0D6EFD" (azul/Enviada), "#198754" (verde/Completada).
        /// </summary>
        public string? Color { get; set; }

        /// <summary>Clase CSS del icono Font Awesome para el estado.</summary>
        public string? Icono { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Órdenes de compra en este estado.</summary>
        public virtual ICollection<OrdenCompra> OrdenesCompra { get; set; }
            = new List<OrdenCompra>();
    }
}
