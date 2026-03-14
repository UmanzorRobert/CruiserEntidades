using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Base
{
    /// <summary>
    /// Clase base abstracta de la que heredan todas las entidades principales del sistema.
    /// Provee campos de auditoría automática, control de estado (soft-delete) y soporte
    /// completo para anonimización GDPR (Art. 17) sin eliminación física de registros.
    /// </summary>
    /// /// <remarks>
    /// Fluent API – aplicar en cada IEntityTypeConfiguration&lt;T&gt;:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.FechaCreacion).IsRequired();
    ///   builder.Property(x => x.TokenAnonimizacion).HasMaxLength(100);
    ///   builder.HasQueryFilter(x => x.EstaActivo);  // filtro global soft-delete (opcional por entidad)
    /// </remarks>
    public abstract class EntidadBase
    {
        /// <summary>
        /// Identificador único del registro. Se genera automáticamente como GUID v4.
        /// Es la clave primaria de todas las entidades que heredan esta clase.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Fecha y hora UTC exacta en que se creó el registro.
        /// Se asigna automáticamente mediante el interceptor de EF Core (SavingChangesInterceptor).
        /// </summary>
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fecha y hora UTC de la última modificación del registro.
        /// Nulo si el registro nunca ha sido modificado tras su creación.
        /// Se actualiza automáticamente mediante el interceptor de EF Core.
        /// </summary>
        public DateTime? FechaModificacion { get; set; }

        /// <summary>
        /// Identificador del usuario que creó el registro.
        /// Puede ser nulo en operaciones de sistema (jobs Hangfire, seeds iniciales).
        /// No se define FK explícita para evitar dependencia circular con la entidad Usuario.
        /// </summary>
        public Guid? UsuarioCreacionId { get; set; }

        /// <summary>
        /// Identificador del usuario que realizó la última modificación del registro.
        /// Puede ser nulo si el registro no ha sido modificado o la operación fue de sistema.
        /// </summary>
        public Guid? UsuarioModificacionId { get; set; }

        /// <summary>
        /// Indica si el registro está activo en el sistema.
        /// Valor false equivale a un "soft-delete": el registro existe en BD pero
        /// no aparece en las consultas normales (filtro global de EF Core).
        /// Por defecto true al crear cualquier entidad.
        /// </summary>
        public bool EstaActivo { get; set; } = true;

        /// <summary>
        /// Indica si los datos personales de este registro han sido anonimizados
        /// en cumplimiento del Art. 17 del GDPR (Derecho al olvido).
        /// La anonimización NUNCA implica eliminación física del registro.
        /// Los campos fiscales y de facturación permanecen intactos por obligación legal.
        /// </summary>
        public bool EstaAnonimizado { get; set; } = false;

        /// <summary>
        /// Fecha y hora UTC en que se ejecutó la operación de anonimización GDPR.
        /// Nulo si el registro no ha sido anonimizado.
        /// </summary>
        public DateTime? FechaAnonimizacion { get; set; }

        /// <summary>
        /// Token único alfanumérico generado durante la anonimización GDPR.
        /// Sirve como comprobante legal auditable de la operación.
        /// Los campos personales anonimizados adoptan el formato: "GDPR_DEL_{TokenAnonimizacion}".
        /// Ejemplo: Nombre → "GDPR_DEL_a3f9b2c1", Email → "gdpr_a3f9b2c1@noreply.gdpr"
        /// </summary>
        public string? TokenAnonimizacion { get; set; }
    }
}
