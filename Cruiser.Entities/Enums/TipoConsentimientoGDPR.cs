using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de consentimiento GDPR que puede otorgar o revocar un interesado.
    /// Basado en los fundamentos legales del Art. 6 del RGPD.
    /// </summary>
    public enum TipoConsentimientoGDPR
    {
        /// <summary>Consentimiento para envío de comunicaciones comerciales y promociones.</summary>
        Marketing = 1,
        /// <summary>Consentimiento para captura y procesamiento de datos de geolocalización GPS.</summary>
        UbicacionGPS = 2,
        /// <summary>Consentimiento para compartir datos con terceros (proveedores, socios).</summary>
        DatosTerceros = 3,
        /// <summary>Consentimiento para el tratamiento de datos de salud o sensibles.</summary>
        DatosSensibles = 4,
        /// <summary>Consentimiento para el envío de notificaciones push en el navegador.</summary>
        NotificacionesPush = 5,
        /// <summary>Aceptación de la política de privacidad y términos del servicio.</summary>
        PoliticaPrivacidad = 6,
        /// <summary>Consentimiento para el uso de cookies no esenciales y analítica.</summary>
        Cookies = 7
    }
}
