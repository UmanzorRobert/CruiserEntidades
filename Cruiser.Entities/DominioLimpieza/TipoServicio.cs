using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.DominioLimpieza
{
    /// <summary>
    /// Catálogo de tipos de servicio de limpieza ofrecidos por la empresa.
    /// Define la duración estimada, el precio base, el número de empleados
    /// necesario y el impuesto aplicable.
    ///
    /// Cada TipoServicio puede tener uno o varios ProtocoloServicio asociados
    /// que detallan los pasos técnicos de ejecución y los materiales necesarios.
    ///
    /// SEED: LimpiezaGeneral, LimpiezaFondo, LimpiezaPostObra, LimpiezaCristaleras,
    ///       LimpiezaIndustrial, LimpiezaHogar, MantenimientoDiario, LimpiezaComunidades,
    ///       LimpiezaHospitalaria, LimpiezaCatering.
    ///
    /// El Icono y Color permiten diferenciar visualmente los tipos de servicio
    /// en el calendario FullCalendar y en el módulo de planificación.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(50);
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150);
    ///   builder.Property(x => x.PrecioBase).HasPrecision(18, 2);
    ///   builder.Property(x => x.Color).HasMaxLength(7);
    ///   builder.Property(x => x.Icono).HasMaxLength(100);
    ///   builder.HasIndex(x => x.Codigo).IsUnique();
    /// </remarks>
    public class TipoServicio : EntidadBase
    {
        /// <summary>Código único del tipo de servicio. Ejemplo: "LIMP_GENERAL", "LIMP_FONDO".</summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre descriptivo del tipo de servicio para la interfaz y los documentos.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción detallada del servicio y las tareas que incluye.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Duración estándar estimada del servicio en minutos.
        /// Usada como valor predeterminado al crear EventoCalendario y OrdenServicio.
        /// </summary>
        public int DuracionEstimadaMinutos { get; set; } = 120;

        /// <summary>
        /// Precio base del servicio sin IVA.
        /// Puede ser sobreescrito por la ListaPrecio del cliente o del segmento.
        /// </summary>
        public decimal PrecioBase { get; set; } = 0m;

        /// <summary>FK hacia el impuesto aplicable a este tipo de servicio (IVA 21%, IGIC 7%, etc.).</summary>
        public Guid? ImpuestoId { get; set; }

        /// <summary>Número mínimo de empleados necesarios para ejecutar el servicio.</summary>
        public int NumeroMinimoEmpleados { get; set; } = 1;

        /// <summary>Número máximo de empleados recomendado para ejecutar el servicio.</summary>
        public int NumeroMaximoEmpleados { get; set; } = 4;

        /// <summary>Clase CSS del icono Font Awesome para identificar visualmente el tipo.</summary>
        public string? Icono { get; set; }

        /// <summary>Color hexadecimal (#RRGGBB) del tipo de servicio en el calendario y las listas.</summary>
        public string? Color { get; set; }

        /// <summary>
        /// Indica si el tipo de servicio requiere supervisor in situ durante la ejecución.
        /// True para servicios de alta complejidad o con riesgo (productos químicos especiales).
        /// </summary>
        public bool RequiereSupervisor { get; set; } = false;

        /// <summary>Indica si este tipo de servicio está activo y disponible para contratar.</summary>
        public bool EstaActivo { get; set; } = true;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Protocolos de ejecución disponibles para este tipo de servicio.</summary>
        public virtual ICollection<ProtocoloServicio> Protocolos { get; set; }
            = new List<ProtocoloServicio>();

        /// <summary>Plantillas de checklist vinculadas a este tipo de servicio.</summary>
        public virtual ICollection<PlantillaChecklist> PlantillasChecklist { get; set; }
            = new List<PlantillaChecklist>();

        /// <summary>Contratos activos que incluyen este tipo de servicio.</summary>
        public virtual ICollection<ContratoServicio> Contratos { get; set; }
            = new List<ContratoServicio>();
    }
}