using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.RRHH
{
    /// <summary>
    /// Registro de entrega y devolución de Equipos de Protección Individual (EPI)
    /// a un empleado. Cumple con la obligación legal del Art. 17 LPRL de documentar
    /// la entrega de EPIs al trabajador.
    ///
    /// Cada entrega genera un registro individual con el tipo de EPI, la talla,
    /// el coste unitario y el estado actual. Cuando el empleado devuelve el EPI,
    /// se actualiza el estado y la FechaDevolucion.
    ///
    /// El informe de EPIs por empleado se usa para calcular el coste total de
    /// equipación por empleado y para verificar que todos tienen sus EPIs al día.
    ///
    /// SEED: Guantes nitrilo, Mascarilla FFP2, Gafas protección, Botas seguridad,
    ///       Uniforme, Chaleco reflectante.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.TipoEPI).HasMaxLength(100);
    ///   builder.Property(x => x.Talla).HasMaxLength(20);
    ///   builder.Property(x => x.CostoUnitario).HasPrecision(10, 2);
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.Estado });
    ///   builder.HasIndex(x => x.FechaEntrega);
    /// </remarks>
    public class EPIEmpleado : EntidadBase
    {
        /// <summary>FK hacia el empleado al que se entrega el EPI.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>FK hacia el empleado de RRHH que realizó la entrega del EPI.</summary>
        public Guid EntregadoPorId { get; set; }

        /// <summary>
        /// Nombre o código del tipo de EPI entregado.
        /// Ejemplo: "Guantes nitrilo talla M", "Mascarilla FFP2", "Botas seguridad S3 Nº43".
        /// </summary>
        public string TipoEPI { get; set; } = string.Empty;

        /// <summary>Descripción detallada del EPI: marca, modelo, referencia del fabricante.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Talla o tamaño del EPI cuando aplica.
        /// Ejemplo: "M", "L", "XL", "43" (para calzado), "1.80m" (para mono de trabajo).
        /// </summary>
        public string? Talla { get; set; }

        /// <summary>Fecha de entrega del EPI al empleado.</summary>
        public DateOnly FechaEntrega { get; set; }

        /// <summary>Fecha de devolución del EPI. Nulo si sigue en posesión del empleado.</summary>
        public DateOnly? FechaDevolucion { get; set; }

        /// <summary>
        /// Fecha de caducidad del EPI cuando aplica (mascarillas, guantes con fecha de uso máximo).
        /// Genera AlertaVencimiento cuando se acerca la fecha.
        /// </summary>
        public DateOnly? FechaCaducidad { get; set; }

        /// <summary>Estado actual del EPI.</summary>
        public EstadoEPI Estado { get; set; } = EstadoEPI.Entregado;

        /// <summary>
        /// Coste unitario de adquisición del EPI.
        /// Permite calcular el coste total de equipación por empleado.
        /// </summary>
        public decimal CostoUnitario { get; set; } = 0m;

        /// <summary>
        /// Indica si el empleado firmó el acuse de recibo de entrega del EPI.
        /// La firma puede ser digital (desde la PWA) o en papel.
        /// </summary>
        public bool FirmadoPorEmpleado { get; set; } = false;

        /// <summary>Notas sobre el estado del EPI o condiciones de devolución.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Empleado receptor del EPI.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }
    }
}
