using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Catalogo
{
    /// <summary>
    /// Ficha de datos de seguridad (SDS/FDS) de un producto peligroso según el
    /// Reglamento REACH (CE 1907/2006) y el Reglamento CLP (CE 1272/2008).
    ///
    /// Obligatoria para todos los productos con EsPeligroso=true.
    /// Relación 1-a-1 con Producto (solo los productos peligrosos tienen ficha).
    ///
    /// Los campos PictogramasRiesgo y EppsRequeridos se almacenan en JSONB
    /// para flexibilidad en los pictogramas GHS/CLP que apliquen a cada producto.
    ///
    /// Los empleados en campo pueden acceder a la ficha de seguridad desde la PWA
    /// antes de usar el producto, cumpliendo con la normativa de PRL.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.NumeroVersion).IsRequired().HasMaxLength(20);
    ///   builder.Property(x => x.NombreQuimico).HasMaxLength(300);
    ///   builder.Property(x => x.ClasificacionPeligro).HasMaxLength(500);
    ///   builder.Property(x => x.PictogramasRiesgo).HasColumnType("jsonb");
    ///   builder.Property(x => x.EppsRequeridos).HasColumnType("jsonb");
    ///   builder.Property(x => x.RutaDocumentoPDF).HasMaxLength(500);
    ///   builder.HasIndex(x => x.ProductoId).IsUnique();
    ///   builder.HasIndex(x => x.FechaVencimiento).HasFilter("\"FechaVencimiento\" IS NOT NULL");
    ///   builder.HasIndex(x => x.PictogramasRiesgo).HasMethod("gin");
    ///
    ///   Relación:
    ///   builder.HasOne(fs => fs.Producto).WithOne(p => p.FichaSeguridad)
    ///          .HasForeignKey&lt;FichaSeguridad&gt;(fs => fs.ProductoId);
    ///
    ///   Estructura JSON de PictogramasRiesgo (código GHS):
    ///   ["GHS01", "GHS05", "GHS07"]
    ///   (GHS01=Explosivo, GHS05=Corrosivo, GHS07=Nocivo, GHS08=Peligro salud, etc.)
    ///
    ///   Estructura JSON de EppsRequeridos:
    ///   ["guantes_nitrilo", "gafas_proteccion", "mascarilla_ffp2", "bata_quimica"]
    /// </remarks>
    public class FichaSeguridad : EntidadBase
    {
        /// <summary>
        /// Identificador del producto peligroso al que aplica esta ficha de seguridad.
        /// FK hacia Productos. Índice único (una ficha por producto).
        /// </summary>
        public Guid ProductoId { get; set; }

        /// <summary>
        /// Número de versión de la ficha de seguridad.
        /// El fabricante debe emitir una nueva versión cuando cambia la formulación.
        /// Ejemplo: "1.0", "2.3", "Rev.4".
        /// </summary>
        public string NumeroVersion { get; set; } = string.Empty;

        /// <summary>
        /// Nombre químico IUPAC o denominación técnica del producto.
        /// Ejemplo: "Hipoclorito de sodio en solución acuosa al 5%".
        /// </summary>
        public string? NombreQuimico { get; set; }

        /// <summary>
        /// Números de registro CAS (Chemical Abstracts Service) de los componentes.
        /// Pueden ser múltiples separados por punto y coma.
        /// Ejemplo: "7681-52-9; 1310-73-2" (lejía comercial).
        /// </summary>
        public string? NumerosCAS { get; set; }

        /// <summary>
        /// Clasificación de peligro según Reglamento CLP (GHS) en formato texto.
        /// Ejemplo: "Corrosivo Cat. 1B (H314), Irritante ocular Cat. 2A (H319)".
        /// </summary>
        public string? ClasificacionPeligro { get; set; }

        /// <summary>
        /// Array JSON de códigos de pictogramas de peligro GHS que aplican al producto.
        /// Los códigos siguen el estándar internacional GHS: GHS01 a GHS09.
        /// Almacenado en JSONB con índice GIN para filtrar productos por tipo de peligro.
        /// Ejemplo: ["GHS05", "GHS07"] (corrosivo + nocivo).
        /// </summary>
        public string? PictogramasRiesgo { get; set; }

        /// <summary>
        /// Array JSON de EPPs (Equipos de Protección Personal) requeridos al usar el producto.
        /// Se muestra en la PWA al empleado antes de iniciar el servicio con este producto.
        /// Los códigos coinciden con los tipos de EPI del catálogo de EPIEmpleado.
        /// Ejemplo: ["guantes_nitrilo", "gafas_proteccion", "mascarilla_ffp2"].
        /// </summary>
        public string? EppsRequeridos { get; set; }

        /// <summary>
        /// Instrucciones de primeros auxilios en caso de contacto, inhalación o ingestión.
        /// Se muestra de forma destacada en la ficha de seguridad en la PWA.
        /// </summary>
        public string? PrimerosAuxilios { get; set; }

        /// <summary>
        /// Instrucciones de almacenamiento seguro del producto.
        /// Ejemplo: "Almacenar en lugar fresco y seco. Alejado de fuentes de calor."
        /// </summary>
        public string? InstruccionesAlmacenamiento { get; set; }

        /// <summary>
        /// Instrucciones de eliminación o gestión de residuos del producto.
        /// Debe cumplir con la normativa de residuos peligrosos aplicable.
        /// </summary>
        public string? InstruccionesEliminacion { get; set; }

        /// <summary>
        /// Ruta relativa del archivo PDF oficial de la ficha de seguridad completa.
        /// Descargable desde la interfaz de administración y desde la PWA en campo.
        /// </summary>
        public string? RutaDocumentoPDF { get; set; }

        /// <summary>
        /// Fecha de emisión de esta versión de la ficha de seguridad por el fabricante.
        /// </summary>
        public DateOnly? FechaEmision { get; set; }

        /// <summary>
        /// Fecha de vencimiento de la ficha de seguridad.
        /// Las fichas de seguridad deben revisarse cada 5 años o ante cambios en la formulación.
        /// Genera una alerta de vencimiento en AlertaVencimiento cuando se acerca.
        /// </summary>
        public DateOnly? FechaVencimiento { get; set; }

        /// <summary>
        /// Indica si esta ficha de seguridad está vigente y puede ser usada.
        /// Se establece automáticamente en false cuando se supera FechaVencimiento.
        /// Los productos con ficha vencida generan una alerta urgente.
        /// </summary>
        public bool EsVigente { get; set; } = true;

        /// <summary>
        /// Nombre del fabricante o proveedor que emitió la ficha de seguridad.
        /// Ejemplo: "Química Industrial S.A.", "BASF España S.L.".
        /// </summary>
        public string? FabricanteEmitente { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Producto peligroso al que corresponde esta ficha de seguridad.</summary>
        public virtual Producto? Producto { get; set; }
    }
}
