using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.GDPR
{
    /// <summary>
    /// Política GDPR versionada que define los textos legales y condiciones
    /// de consentimiento que el usuario debe aceptar. Cada vez que cambia el texto
    /// legal, se crea una nueva versión (nunca se edita la versión activa).
    ///
    /// Solo puede haber una versión activa por tipo de consentimiento al mismo tiempo.
    /// Al activar una nueva versión, la anterior se desactiva automáticamente.
    ///
    /// Los consentimientos de usuarios quedan vinculados a la versión vigente en el
    /// momento en que se otorgaron (ConsentimientoGDPR.VersionPoliticaPrivacidad).
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Version).IsRequired().HasMaxLength(20);
    ///   builder.Property(x => x.TextoLegal).IsRequired();
    ///   builder.Property(x => x.ResumenLegible).HasMaxLength(1000);
    ///   builder.HasIndex(x => new { x.TipoConsentimiento, x.EstaActiva });
    ///   builder.HasIndex(x => new { x.TipoConsentimiento, x.Version }).IsUnique();
    /// </remarks>
    public class PoliticaGDPR : EntidadBase
    {
        /// <summary>
        /// Tipo de consentimiento al que aplica esta política.
        /// Ejemplo: Marketing, UbicacionGPS, PoliticaPrivacidad.
        /// </summary>
        public TipoConsentimientoGDPR TipoConsentimiento { get; set; }

        /// <summary>
        /// Número de versión de la política en formato semántico.
        /// Ejemplo: "1.0", "1.1", "2.0". Se incrementa con cada cambio en el texto legal.
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Texto legal completo de la política en formato HTML.
        /// Es el texto que se muestra al usuario en el modal de consentimiento.
        /// Debe ser redactado y revisado por el responsable de protección de datos (DPO).
        /// </summary>
        public string TextoLegal { get; set; } = string.Empty;

        /// <summary>
        /// Resumen en lenguaje claro y comprensible de lo que el usuario está aceptando.
        /// Complementa el texto legal para mejorar la transparencia (Art. 12 RGPD).
        /// </summary>
        public string? ResumenLegible { get; set; }

        /// <summary>
        /// Indica si esta versión de la política está actualmente en vigor.
        /// Solo puede estar activa una versión por tipo de consentimiento.
        /// </summary>
        public bool EstaActiva { get; set; } = false;

        /// <summary>
        /// Fecha y hora UTC en que esta versión de la política fue publicada y entró en vigor.
        /// </summary>
        public DateTime FechaPublicacion { get; set; }

        /// <summary>
        /// Fecha y hora UTC en que esta versión fue sustituida por una versión más nueva.
        /// Nulo si la política sigue activa o nunca fue reemplazada.
        /// </summary>
        public DateTime? FechaDesactivacion { get; set; }

        /// <summary>
        /// Idioma de esta versión de la política en formato BCP-47.
        /// Ejemplo: "es-ES", "en-US", "ca-ES".
        /// Permite mantener versiones en múltiples idiomas.
        /// </summary>
        public string Idioma { get; set; } = "es-ES";

        /// <summary>
        /// Identificador del responsable de protección de datos (DPO) que aprobó esta versión.
        /// </summary>
        public Guid? AprobadaPorId { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Consentimientos otorgados bajo esta versión de la política.</summary>
        public virtual ICollection<ConsentimientoGDPR> Consentimientos { get; set; }
            = new List<ConsentimientoGDPR>();
    }
}
