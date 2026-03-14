using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Contacto adicional de una entidad del sistema (Cliente, Proveedor o Empleado).
    /// Permite registrar múltiples personas de contacto con distintos roles
    /// para cada entidad sin duplicar la entidad principal.
    ///
    /// El polimorfismo se implementa mediante TipoEntidad (string) y EntidadId (Guid),
    /// siguiendo el mismo patrón que DireccionCompleta para máxima flexibilidad.
    ///
    /// Ejemplo: un cliente puede tener un contacto de facturación (para enviar facturas),
    /// un contacto técnico (para coordinar el acceso) y un contacto de gerencia.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150);
    ///   builder.Property(x => x.TipoEntidad).IsRequired().HasMaxLength(50);
    ///   builder.Property(x => x.Email).HasMaxLength(256);
    ///   builder.Property(x => x.Telefono).HasMaxLength(20);
    ///   builder.HasIndex(x => new { x.TipoEntidad, x.EntidadId });
    ///   builder.HasIndex(x => x.Email).HasFilter("\"Email\" IS NOT NULL");
    /// </remarks>
    public class ContactoAdicional : EntidadBase
    {
        // ── Polimorfismo ─────────────────────────────────────────────────────

        /// <summary>
        /// Nombre de la entidad propietaria de este contacto.
        /// Valores válidos: "Cliente", "Proveedor", "Empleado".
        /// </summary>
        public string TipoEntidad { get; set; } = string.Empty;

        /// <summary>
        /// Identificador GUID de la entidad propietaria de este contacto.
        /// Sin FK explícita en BD (referencia polimórfica).
        /// </summary>
        public Guid EntidadId { get; set; }

        // ── Datos del contacto ───────────────────────────────────────────────

        /// <summary>Nombre completo de la persona de contacto.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Apellidos de la persona de contacto.</summary>
        public string? Apellidos { get; set; }

        /// <summary>Cargo o puesto de la persona de contacto en su organización.</summary>
        public string? Cargo { get; set; }

        /// <summary>Email de contacto directo de la persona.</summary>
        public string? Email { get; set; }

        /// <summary>Teléfono fijo directo de la persona de contacto.</summary>
        public string? Telefono { get; set; }

        /// <summary>Teléfono móvil directo de la persona de contacto.</summary>
        public string? TelefonoMovil { get; set; }

        // ── Roles y permisos ─────────────────────────────────────────────────

        /// <summary>Tipo principal de este contacto en la entidad.</summary>
        public TipoContactoAdicional TipoContacto { get; set; } = TipoContactoAdicional.Comercial;

        /// <summary>Indica si este contacto recibe facturas y documentos de facturación.</summary>
        public bool EsFacturacion { get; set; } = false;

        /// <summary>Indica si este contacto es el interlocutor comercial para contratos y pedidos.</summary>
        public bool EsComercial { get; set; } = false;

        /// <summary>Indica si este contacto es el responsable técnico para coordinar los servicios.</summary>
        public bool EsTecnico { get; set; } = false;

        /// <summary>
        /// Indica si este contacto debe recibir las comunicaciones de emergencia o urgencias.
        /// </summary>
        public bool EsContactoEmergencia { get; set; } = false;

        /// <summary>Indica si este es el contacto principal de la entidad.</summary>
        public bool EsPrincipal { get; set; } = false;

        /// <summary>Notas sobre el contacto y su disponibilidad.</summary>
        public string? Notas { get; set; }
    }
}
