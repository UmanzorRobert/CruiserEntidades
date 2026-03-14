using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.GDPR
{
    /// <summary>
    /// Trazabilidad completa de cada operación de anonimización GDPR ejecutada en el sistema.
    /// Constituye el comprobante legal auditable de que se ejecutó el "Derecho al Olvido"
    /// (Art. 17 RGPD) de forma correcta y sin eliminación física de registros.
    ///
    /// Este registro es INMUTABLE una vez creado. Representa la prueba legal de la operación.
    /// El TokenCumplimiento es el identificador único que puede entregarse al interesado
    /// como comprobante y que puede verificarse en auditorías externas.
    ///
    /// Los campos JSONB CamposAnonimizados y HashValoresOriginales permiten demostrar
    /// exactamente qué campos fueron afectados y que la operación fue irreversible.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.TipoEntidad).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.EntidadId).IsRequired().HasMaxLength(36);
    ///   builder.Property(x => x.CamposAnonimizados).HasColumnType("jsonb");
    ///   builder.Property(x => x.HashValoresOriginales).HasColumnType("jsonb");
    ///   builder.Property(x => x.TokenCumplimiento).IsRequired().HasMaxLength(256);
    ///   builder.HasIndex(x => x.TokenCumplimiento).IsUnique();
    ///   builder.HasIndex(x => new { x.TipoEntidad, x.EntidadId });
    ///   builder.HasIndex(x => x.SolicitudGDPRId);
    ///   builder.HasIndex(x => x.CamposAnonimizados).HasMethod("gin");
    ///
    ///   Estructura JSON de CamposAnonimizados:
    ///   { "Nombre": "GDPR_DEL_a3f9b2c1", "Email": "gdpr_a3f9b2c1@noreply.gdpr",
    ///     "Telefono": "000-GDPR-a3f9b2c1", "NIF": "GDPR_NIF_a3f9b2c1" }
    ///
    ///   Estructura JSON de HashValoresOriginales:
    ///   { "Nombre": "sha256_hash_del_valor_original", "Email": "sha256_hash..." }
    /// </remarks>
    public class RegistroAnonimizacionGDPR : EntidadBase
    {
        /// <summary>
        /// Identificador de la solicitud GDPR que originó esta operación de anonimización.
        /// FK hacia SolicitudGDPR.
        /// </summary>
        public Guid SolicitudGDPRId { get; set; }

        /// <summary>
        /// Nombre de la entidad/tabla cuyos datos fueron anonimizados.
        /// Ejemplo: "Cliente", "Empleado", "Usuario".
        /// </summary>
        public string TipoEntidad { get; set; } = string.Empty;

        /// <summary>
        /// Identificador (GUID en formato string) del registro específico que fue anonimizado.
        /// </summary>
        public string EntidadId { get; set; } = string.Empty;

        /// <summary>
        /// Mapa JSON de los campos anonimizados con sus nuevos valores pseudonimizados.
        /// Almacenado en JSONB con índice GIN para consultas de auditoría.
        /// Permite verificar exactamente qué campos fueron modificados y cómo.
        /// NUNCA contiene los valores originales, solo los valores de reemplazo.
        /// </summary>
        public string? CamposAnonimizados { get; set; }

        /// <summary>
        /// Mapa JSON de hashes SHA-256 de los valores originales antes de la anonimización.
        /// Permite demostrar criptográficamente que un valor fue anonimizado
        /// sin revelar el valor original (el hash es irreversible).
        /// Almacenado en JSONB para consultas de verificación.
        /// </summary>
        public string? HashValoresOriginales { get; set; }

        /// <summary>
        /// Token único alfanumérico (UUID v4) que identifica unívocamente esta operación.
        /// Es el comprobante legal que se entrega al interesado y que puede usarse
        /// para verificar la operación en auditorías externas.
        /// Generado con criptografía segura. Inmutable tras su creación.
        /// </summary>
        public string TokenCumplimiento { get; set; } = string.Empty;

        /// <summary>
        /// Fecha y hora UTC exacta en que se ejecutó la operación de anonimización.
        /// </summary>
        public DateTime FechaAnonimizacion { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Identificador del responsable de datos (DPO o admin) que ejecutó la operación.
        /// </summary>
        public Guid RealizadoPorId { get; set; }

        /// <summary>
        /// Técnica de anonimización aplicada sobre los datos personales.
        /// El método principal del sistema es Pseudonimizacion.
        /// </summary>
        public MetodoAnonimizacion MetodoAnonimizacion { get; set; }
            = MetodoAnonimizacion.Pseudonimizacion;

        /// <summary>
        /// Indica si la operación de anonimización fue verificada y validada
        /// por el responsable de datos tras su ejecución.
        /// Las operaciones no verificadas aparecen como pendientes de validación en el dashboard GDPR.
        /// </summary>
        public bool EsVerificada { get; set; } = false;

        /// <summary>
        /// Fecha y hora UTC en que se verificó la operación. Nulo si aún no se ha verificado.
        /// </summary>
        public DateTime? FechaVerificacion { get; set; }

        /// <summary>
        /// Identificador del responsable que verificó la operación.
        /// </summary>
        public Guid? VerificadaPorId { get; set; }

        /// <summary>
        /// Notas adicionales sobre la operación de anonimización.
        /// Puede incluir observaciones del DPO o incidencias durante el proceso.
        /// </summary>
        public string? Observaciones { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Solicitud GDPR que originó esta operación de anonimización.</summary>
        public virtual SolicitudGDPR? SolicitudGDPR { get; set; }
    }
}
