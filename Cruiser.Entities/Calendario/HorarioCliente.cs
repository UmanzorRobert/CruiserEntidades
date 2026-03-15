using System;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Calendario
{
    /// <summary>
    /// Disponibilidad horaria semanal de un cliente para recibir el servicio de limpieza.
    /// Define en qué días y franjas horarias el cliente permite el acceso a sus instalaciones.
    ///
    /// Esta información se muestra en el calendario al seleccionar un cliente,
    /// y el sistema la usa para validar que una orden de servicio programada
    /// cae dentro de la disponibilidad declarada del cliente.
    ///
    /// TiempoMinimoAvisoHoras define con cuántas horas de antelación mínima
    /// se debe notificar al cliente antes de una visita no programada.
    ///
    /// Las InstruccionesAcceso (código puerta, nombre portero, instrucciones especiales)
    /// se muestran al empleado en la PWA al confirmar que va a acceder a las instalaciones.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasIndex(x => x.ClienteId).IsUnique();
    ///   builder.Property(x => x.InstruccionesAcceso).HasMaxLength(1000);
    /// </remarks>
    public class HorarioCliente : EntidadBase
    {
        /// <summary>FK hacia el cliente cuya disponibilidad horaria define esta entidad. Índice único (1-a-1).</summary>
        public Guid ClienteId { get; set; }

        // ── Disponibilidad por día ────────────────────────────────────────────

        /// <summary>Indica si el cliente permite visitas el lunes.</summary>
        public bool LunesDisponible { get; set; } = true;
        /// <summary>Hora de inicio de la ventana de disponibilidad del lunes. Formato HH:mm.</summary>
        public string? LunesInicio { get; set; }
        /// <summary>Hora de fin de la ventana de disponibilidad del lunes. Formato HH:mm.</summary>
        public string? LunesFin { get; set; }

        /// <summary>Indica si el cliente permite visitas el martes.</summary>
        public bool MartesDisponible { get; set; } = true;
        public string? MartesInicio { get; set; }
        public string? MartesFin { get; set; }

        /// <summary>Indica si el cliente permite visitas el miércoles.</summary>
        public bool MiercolesDisponible { get; set; } = true;
        public string? MiercolesInicio { get; set; }
        public string? MiercolesFin { get; set; }

        /// <summary>Indica si el cliente permite visitas el jueves.</summary>
        public bool JuevesDisponible { get; set; } = true;
        public string? JuevesInicio { get; set; }
        public string? JuevesFin { get; set; }

        /// <summary>Indica si el cliente permite visitas el viernes.</summary>
        public bool ViernesDisponible { get; set; } = true;
        public string? ViernesInicio { get; set; }
        public string? ViernesFin { get; set; }

        /// <summary>Indica si el cliente permite visitas el sábado.</summary>
        public bool SabadoDisponible { get; set; } = false;
        public string? SabadoInicio { get; set; }
        public string? SabadoFin { get; set; }

        /// <summary>Indica si el cliente permite visitas el domingo.</summary>
        public bool DomingoDisponible { get; set; } = false;
        public string? DomingoInicio { get; set; }
        public string? DomingoFin { get; set; }

        // ── Políticas de acceso ──────────────────────────────────────────────

        /// <summary>
        /// Indica si el cliente requiere cita previa para cualquier visita.
        /// True = no se pueden programar órdenes sin confirmar disponibilidad con el cliente.
        /// </summary>
        public bool RequiereCitaPrevia { get; set; } = false;

        /// <summary>
        /// Horas mínimas de antelación para notificar al cliente antes de una visita.
        /// Ejemplo: 24 = se debe avisar con al menos 24 horas de antelación.
        /// </summary>
        public int TiempoMinimoAvisoHoras { get; set; } = 24;

        /// <summary>
        /// Número máximo de empleados que pueden estar simultáneamente en las instalaciones.
        /// Relevante para clientes con instalaciones pequeñas o con restricciones de aforo.
        /// 0 = sin límite definido.
        /// </summary>
        public int CapacidadMaximaEmpleados { get; set; } = 0;

        /// <summary>
        /// Instrucciones específicas de acceso a las instalaciones del cliente.
        /// Ejemplo: "Tocar timbre 3 veces. Código portería: 1234. Hablar con recepción."
        /// Se muestran al empleado en la PWA al llegar a las instalaciones.
        /// </summary>
        public string? InstruccionesAcceso { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Cliente al que pertenece esta configuración de disponibilidad.</summary>
        public virtual Comercial.Cliente? Cliente { get; set; }
    }
}
