using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Motivo o contexto del cambio de contraseña registrado en el historial.
    /// </summary>
    public enum TipoCambioContrasena
    {
        /// <summary>Cambio voluntario iniciado por el propio usuario.</summary>
        CambioManual = 1,
        /// <summary>Cambio forzado por política de expiración de contraseñas.</summary>
        CambioForzadoPolitica = 2,
        /// <summary>Cambio tras proceso de recuperación de contraseña olvidada.</summary>
        Recuperacion = 3,
        /// <summary>Contraseña provisional asignada al crear la cuenta por un administrador.</summary>
        PasswordInicialAdmin = 4,
        /// <summary>Cambio realizado directamente por un administrador del sistema.</summary>
        ResetPorAdmin = 5,
        /// <summary>Cambio obligatorio en el primer inicio de sesión del usuario.</summary>
        PrimerIngreso = 6
    }
}
