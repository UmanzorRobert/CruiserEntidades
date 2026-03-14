using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Proveedor o tecnología utilizada para la firma electrónica de documentos.
    /// </summary>
    public enum TipoProveedorFirma
    {
        /// <summary>Firma capturada en canvas HTML5 dentro de la PWA (firma biométrica simple).</summary>
        CanvasPWA = 1,
        /// <summary>Firma mediante certificado digital cualificado (eIDAS nivel avanzado).</summary>
        CertificadoDigital = 2,
        /// <summary>Firma mediante servicio externo DocuSign.</summary>
        DocuSign = 3,
        /// <summary>Firma mediante servicio externo Signaturit.</summary>
        Signaturit = 4,
        /// <summary>Firma mediante código OTP enviado al email del firmante.</summary>
        OTPEmail = 5,
        /// <summary>Firma mediante código OTP enviado por SMS al teléfono del firmante.</summary>
        OTPSMS = 6
    }
}
