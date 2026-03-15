using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.RRHH
{
    /// <summary>
    /// Registro de una formación o certificación laboral obtenida por un empleado.
    /// Gestiona la vigencia de los certificados obligatorios (PRL, productos químicos)
    /// con alertas automáticas antes del vencimiento.
    ///
    /// Las formaciones con EsObligatoria=true y FechaCaducidad superada bloquean
    /// la asignación del empleado a nuevas órdenes de servicio hasta que la formación
    /// sea renovada y registrada en el sistema.
    ///
    /// El RutaCertificado permite almacenar el PDF oficial del certificado para
    /// verificación por inspecciones de trabajo o auditorías de calidad.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.NumeroCertificado).HasMaxLength(100);
    ///   builder.Property(x => x.RutaCertificado).HasMaxLength(1000);
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.TipoFormacion });
    ///   builder.HasIndex(x => x.FechaCaducidad).HasFilter("\"FechaCaducidad\" IS NOT NULL");
    ///   builder.HasIndex(x => new { x.EsObligatoria, x.FechaCaducidad });
    /// </remarks>
    public class FormacionEmpleado : EntidadBase
    {
        /// <summary>FK hacia el empleado que posee esta formación o certificación.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>Tipo de formación o certificación.</summary>
        public TipoFormacion TipoFormacion { get; set; }

        /// <summary>Nombre completo de la formación o certificación.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Entidad o empresa que impartió y certificó la formación.</summary>
        public string? EntidadCertificadora { get; set; }

        /// <summary>Fecha de obtención o superación de la formación.</summary>
        public DateOnly FechaObtencion { get; set; }

        /// <summary>
        /// Fecha de caducidad del certificado.
        /// Nulo si la formación no caduca (titulaciones académicas, por ejemplo).
        /// Genera AlertaVencimiento automática cuando se acerca la fecha.
        /// </summary>
        public DateOnly? FechaCaducidad { get; set; }

        /// <summary>Resultado obtenido: Apto, No Apto, Aprobado, Suspenso, calificación numérica.</summary>
        public string? Resultado { get; set; }

        /// <summary>
        /// Número del certificado emitido por la entidad certificadora.
        /// Necesario para verificar la autenticidad del certificado en inspecciones.
        /// </summary>
        public string? NumeroCertificado { get; set; }

        /// <summary>Ruta relativa del PDF del certificado oficial almacenado en el sistema.</summary>
        public string? RutaCertificado { get; set; }

        /// <summary>Duración de la formación en horas. Informativo para el expediente del empleado.</summary>
        public decimal? HorasDuracion { get; set; }

        /// <summary>
        /// Indica si esta formación es obligatoria para el puesto del empleado.
        /// True bloquea la asignación a órdenes si está caducada.
        /// </summary>
        public bool EsObligatoria { get; set; } = false;

        /// <summary>Notas adicionales sobre la formación o el proceso de renovación.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Empleado poseedor de esta formación.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }
    }
}