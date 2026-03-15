using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Protocolo técnico de ejecución de un tipo de servicio de limpieza.
    /// Define los pasos ordenados, los productos recomendados, el tiempo estimado
    /// y el nivel de dificultad técnica requerida.
    ///
    /// PasosJSON almacena el listado ordenado de pasos como un array JSONB:
    /// [
    ///   { "orden": 1, "descripcion": "Ventilar el área", "duracionMinutos": 5, "esObligatorio": true },
    ///   { "orden": 2, "descripcion": "Aplicar desengrasante", "duracionMinutos": 10, "esObligatorio": true }
    /// ]
    ///
    /// ProductosRecomendadosJSON almacena los productos del catálogo recomendados:
    /// [{ "productoId": "...", "nombre": "...", "cantidadEstimada": 0.5, "unidad": "litros" }]
    ///
    /// Cada tipo de servicio puede tener múltiples versiones del protocolo;
    /// solo el más reciente con EsActivo=true se usa en nuevas órdenes.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.PasosJSON).HasColumnType("jsonb");
    ///   builder.Property(x => x.ProductosRecomendadosJSON).HasColumnType("jsonb");
    ///   builder.HasIndex(x => new { x.TipoServicioId, x.EsActivo }).HasFilter("\"EsActivo\" = true");
    ///   builder.HasIndex(x => x.PasosJSON).HasMethod("gin");
    ///   builder.HasIndex(x => x.ProductosRecomendadosJSON).HasMethod("gin");
    /// </remarks>
    public class ProtocoloServicio : EntidadBase
    {
        /// <summary>FK hacia el tipo de servicio al que pertenece este protocolo.</summary>
        public Guid TipoServicioId { get; set; }

        /// <summary>Nombre descriptivo de la versión del protocolo.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción general del protocolo y sus objetivos de calidad.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Pasos técnicos ordenados del protocolo en formato JSONB.
        /// Array de objetos con: orden, descripcion, duracionMinutos, esObligatorio, notas.
        /// </summary>
        public string? PasosJSON { get; set; }

        /// <summary>
        /// Productos del catálogo recomendados para ejecutar el protocolo en formato JSONB.
        /// Array de objetos con: productoId, nombre, cantidadEstimada, unidad.
        /// </summary>
        public string? ProductosRecomendadosJSON { get; set; }

        /// <summary>Tiempo total estimado en minutos para ejecutar el protocolo completo.</summary>
        public int TiempoEstimadoMinutos { get; set; } = 0;

        /// <summary>Nivel de dificultad técnica requerido del empleado para ejecutar el protocolo.</summary>
        public NivelDificultad NivelDificultad { get; set; } = NivelDificultad.Basico;

        /// <summary>
        /// Indica si este protocolo requiere que haya un supervisor in situ
        /// durante toda la ejecución del servicio.
        /// </summary>
        public bool RequiereSupervisor { get; set; } = false;

        /// <summary>Número de versión del protocolo. Incrementado al crear una nueva versión.</summary>
        public int Version { get; set; } = 1;

        /// <summary>
        /// Indica si este es el protocolo activo y vigente para el tipo de servicio.
        /// Solo puede haber un protocolo activo por TipoServicio.
        /// Al activar una nueva versión, la anterior se desactiva.
        /// </summary>
        public bool EsActivo { get; set; } = true;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Tipo de servicio al que pertenece este protocolo.</summary>
        public virtual TipoServicio? TipoServicio { get; set; }
    }
}
