using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de incidencia de seguridad registrada en el sistema.
    /// </summary>
    public enum TipoIncidencia
    {
        /// <summary>Intento de acceso con credenciales incorrectas.</summary>
        LoginFallido = 1,

        /// <summary>Cuenta de usuario bloqueada por exceso de intentos fallidos.</summary>
        CuentaBloqueada = 2,

        /// <summary>Intento de acceso a un recurso sin los permisos necesarios.</summary>
        AccesoNoAutorizado = 3,

        /// <summary>Posible ataque de fuerza bruta detectado desde una IP.</summary>
        FuerzaBruta = 4,

        /// <summary>Token JWT inválido, expirado o manipulado.</summary>
        TokenInvalido = 5,

        /// <summary>Violación de la política de contraseñas.</summary>
        ViolacionPoliticaContrasena = 6,

        /// <summary>Solicitud que excede los límites de tasa (rate limiting).</summary>
        RateLimitExcedido = 7,

        /// <summary>Cambio en la configuración de seguridad del sistema.</summary>
        CambioConfiguracionSeguridad = 8,

        /// <summary>Acceso desde un dispositivo o IP no reconocidos.</summary>
        DispositivoDesconocido = 9,

        /// <summary>Otro tipo de incidencia no clasificada.</summary>
        Otro = 99
    }
}
