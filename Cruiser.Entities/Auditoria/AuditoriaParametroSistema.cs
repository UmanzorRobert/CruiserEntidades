using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Auditoria
{
    /// <summary>
    /// Registro histórico de cada modificación realizada sobre parámetros de configuración
    /// del sistema (entidad ParametroSistema). Permite auditar quién cambió qué parámetro,
    /// cuándo, por qué motivo y desde dónde.
    ///
    /// NO hereda de EntidadBase por ser un registro de solo escritura e inmutable.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.ValorAnterior).HasMaxLength(1000);
    ///   builder.Property(x => x.ValorNuevo).IsRequired().HasMaxLength(1000);
    ///   builder.Property(x => x.Motivo).HasMaxLength(500);
    ///   builder.Property(x => x.DireccionIP).HasMaxLength(45);
    ///   builder.HasIndex(x => x.ParametroSistemaId);
    ///   builder.HasIndex(x => x.FechaCambio);
    ///
    ///   Relación:
    ///   builder.HasOne(x => x.ParametroSistema)
    ///          .WithMany(p => p.HistorialAuditoria)
    ///          .HasForeignKey(x => x.ParametroSistemaId)
    ///          .OnDelete(DeleteBehavior.Restrict);
    /// </remarks>
    public class AuditoriaParametroSistema
    {
        /// <summary>Identificador único del registro de auditoría de parámetro.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Identificador del parámetro de sistema que fue modificado.
        /// FK hacia la entidad ParametroSistema.
        /// </summary>
        public Guid ParametroSistemaId { get; set; }

        /// <summary>
        /// Identificador del usuario que realizó el cambio del parámetro.
        /// Nulo si el cambio fue realizado por el sistema automáticamente.
        /// </summary>
        public Guid? UsuarioId { get; set; }

        /// <summary>
        /// Nombre del usuario capturado en el momento del cambio.
        /// Denormalizado para mantener el historial si el usuario es anonimizado.
        /// </summary>
        public string? NombreUsuario { get; set; }

        /// <summary>
        /// Fecha y hora UTC exacta en que se modificó el parámetro.
        /// </summary>
        public DateTime FechaCambio { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Valor que tenía el parámetro ANTES de la modificación.
        /// Nulo si el parámetro es nuevo y no tenía valor previo.
        /// </summary>
        public string? ValorAnterior { get; set; }

        /// <summary>
        /// Valor que tiene el parámetro DESPUÉS de la modificación.
        /// </summary>
        public string ValorNuevo { get; set; } = string.Empty;

        /// <summary>
        /// Motivo o justificación del cambio proporcionado por el usuario.
        /// Recomendado para cambios en parámetros críticos de seguridad y facturación.
        /// </summary>
        public string? Motivo { get; set; }

        /// <summary>
        /// Dirección IP desde la que se realizó la modificación del parámetro.
        /// Soporta IPv4 (máx 15 chars) e IPv6 (máx 45 chars).
        /// </summary>
        public string? DireccionIP { get; set; }

        // ── Navegación ──────────────────────────────────────────────────────────
        // Nota: Las propiedades de navegación se declaran virtual para
        // compatibilidad con proxies de EF Core si se habilitan.

        /// <summary>Referencia al parámetro de sistema modificado.</summary>
        public virtual Configuracion.ParametroSistema? ParametroSistema { get; set; }
    }
}
