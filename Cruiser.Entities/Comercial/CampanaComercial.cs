using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Comercial
{
    /// <summary>
    /// Campaña de marketing o comunicación comercial dirigida a un segmento de clientes.
    /// Permite enviar emails masivos personalizados usando PlantillaEmail y hacer
    /// seguimiento de métricas de apertura, clics y conversiones.
    ///
    /// El envío masivo se realiza mediante el job de Hangfire
    /// ICampanaComercialService.EjecutarEnvio, que respeta los consentimientos GDPR
    /// de cada cliente (ConsentimientoMarketing=true) antes de incluirlo en el envío.
    ///
    /// La tasa de conversión se calcula como:
    /// Conversiones / ClientesIncluidos × 100.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.AsuntoEmail).HasMaxLength(300);
    ///   builder.HasIndex(x => new { x.Estado, x.FechaInicio });
    ///   builder.HasIndex(x => x.SegmentoObjetivo);
    /// </remarks>
    public class CampanaComercial : EntidadBase
    {
        /// <summary>Nombre descriptivo interno de la campaña.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del objetivo y contexto de la campaña.</summary>
        public string? Descripcion { get; set; }

        /// <summary>Objetivo principal de la campaña.</summary>
        public TipoObjetivoCampana TipoObjetivo { get; set; }

        /// <summary>Fecha y hora UTC de inicio de la campaña (primer envío).</summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>Fecha y hora UTC de fin de la campaña. Nulo si es sin fecha límite.</summary>
        public DateTime? FechaFin { get; set; }

        /// <summary>
        /// Segmento de clientes objetivo de la campaña.
        /// El sistema filtra automáticamente los clientes de este segmento con
        /// ConsentimientoMarketing=true al ejecutar el envío.
        /// Nulo = todos los segmentos (campaña general).
        /// </summary>
        public SegmentoCliente? SegmentoObjetivo { get; set; }

        /// <summary>
        /// FK hacia la plantilla de email usada en el envío masivo.
        /// La plantilla define el diseño HTML y las variables dinámicas del mensaje.
        /// </summary>
        public Guid? PlantillaEmailId { get; set; }

        /// <summary>Asunto del email. Puede sobrescribir el asunto de la plantilla.</summary>
        public string? AsuntoEmail { get; set; }

        /// <summary>Estado actual del ciclo de vida de la campaña.</summary>
        public EstadoCampanaComercial Estado { get; set; } = EstadoCampanaComercial.Borrador;

        // ── Métricas de la campaña ───────────────────────────────────────────

        /// <summary>Número total de clientes incluidos en el envío de la campaña.</summary>
        public int ClientesIncluidos { get; set; } = 0;

        /// <summary>Número de emails enviados exitosamente.</summary>
        public int EmailsEnviados { get; set; } = 0;

        /// <summary>Número de clientes que abrieron el email de la campaña.</summary>
        public int EmailsAbiertos { get; set; } = 0;

        /// <summary>
        /// Número de conversiones (clientes que contrataron un servicio como resultado
        /// directo de la campaña). Actualizado manualmente por el equipo comercial.
        /// </summary>
        public int Conversiones { get; set; } = 0;

        /// <summary>
        /// Tasa de apertura calculada: EmailsAbiertos / EmailsEnviados × 100.
        /// Actualizado automáticamente al registrar aperturas en ContactoCampana.
        /// </summary>
        public decimal TasaApertura { get; set; } = 0m;

        /// <summary>
        /// Tasa de conversión calculada: Conversiones / ClientesIncluidos × 100.
        /// </summary>
        public decimal TasaConversion { get; set; } = 0m;

        /// <summary>
        /// FK hacia el empleado responsable de gestionar y analizar esta campaña.
        /// </summary>
        public Guid? ResponsableId { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Registros individuales de contacto por cliente en esta campaña.</summary>
        public virtual ICollection<ContactoCampana> Contactos { get; set; }
            = new List<ContactoCampana>();
    }
}
