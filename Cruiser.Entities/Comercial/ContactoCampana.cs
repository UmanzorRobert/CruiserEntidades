using System;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Registro individual del estado de contacto de un cliente específico
    /// dentro de una campaña comercial.
    ///
    /// Se crea un registro por cada cliente incluido en la campaña al ejecutar el envío.
    /// Almacena si el email fue enviado, abierto y si el cliente se convirtió.
    ///
    /// NO hereda de EntidadBase: es un log de estado append-only.
    /// El tracking de aperturas se realiza mediante un píxel de seguimiento
    /// incrustado en el HTML del email.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.HasIndex(x => new { x.CampanaComercialId, x.ClienteId }).IsUnique();
    ///   builder.HasIndex(x => x.FechaEnvio);
    ///   builder.HasIndex(x => x.EsConvertido);
    /// </remarks>
    public class ContactoCampana
    {
        /// <summary>Identificador único del registro de contacto.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>FK hacia la campaña comercial a la que pertenece este registro.</summary>
        public Guid CampanaComercialId { get; set; }

        /// <summary>FK hacia el cliente contactado en esta campaña.</summary>
        public Guid ClienteId { get; set; }

        /// <summary>Fecha y hora UTC en que se envió el email al cliente.</summary>
        public DateTime? FechaEnvio { get; set; }

        /// <summary>Indica si el email fue enviado exitosamente (sin rebote).</summary>
        public bool EstaEnviado { get; set; } = false;

        /// <summary>
        /// Indica si el cliente abrió el email de la campaña.
        /// Detectado mediante píxel de seguimiento en el HTML del email.
        /// </summary>
        public bool EstaAbierto { get; set; } = false;

        /// <summary>Fecha y hora UTC de la primera apertura del email.</summary>
        public DateTime? FechaPrimeraApertura { get; set; }

        /// <summary>
        /// Indica si el cliente realizó una conversión (contrató un servicio,
        /// solicitó una cotización o completó la acción objetivo de la campaña).
        /// </summary>
        public bool EsConvertido { get; set; } = false;

        /// <summary>Fecha y hora UTC en que se registró la conversión del cliente.</summary>
        public DateTime? FechaConversion { get; set; }

        /// <summary>Error de envío si el email no pudo ser entregado (rebote, dirección inválida).</summary>
        public string? ErrorEnvio { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Campaña comercial a la que pertenece este registro de contacto.</summary>
        public virtual CampanaComercial? CampanaComercial { get; set; }

        /// <summary>Cliente contactado en la campaña.</summary>
        public virtual Cliente? Cliente { get; set; }
    }
}
