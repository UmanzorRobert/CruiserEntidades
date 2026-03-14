using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Proveedor de productos o servicios para la empresa de limpieza.
    /// Almacena datos fiscales, múltiples emails de contacto, datos bancarios
    /// para transferencias, puntuaciones de evaluación y estadísticas de compras.
    ///
    /// Un proveedor puede ser bloqueado (EstaBloqueado=true) para impedir
    /// la creación de nuevas órdenes de compra hacia él mientras se resuelve
    /// una incidencia de calidad o un problema de pago.
    ///
    /// El proceso de homologación (EsHomologado, FechaHomologacion) garantiza
    /// que solo proveedores certificados pueden suministrar productos críticos.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.RazonSocial).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.CIF).HasMaxLength(20);
    ///   builder.Property(x => x.EmailPrincipal).HasMaxLength(256);
    ///   builder.Property(x => x.IBAN).HasMaxLength(34);
    ///   builder.Property(x => x.BIC).HasMaxLength(11);
    ///   builder.Property(x => x.TotalCompras).HasPrecision(18, 2);
    ///   builder.Property(x => x.PuntuacionMedia).HasPrecision(3, 2);
    ///   builder.HasIndex(x => x.CIF).HasFilter("\"CIF\" IS NOT NULL");
    ///   builder.HasIndex(x => x.EmailPrincipal).HasFilter("\"EmailPrincipal\" IS NOT NULL");
    ///   builder.HasIndex(x => x.EstaBloqueado);
    ///   builder.HasIndex(x => x.EsHomologado);
    /// </remarks>
    public class Proveedor : EntidadBase
    {
        // ── Identificación fiscal ────────────────────────────────────────────

        /// <summary>
        /// Razón social o nombre completo del proveedor.
        /// Usado en documentos legales, órdenes de compra y facturas.
        /// </summary>
        public string RazonSocial { get; set; } = string.Empty;

        /// <summary>Nombre comercial del proveedor si difiere de la razón social.</summary>
        public string? NombreComercial { get; set; }

        /// <summary>CIF, NIF o número de identificación fiscal del proveedor.</summary>
        public string? CIF { get; set; }

        /// <summary>
        /// Número de IVA intracomunitario del proveedor (para proveedores UE).
        /// Formato: código país ISO2 + número. Ejemplo: "ES12345678A", "FR12345678901".
        /// </summary>
        public string? NumeroIVAIntracomunitario { get; set; }

        // ── Contacto ─────────────────────────────────────────────────────────

        /// <summary>Email principal para envío de órdenes de compra y comunicaciones.</summary>
        public string? EmailPrincipal { get; set; }

        /// <summary>Email del departamento de facturación del proveedor.</summary>
        public string? EmailFacturacion { get; set; }

        /// <summary>Email del departamento comercial o representante de ventas.</summary>
        public string? EmailComercial { get; set; }

        /// <summary>Teléfono principal del proveedor.</summary>
        public string? Telefono { get; set; }

        /// <summary>Teléfono del representante comercial asignado a la cuenta.</summary>
        public string? TelefonoComercial { get; set; }

        /// <summary>Nombre del contacto principal en el proveedor.</summary>
        public string? NombreContactoPrincipal { get; set; }

        /// <summary>Sitio web del proveedor.</summary>
        public string? SitioWeb { get; set; }

        // ── Datos bancarios ──────────────────────────────────────────────────

        /// <summary>
        /// IBAN de la cuenta bancaria del proveedor para transferencias de pago.
        /// Validado con módulo 97 (FluentValidation) al guardar.
        /// </summary>
        public string? IBAN { get; set; }

        /// <summary>Código BIC/SWIFT del banco del proveedor.</summary>
        public string? BIC { get; set; }

        /// <summary>Nombre del titular de la cuenta bancaria (puede diferir de RazonSocial).</summary>
        public string? TitularCuentaBancaria { get; set; }

        // ── Condiciones comerciales ──────────────────────────────────────────

        /// <summary>Días de plazo de pago pactado con el proveedor.</summary>
        public int DiasPlazoPago { get; set; } = 30;

        /// <summary>Porcentaje de descuento general pactado con el proveedor.</summary>
        public decimal? PorcentajeDescuentoGeneral { get; set; }

        /// <summary>Importe mínimo de pedido aceptado por el proveedor.</summary>
        public decimal? MontoMinimoPedido { get; set; }

        /// <summary>
        /// Moneda de facturación del proveedor.
        /// FK hacia Moneda. Nulo = moneda base del sistema (EUR).
        /// </summary>
        public Guid? MonedaId { get; set; }

        // ── Homologación y evaluación ────────────────────────────────────────

        /// <summary>
        /// Indica si el proveedor ha superado el proceso de homologación
        /// y está certificado para suministrar productos o servicios a la empresa.
        /// </summary>
        public bool EsHomologado { get; set; } = false;

        /// <summary>Fecha en que el proveedor completó satisfactoriamente el proceso de homologación.</summary>
        public DateOnly? FechaHomologacion { get; set; }

        /// <summary>Fecha de vencimiento de la homologación. Genera alerta en AlertaVencimiento.</summary>
        public DateOnly? FechaVencimientoHomologacion { get; set; }

        /// <summary>
        /// Puntuación media de las evaluaciones recibidas (0.00 a 5.00).
        /// Actualizada automáticamente al guardar una nueva EvaluacionProveedor.
        /// </summary>
        public decimal PuntuacionMedia { get; set; } = 0m;

        /// <summary>Número total de evaluaciones realizadas al proveedor.</summary>
        public int NumeroEvaluaciones { get; set; } = 0;

        // ── Estadísticas de compras ──────────────────────────────────────────

        /// <summary>Total acumulado de compras al proveedor en la moneda base del sistema.</summary>
        public decimal TotalCompras { get; set; } = 0m;

        /// <summary>Número total de órdenes de compra emitidas a este proveedor.</summary>
        public int NumeroOrdenesCompra { get; set; } = 0;

        /// <summary>Fecha de la última orden de compra emitida a este proveedor.</summary>
        public DateTime? FechaUltimaCompra { get; set; }

        // ── Estado ───────────────────────────────────────────────────────────

        /// <summary>
        /// Indica si el proveedor está bloqueado para nuevas órdenes de compra.
        /// Se bloquea por incidencias de calidad, impagos o problemas contractuales.
        /// </summary>
        public bool EstaBloqueado { get; set; } = false;

        /// <summary>Motivo del bloqueo cuando EstaBloqueado=true.</summary>
        public string? MotivoBloqueado { get; set; }

        /// <summary>Notas internas sobre el proveedor. No visibles en documentos generados.</summary>
        public string? NotasInternas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Evaluaciones de calidad realizadas a este proveedor.</summary>
        public virtual ICollection<EvaluacionProveedor> Evaluaciones { get; set; }
            = new List<EvaluacionProveedor>();

        /// <summary>Productos suministrados por este proveedor con sus condiciones específicas.</summary>
        public virtual ICollection<ProveedorProducto> ProductosProveedores { get; set; }
            = new List<ProveedorProducto>();

        /// <summary>Contactos adicionales registrados para este proveedor.</summary>
        public virtual ICollection<ContactoAdicional> ContactosAdicionales { get; set; }
            = new List<ContactoAdicional>();
    }
}
