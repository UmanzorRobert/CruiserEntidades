using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Inventario
{
    /// <summary>
    /// Almacén o bodega física donde se gestiona el stock de productos.
    /// El sistema soporta múltiples almacenes (sede principal, almacén externo,
    /// vehículo de empleado, etc.) con control de temperatura opcional.
    ///
    /// Cada almacén tiene un responsable asignado y puede tener múltiples
    /// ubicaciones internas (zonas, pasillos, estanterías, niveles, posiciones).
    ///
    /// El AlmacenPrincipal (EsPrincipal=true) es el almacén por defecto
    /// para recepciones de compras y asignaciones sin almacén especificado.
    ///
    /// SEED: Un almacén principal creado al inicializar el sistema.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(20);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150);
    ///   builder.Property(x => x.CapacidadTotalM3).HasPrecision(10, 2);
    ///   builder.Property(x => x.TemperaturaMinGrados).HasPrecision(5, 1);
    ///   builder.Property(x => x.TemperaturaMaxGrados).HasPrecision(5, 1);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    ///   builder.HasIndex(x => x.EsPrincipal).HasFilter("\"EsPrincipal\" = true");
    /// </remarks>
    public class Almacen : EntidadBase
    {
        /// <summary>
        /// Código único del almacén en el sistema.
        /// Ejemplo: "ALM-01", "ALM-SEV", "VEH-FURG-01".
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo del almacén para mostrar en la interfaz.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del tipo de productos que alberga y su uso.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Tipo o categoría del almacén.
        /// Ejemplo: "Almacén Central", "Almacén Externo", "Vehículo", "Taquilla Empleado".
        /// </summary>
        public string? Tipo { get; set; }

        /// <summary>
        /// Indica si este es el almacén principal del sistema.
        /// Solo puede haber uno activo. Es el almacén por defecto en todas las operaciones.
        /// </summary>
        public bool EsPrincipal { get; set; } = false;

        /// <summary>
        /// Identificador de la dirección física del almacén.
        /// FK hacia DireccionCompleta. Nulo para almacenes virtuales o vehículos.
        /// </summary>
        public Guid? DireccionId { get; set; }

        // ── Capacidad y condiciones de almacenamiento ────────────────────────

        /// <summary>
        /// Capacidad total del almacén en metros cúbicos (m³).
        /// Usado para calcular el porcentaje de ocupación mostrado en el semáforo de la UI.
        /// </summary>
        public decimal? CapacidadTotalM3 { get; set; }

        /// <summary>
        /// Indica si el almacén tiene sistema de control y monitoreo de temperatura.
        /// Requerido para productos que necesiten refrigeración (Producto.RequiereRefrigeracion=true).
        /// </summary>
        public bool TieneControlTemperatura { get; set; } = false;

        /// <summary>
        /// Temperatura mínima en grados Celsius del almacén con control de temperatura.
        /// Nulo si no tiene control de temperatura.
        /// </summary>
        public decimal? TemperaturaMinGrados { get; set; }

        /// <summary>
        /// Temperatura máxima en grados Celsius del almacén con control de temperatura.
        /// </summary>
        public decimal? TemperaturaMaxGrados { get; set; }

        // ── Horario de operación ─────────────────────────────────────────────

        /// <summary>
        /// Hora de apertura del almacén en formato HH:mm.
        /// Usada para verificar disponibilidad en la planificación de rutas y recepciones.
        /// </summary>
        public string? HoraApertura { get; set; }

        /// <summary>Hora de cierre del almacén en formato HH:mm.</summary>
        public string? HoraCierre { get; set; }

        /// <summary>
        /// Días de operación del almacén como cadena de 7 bits (LunMarMiéJueVieSáb Dom).
        /// Ejemplo: "1111100" = Lunes a Viernes. "1111111" = todos los días.
        /// </summary>
        public string? DiasOperacion { get; set; }

        // ── Responsable ──────────────────────────────────────────────────────

        /// <summary>
        /// Identificador del empleado responsable del almacén.
        /// FK hacia Empleado. Recibe alertas de stock mínimo y diferencias de inventario.
        /// </summary>
        public Guid? ResponsableId { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Ubicaciones físicas internas definidas en este almacén.</summary>
        public virtual ICollection<Ubicacion> Ubicaciones { get; set; }
            = new List<Ubicacion>();
    }
}
