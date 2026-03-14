using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de segundo factor de autenticación (MFA) configurado por el usuario.
    /// </summary>
    public enum TipoMFA
    {
        /// <summary>Aplicación autenticadora TOTP (Google Authenticator, Authy, etc.).</summary>
        TOTP = 1,
        /// <summary>Código enviado por SMS al teléfono verificado del usuario.</summary>
        SMS = 2,
        /// <summary>Código enviado al email del usuario.</summary>
        Email = 3,
        /// <summary>Código de respaldo de un solo uso generado al activar MFA.</summary>
        CodigoRespaldo = 4
    }
}
