using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Alerta generada automáticamente por Hangfire cuando una entidad del sistema
    /// tiene una fecha de vencimiento próxima o ya vencida.
    ///
    /// Aplica a: ITV de vehículos, seguros de vehículos, formaciones de empleados,
    /// contratos de servicio, documentos de proveedores, EPIs, fichas de seguridad.
    ///
    /// El nivel de alerta (semáforo verde/amarillo/naranja/rojo) se calcula
    /// automáticamente según los días restantes hasta el vencimiento.
    /// Las alertas gestionadas (EstaGestionada=true) desaparecen del dashboard.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.TipoEntidad).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.EntidadId).IsRequired().HasMaxLength(36);
    ///   builder.Property(x => x.DescripcionEntidad).HasMaxLength(300);
    ///   builder.HasIndex(x => new { x.TipoEntidad, x.EntidadId });
    ///   builder.HasIndex(x => new { x.NivelAlerta, x.EstaGestionada });
    ///   builder.HasIndex(x => x.FechaVencimiento);
    /// </remarks>
    public class AlertaVencimiento : EntidadBase
    {
        /// <summary>
        /// Tipo de entidad que está próxima a vencer.
        /// Ejemplos: "VehiculoEmpresa_ITV", "VehiculoEmpresa_Seguro",
        /// "FormacionEmpleado", "ContratoServicio", "FichaSeguridad".
        /// </summary>
        public string TipoEntidad { get; set; } = string.Empty;

        /// <summary>
        /// Identificador GUID de la entidad específica que está por vencer.
        /// </summary>
        public string EntidadId { get; set; } = string.Empty;

        /// <summary>
        /// Descripción legible de la entidad para mostrar en el dashboard de vencimientos.
        /// Ejemplo: "Vehículo 1234-ABC - ITV", "Empleado Juan García - Formación PRL".
        /// </summary>
        public string? DescripcionEntidad { get; set; }

        /// <summary>
        /// Fecha exacta de vencimiento de la entidad.
        /// </summary>
        public DateTime FechaVencimiento { get; set; }

        /// <summary>
        /// Número de días que quedan hasta el vencimiento en el momento de calcular la alerta.
        /// Negativo si ya está vencido. Se recalcula en cada ejecución del job.
        /// </summary>
        public int DiasRestantes { get; set; }

        /// <summary>
        /// Nivel de urgencia calculado a partir de DiasRestantes.
        /// Verde > 30 días | Amarillo 15-30 | Naranja 7-15 | Rojo menos de 7 días o vencido.
        /// </summary>
        public NivelAlertaVencimiento NivelAlerta { get; set; }

        /// <summary>
        /// Indica si la alerta ya fue gestionada (revisada y tomadas las acciones oportunas).
        /// Las alertas gestionadas se ocultan del dashboard pero no se eliminan.
        /// </summary>
        public bool EstaGestionada { get; set; } = false;

        /// <summary>
        /// Fecha y hora UTC en que se marcó la alerta como gestionada.
        /// </summary>
        public DateTime? FechaGestion { get; set; }

        /// <summary>
        /// Identificador del usuario que marcó la alerta como gestionada.
        /// </summary>
        public Guid? GestionadaPorId { get; set; }

        /// <summary>
        /// Notas sobre las acciones tomadas para gestionar el vencimiento.
        /// Ejemplo: "Cita ITV solicitada para el 15/03/2026", "Renovación en proceso".
        /// </summary>
        public string? NotasGestion { get; set; }
    }
}
