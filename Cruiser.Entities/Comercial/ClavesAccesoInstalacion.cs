using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Credencial o clave de acceso a las instalaciones de un cliente.
    /// Almacena de forma cifrada (AES-256) los códigos, PINs, o referencias
    /// a llaves físicas necesarias para que los empleados accedan a las instalaciones.
    ///
    /// El Valor siempre se almacena cifrado y nunca se muestra en claro en la interfaz.
    /// El empleado con acceso puede "consultar" la clave, lo que genera un registro
    /// en HistorialAccesoClaveInstalacion para auditoría.
    ///
    /// Las claves con RequiereDevolucion=true (llaves físicas) generan una alerta
    /// cuando un empleado finaliza su asignación al cliente sin haber devuelto la llave.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.ValorCifrado).IsRequired().HasMaxLength(1000);
    ///   builder.HasIndex(x => new { x.ClienteId, x.TipoAcceso, x.EstaActivo });
    ///   builder.HasIndex(x => x.FechaVencimiento).HasFilter("\"FechaVencimiento\" IS NOT NULL");
    /// </remarks>
    public class ClavesAccesoInstalacion : EntidadBase
    {
        /// <summary>FK hacia el cliente cuyas instalaciones requieren esta clave de acceso.</summary>
        public Guid ClienteId { get; set; }

        /// <summary>Tipo de mecanismo de acceso que representa esta clave.</summary>
        public TipoAccesoInstalacion TipoAcceso { get; set; }

        /// <summary>
        /// Descripción del acceso. Ejemplo: "Código portero automático portal principal",
        /// "PIN alarma oficina 3ª planta", "Llave puerta trasera almacén".
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Valor de la clave cifrado con AES-256 usando la clave maestra del sistema.
        /// NUNCA se almacena en texto plano. Se descifra solo en el momento de visualización
        /// y se registra el acceso en HistorialAccesoClaveInstalacion.
        /// </summary>
        public string ValorCifrado { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del empleado responsable de custodiar esta clave.
        /// Especialmente relevante para llaves físicas (RequiereDevolucion=true).
        /// </summary>
        public Guid? EmpleadoResponsableId { get; set; }

        /// <summary>
        /// Fecha de vencimiento de la clave (para códigos temporales, tarjetas con fecha).
        /// Genera una AlertaVencimiento cuando se acerca el vencimiento.
        /// </summary>
        public DateOnly? FechaVencimiento { get; set; }

        /// <summary>
        /// Indica si esta clave requiere ser devuelta al cliente o a la empresa
        /// cuando el empleado responsable cambia o termina el contrato.
        /// True para llaves físicas, tarjetas RFID, etc.
        /// </summary>
        public bool RequiereDevolucion { get; set; } = false;

        /// <summary>Indica si ha sido devuelta cuando RequiereDevolucion=true.</summary>
        public bool EstaDevuelta { get; set; } = false;

        /// <summary>Fecha en que fue devuelta la clave.</summary>
        public DateOnly? FechaDevolucion { get; set; }

        /// <summary>Notas adicionales sobre el uso o restricciones de esta clave.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Cliente al que pertenecen las instalaciones.</summary>
        public virtual Cliente? Cliente { get; set; }

        /// <summary>Historial de consultas y usos de esta clave por parte de los empleados.</summary>
        public virtual ICollection<HistorialAccesoClaveInstalacion> HistorialAccesos { get; set; }
            = new List<HistorialAccesoClaveInstalacion>();
    }
}
