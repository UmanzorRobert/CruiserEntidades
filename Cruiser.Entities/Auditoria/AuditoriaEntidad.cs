using Cruiser.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Auditoria
{
    /// <summary>
    /// Registro granular de cada cambio realizado en cualquier campo de cualquier entidad
    /// del sistema. Se popula automáticamente mediante un interceptor de EF Core
    /// (SaveChangesInterceptor) que detecta cambios en el ChangeTracker.
    ///
    /// NO hereda de EntidadBase porque es un registro inmutable de solo escritura:
    /// auditarlo generaría un bucle infinito y carecería de sentido semántico.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.EntidadNombre).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.EntidadId).IsRequired().HasMaxLength(36);
    ///   builder.Property(x => x.CampoModificado).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.ValorAnterior).HasMaxLength(2000);
    ///   builder.Property(x => x.ValorNuevo).HasMaxLength(2000);
    ///   builder.HasIndex(x => new { x.EntidadNombre, x.EntidadId });
    ///   builder.HasIndex(x => x.FechaCambio);
    ///   builder.HasIndex(x => x.UsuarioId);
    /// </remarks>
    public class AuditoriaEntidad
    {
        /// <summary>Identificador único del registro de auditoría.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Nombre de la entidad/tabla afectada.
        /// Ejemplo: "Cliente", "Producto", "OrdenServicio".
        /// </summary>
        public string EntidadNombre { get; set; } = string.Empty;

        /// <summary>
        /// Identificador (GUID en formato string) del registro específico que fue modificado.
        /// Se almacena como string para soportar cualquier tipo de PK sin restricción de tipo.
        /// </summary>
        public string EntidadId { get; set; } = string.Empty;

        /// <summary>
        /// Nombre exacto del campo/propiedad que fue modificado.
        /// Ejemplo: "Email", "EstaActivo", "ImporteTotal".
        /// </summary>
        public string CampoModificado { get; set; } = string.Empty;

        /// <summary>
        /// Valor que tenía el campo ANTES de la modificación.
        /// Nulo si la operación fue una inserción (no había valor previo).
        /// Los valores complejos se serializan como JSON string.
        /// </summary>
        public string? ValorAnterior { get; set; }

        /// <summary>
        /// Valor que tiene el campo DESPUÉS de la modificación.
        /// Nulo si la operación fue una eliminación lógica donde el campo pierde valor.
        /// </summary>
        public string? ValorNuevo { get; set; }

        /// <summary>
        /// Fecha y hora UTC exacta en que se realizó el cambio.
        /// Se captura en el momento del SaveChanges del interceptor.
        /// </summary>
        public DateTime FechaCambio { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Identificador del usuario que realizó el cambio.
        /// Nulo para operaciones de sistema (jobs, seeds, migraciones).
        /// No se define FK explícita para evitar dependencias circulares.
        /// </summary>
        public Guid? UsuarioId { get; set; }

        /// <summary>
        /// Nombre de usuario capturado en el momento del cambio.
        /// Se almacena denormalizado para preservar el historial aunque el usuario se anonimice.
        /// </summary>
        public string? NombreUsuario { get; set; }

        /// <summary>
        /// Tipo de operación DML que generó este registro de auditoría.
        /// </summary>
        public TipoOperacionAuditoria TipoOperacion { get; set; }

        /// <summary>
        /// Dirección IP desde la que se originó la operación.
        /// Útil para trazabilidad de seguridad y cumplimiento GDPR.
        /// </summary>
        public string? DireccionIP { get; set; }

        /// <summary>
        /// Nombre del módulo funcional desde el que se realizó el cambio.
        /// Ejemplo: "Inventario", "Facturación", "RRHH".
        /// </summary>
        public string? Modulo { get; set; }
    }
}
