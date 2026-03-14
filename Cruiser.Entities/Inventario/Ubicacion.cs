using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Inventario
{
    /// <summary>
    /// Ubicación física específica dentro de un almacén que identifica con precisión
    /// dónde está almacenado cada lote de producto: zona, pasillo, estantería, nivel y posición.
    ///
    /// El sistema de coordenadas alfanuméricas (Zona A, Pasillo 01, Estantería B, Nivel 3, Pos 04)
    /// permite localizar cualquier producto en el almacén de forma inequívoca.
    ///
    /// La ClasificacionABC determina la frecuencia de conteo en inventarios cíclicos:
    /// los productos A se ubican en posiciones de acceso más rápido.
    ///
    /// El CodigoBarras de la ubicación se usa en la PWA para confirmar la posición
    /// correcta al realizar movimientos de almacén con escáner.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Zona).HasMaxLength(20);
    ///   builder.Property(x => x.Pasillo).HasMaxLength(10);
    ///   builder.Property(x => x.Estanteria).HasMaxLength(10);
    ///   builder.Property(x => x.Nivel).HasMaxLength(10);
    ///   builder.Property(x => x.Posicion).HasMaxLength(10);
    ///   builder.Property(x => x.CodigoBarras).HasMaxLength(100);
    ///   builder.Property(x => x.CapacidadMaxKg).HasPrecision(10, 2);
    ///   builder.Property(x => x.CapacidadMaxM3).HasPrecision(10, 3);
    ///   builder.HasIndex(x => new { x.AlmacenId, x.CodigoUbicacion }).IsUnique();
    ///   builder.HasIndex(x => x.CodigoBarras).HasFilter("\"CodigoBarras\" IS NOT NULL");
    /// </remarks>
    public class Ubicacion : EntidadBase
    {
        /// <summary>
        /// Identificador del almacén al que pertenece esta ubicación.
        /// FK hacia Almacenes.
        /// </summary>
        public Guid AlmacenId { get; set; }

        /// <summary>
        /// Código de zona dentro del almacén (área funcional o sección).
        /// Ejemplo: "A", "B", "QUIMICOS", "FRIO", "CUARENTENA".
        /// </summary>
        public string? Zona { get; set; }

        /// <summary>Identificador del pasillo dentro de la zona. Ejemplo: "01", "02", "A".</summary>
        public string? Pasillo { get; set; }

        /// <summary>Letra o código de la estantería dentro del pasillo. Ejemplo: "A", "B", "C".</summary>
        public string? Estanteria { get; set; }

        /// <summary>Número de nivel o altura de la estantería. Ejemplo: "1", "2", "3", "SUELO".</summary>
        public string? Nivel { get; set; }

        /// <summary>Posición horizontal dentro del nivel. Ejemplo: "01", "02", "03".</summary>
        public string? Posicion { get; set; }

        /// <summary>
        /// Código completo y único de la ubicación generado concatenando los campos anteriores.
        /// Generado automáticamente. Ejemplo: "A-01-B-3-04".
        /// Es el código que se imprime en las etiquetas de la ubicación física.
        /// </summary>
        public string CodigoUbicacion { get; set; } = string.Empty;

        /// <summary>
        /// Código de barras de la etiqueta física en la estantería.
        /// Escaneado con la PWA para confirmar que el empleado está en la ubicación correcta
        /// al realizar movimientos de stock o conteos de inventario.
        /// </summary>
        public string? CodigoBarras { get; set; }

        // ── Capacidad física ─────────────────────────────────────────────────

        /// <summary>
        /// Capacidad máxima de carga en kilogramos que soporta la ubicación.
        /// Usado para validar que no se supere el límite estructural de la estantería.
        /// </summary>
        public decimal? CapacidadMaxKg { get; set; }

        /// <summary>
        /// Capacidad máxima en metros cúbicos disponibles en la ubicación.
        /// </summary>
        public decimal? CapacidadMaxM3 { get; set; }

        // ── Clasificación y estado ───────────────────────────────────────────

        /// <summary>
        /// Clasificación ABC de la ubicación según el tipo de productos que alberga.
        /// Las ubicaciones A están en las posiciones de acceso más ergonómico y rápido.
        /// </summary>
        public ClasificacionABC ClasificacionABC { get; set; } = ClasificacionABC.C;

        /// <summary>
        /// Indica si la ubicación está bloqueada para operaciones normales.
        /// True para ubicaciones en cuarentena, mantenimiento o reservadas.
        /// </summary>
        public bool EstaBloqueada { get; set; } = false;

        /// <summary>Motivo del bloqueo si EstaBloqueada=true.</summary>
        public string? MotivoBloqueado { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Almacén al que pertenece esta ubicación.</summary>
        public virtual Almacen? Almacen { get; set; }

        /// <summary>Lotes de productos almacenados actualmente en esta ubicación.</summary>
        public virtual ICollection<UbicacionLote> LotesEnUbicacion { get; set; }
            = new List<UbicacionLote>();
    }
}
