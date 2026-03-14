using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Método por el que se desbloqueó una cuenta previamente bloqueada.
    /// </summary>
    public enum MetodoDesbloqueo
    {
        /// <summary>Desbloqueo manual realizado por un administrador desde el panel.</summary>
        ManualAdmin = 1,
        /// <summary>Desbloqueo automático por expiración del tiempo de bloqueo configurado.</summary>
        AutomaticoExpiracion = 2,
        /// <summary>Desbloqueo mediante token de recuperación enviado al email del usuario.</summary>
        TokenRecuperacion = 3,
        /// <summary>Desbloqueo tras cambio exitoso de contraseña.</summary>
        CambioContrasena = 4
    }
}
