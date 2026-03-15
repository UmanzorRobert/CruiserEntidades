using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Precios
{
    /// <summary>
    /// Lista de precios especiales aplicable a un segmento de clientes,
    /// a un cliente específico o de forma general a todos los clientes.
    ///
    /// El sistema aplica la lista de mayor prioridad (menor número en Prioridad)
    /// que sea aplicable al cliente en el momento de generar una cotización o factura.
    /// Las listas específicas por cliente (TipoListaPrecio = PorCliente) tienen
    /// siempre prioridad sobre las listas generales o por segmento.
    ///
    /// SEED: Lista General (tipo General, prioridad 99, siempre activa).
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(30);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    ///   builder.HasIndex(x => new { x.Tipo, x.EstaActiva, x.Prioridad });
    ///   builder.HasIndex(x => new { x.ClienteId, x.EstaActiva }).HasFilter("\"ClienteId\" IS NOT NULL");
    ///   builder.HasIndex(x => new { x.FechaInicio, x.FechaFin });
    /// </remarks>
    public class ListaPrecio : EntidadBase
    {
        /// <summary>
        /// Código único de la lista de precios.
        /// Ejemplo: "GENERAL-2026", "VIP-2026", "CLIENTE-001-ESP".
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo de la lista para la interfaz de administración.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción de la lista y el segmento de cliente al que aplica.</summary>
        public string? Descripcion { get; set; }

        /// <summary>Tipo de lista según el segmento de aplicación.</summary>
        public TipoListaPrecio Tipo { get; set; } = TipoListaPrecio.General;

        /// <summary>
        /// Segmento de cliente al que aplica esta lista.
        /// Solo aplica cuando Tipo = PorSegmento.
        /// Nulo para listas generales o por cliente específico.
        /// </summary>
        public SegmentoCliente? SegmentoObjetivo { get; set; }

        /// <summary>
        /// FK hacia el cliente específico al que aplica esta lista personalizada.
        /// Solo aplica cuando Tipo = PorCliente.
        /// </summary>
        public Guid? ClienteId { get; set; }

        /// <summary>
        /// Número de prioridad de aplicación. Menor número = mayor prioridad.
        /// La lista de mayor prioridad que aplique al cliente se usa en la facturación.
        /// Lista por cliente: prioridad 1. Lista por segmento: 10. Lista general: 99.
        /// </summary>
        public int Prioridad { get; set; } = 99;

        /// <summary>FK hacia la moneda en que están expresados los precios de esta lista.</summary>
        public Guid? MonedaId { get; set; }

        /// <summary>
        /// Indica si los precios de la lista incluyen IVA (precio final al cliente)
        /// o son precios sin IVA (base imponible).
        /// </summary>
        public bool IncluyeImpuestos { get; set; } = false;

        /// <summary>Fecha de inicio de validez de la lista. Nulo = válida desde siempre.</summary>
        public DateOnly? FechaInicio { get; set; }

        /// <summary>Fecha de fin de validez. Nulo = sin fecha de vencimiento.</summary>
        public DateOnly? FechaFin { get; set; }

        /// <summary>Indica si la lista está activa y aplicable en la facturación.</summary>
        public bool EstaActiva { get; set; } = true;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Precios específicos de productos en esta lista.</summary>
        public virtual ICollection<DetalleListaPrecio> Detalles { get; set; }
            = new List<DetalleListaPrecio>();
    }
}
