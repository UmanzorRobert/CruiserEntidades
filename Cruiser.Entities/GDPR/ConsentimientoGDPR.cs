using System;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.GDPR
{
    /// <summary>
    /// Registro inmutable (append-only) de cada consentimiento o revocación GDPR
    /// otorgado por un interesado (cliente o empleado).
    ///
    /// PRINCIPIO CLAVE: Los consentimientos NUNCA se editan ni eliminan.
    /// Cada cambio (aceptación o revocación) genera un nuevo registro.
    /// El estado vigente del consentimiento se determina leyendo el último registro
    /// por tipo para ese interesado.
    ///
    /// Vinculado polimórficamente a Cliente o Empleado mediante ClienteId / EmpleadoId
    /// (solo uno de los dos será no nulo en cada registro).
    ///
    /// NO hereda de EntidadBase: es un registro append-only inmutable por diseño legal.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.DireccionIp).HasMaxLength(45);
    ///   builder.Property(x => x.VersionPoliticaPrivacidad).HasMaxLength(20);
    ///   builder.HasIndex(x => new { x.ClienteId, x.TipoConsentimiento, x.FechaOtorgamiento });
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.TipoConsentimiento, x.FechaOtorgamiento });
    ///   builder.HasIndex(x => x.FechaOtorgamiento);
    ///
    ///   Relaciones:
    ///   builder.HasOne(c => c.PoliticaGDPR).WithMany(p => p.Consentimientos)
    ///          .HasForeignKey(c => c.PoliticaGDPRId).OnDelete(DeleteBehavior.Restrict);
    /// </remarks>
    public class ConsentimientoGDPR
    {
        /// <summary>Identificador único del registro de consentimiento.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Identificador del cliente que otorgó o revocó el consentimiento.
        /// Mutuamente excluyente con EmpleadoId: solo uno debe tener valor.
        /// </summary>
        public Guid? ClienteId { get; set; }

        /// <summary>
        /// Identificador del empleado que otorgó o revocó el consentimiento.
        /// Mutuamente excluyente con ClienteId: solo uno debe tener valor.
        /// </summary>
        public Guid? EmpleadoId { get; set; }

        /// <summary>
        /// Identificador de la versión de la política GDPR vigente en el momento
        /// en que se registró este consentimiento. Trazabilidad legal obligatoria.
        /// </summary>
        public Guid? PoliticaGDPRId { get; set; }

        /// <summary>
        /// Tipo de consentimiento que se está registrando en este registro.
        /// </summary>
        public TipoConsentimientoGDPR TipoConsentimiento { get; set; }

        /// <summary>
        /// Versión exacta de la política de privacidad que el interesado aceptó.
        /// Se almacena como string además de la FK para preservar el dato si la política cambia.
        /// Ejemplo: "2.1".
        /// </summary>
        public string? VersionPoliticaPrivacidad { get; set; }

        /// <summary>
        /// Indica si el interesado ACEPTÓ el consentimiento en este registro.
        /// True = aceptación, False = revocación.
        /// </summary>
        public bool EsAceptado { get; set; }

        /// <summary>
        /// Indica si este registro es una REVOCACIÓN de un consentimiento previamente otorgado.
        /// Complementario a EsAceptado: cuando EsAceptado=false y EsRevocado=true es revocación explícita.
        /// </summary>
        public bool EsRevocado { get; set; } = false;

        /// <summary>
        /// Fecha y hora UTC en que se registró el consentimiento o revocación.
        /// Campo crítico para auditoría legal. No puede ser nulo ni modificado.
        /// </summary>
        public DateTime FechaOtorgamiento { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Dirección IP del cliente desde la que se registró el consentimiento.
        /// Requerida por el Art. 7.1 RGPD como evidencia del consentimiento.
        /// </summary>
        public string? DireccionIp { get; set; }

        /// <summary>
        /// User-Agent del navegador desde el que se registró el consentimiento.
        /// Complementa la dirección IP como evidencia técnica del consentimiento.
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// Canal o método a través del cual se obtuvo el consentimiento.
        /// Ejemplos: "Formulario web", "App móvil", "Contrato firmado", "Llamada telefónica".
        /// </summary>
        public string? CanalConsentimiento { get; set; }

        /// <summary>
        /// Identificador del empleado o sistema que registró el consentimiento en nombre del interesado.
        /// Nulo si el interesado lo registró directamente él mismo.
        /// </summary>
        public Guid? RegistradoPorId { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Política GDPR vigente en el momento del consentimiento.</summary>
        public virtual PoliticaGDPR? PoliticaGDPR { get; set; }
    }
}
