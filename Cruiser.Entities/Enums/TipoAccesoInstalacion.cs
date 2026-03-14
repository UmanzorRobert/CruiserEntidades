using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de credencial o mecanismo de acceso a las instalaciones del cliente.
    /// Los valores se almacenan cifrados en la entidad ClavesAccesoInstalacion.
    /// </summary>
    public enum TipoAccesoInstalacion
    {
        /// <summary>Código numérico de teclado para portón, portero automático o alarma.</summary>
        CodigoTeclado = 1,
        /// <summary>Tarjeta RFID o llave electrónica de acceso.</summary>
        TarjetaRFID = 2,
        /// <summary>Llave física convencional del inmueble.</summary>
        LlaveFisica = 3,
        /// <summary>Código de aplicación móvil o QR para acceso sin llave.</summary>
        CodigoApp = 4,
        /// <summary>Código PIN del sistema de alarma (activación/desactivación).</summary>
        CodigoAlarma = 5,
        /// <summary>Credenciales de videoportero o intercomunicador.</summary>
        Videoportero = 6,
        /// <summary>Otro tipo de credencial de acceso no contemplado.</summary>
        Otro = 99
    }
}
