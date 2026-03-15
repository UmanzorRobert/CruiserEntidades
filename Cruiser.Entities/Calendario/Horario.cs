using System.Collections.Generic;
using Cruiser.Entities.Base;

namespace Cruiser.Entities.Calendario
{
    /// <summary>
    /// Plantilla de horario laboral semanal reutilizable que define las franjas horarias
    /// de trabajo para los 7 días de la semana.
    ///
    /// Soporta jornada partida (mañana + tarde con pausa al mediodía) y jornada continua.
    /// Los días no laborables tienen los campos de hora en null.
    ///
    /// El HorarioEmpleado asigna una plantilla de Horario a cada empleado durante
    /// un período de tiempo, permitiendo cambios de horario con historial completo.
    ///
    /// SEED: Horario Jornada Completa (L-V 9:00-14:00 / 15:00-18:00),
    ///       Horario Media Jornada (L-V 9:00-13:00).
    ///
    /// Nota: La zona horaria (ZonaHoraria) es crítica para empleados en Canarias
    /// que trabajan en horario UTC-1 respecto a la península.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150);
    ///   builder.Property(x => x.ZonaHoraria).HasMaxLength(100);
    ///   builder.HasIndex(x => x.EsPorDefecto).HasFilter("\"EsPorDefecto\" = true");
    /// </remarks>
    public class Horario : EntidadBase
    {
        /// <summary>Nombre descriptivo del horario para identificarlo en la gestión.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del horario y su aplicación en la empresa.</summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Zona horaria IANA del horario.
        /// Ejemplo: "Europe/Madrid", "Atlantic/Canary".
        /// Crítico para empleados en Canarias (UTC-1 en invierno, UTC+0 en verano).
        /// </summary>
        public string ZonaHoraria { get; set; } = "Europe/Madrid";

        /// <summary>
        /// Indica si este es el horario por defecto que se asigna automáticamente
        /// a los nuevos empleados al darlos de alta.
        /// </summary>
        public bool EsPorDefecto { get; set; } = false;

        // ── Lunes ────────────────────────────────────────────────────────────

        /// <summary>Indica si el lunes es día laborable en este horario.</summary>
        public bool LunesLaborable { get; set; } = true;
        /// <summary>Hora de inicio de la jornada de mañana del lunes. Formato HH:mm.</summary>
        public string? LunesMananaInicio { get; set; }
        /// <summary>Hora de fin de la jornada de mañana del lunes. Formato HH:mm.</summary>
        public string? LunesMananaFin { get; set; }
        /// <summary>Hora de inicio de la jornada de tarde del lunes. Null si jornada continua.</summary>
        public string? LunesTardeInicio { get; set; }
        /// <summary>Hora de fin de la jornada de tarde del lunes. Null si jornada continua.</summary>
        public string? LunesTardeFin { get; set; }

        // ── Martes ───────────────────────────────────────────────────────────

        /// <summary>Indica si el martes es día laborable.</summary>
        public bool MartesLaborable { get; set; } = true;
        public string? MartesMananaInicio { get; set; }
        public string? MartesMananaFin { get; set; }
        public string? MartesTardeInicio { get; set; }
        public string? MartesTardeFin { get; set; }

        // ── Miércoles ────────────────────────────────────────────────────────

        /// <summary>Indica si el miércoles es día laborable.</summary>
        public bool MiercolesLaborable { get; set; } = true;
        public string? MiercolesMananaInicio { get; set; }
        public string? MiercolesMananaFin { get; set; }
        public string? MiercolesTardeInicio { get; set; }
        public string? MiercolesTardeFin { get; set; }

        // ── Jueves ───────────────────────────────────────────────────────────

        /// <summary>Indica si el jueves es día laborable.</summary>
        public bool JuevesLaborable { get; set; } = true;
        public string? JuevesMananaInicio { get; set; }
        public string? JuevesMananaFin { get; set; }
        public string? JuevesTardeInicio { get; set; }
        public string? JuevesTardeFin { get; set; }

        // ── Viernes ──────────────────────────────────────────────────────────

        /// <summary>Indica si el viernes es día laborable.</summary>
        public bool ViernesLaborable { get; set; } = true;
        public string? ViernesMananaInicio { get; set; }
        public string? ViernesMananaFin { get; set; }
        public string? ViernesTardeInicio { get; set; }
        public string? ViernesTardeFin { get; set; }

        // ── Sábado ───────────────────────────────────────────────────────────

        /// <summary>Indica si el sábado es día laborable (servicios de limpieza en fin de semana).</summary>
        public bool SabadoLaborable { get; set; } = false;
        public string? SabadoMananaInicio { get; set; }
        public string? SabadoMananaFin { get; set; }
        public string? SabadoTardeInicio { get; set; }
        public string? SabadoTardeFin { get; set; }

        // ── Domingo ──────────────────────────────────────────────────────────

        /// <summary>Indica si el domingo es día laborable.</summary>
        public bool DomingoLaborable { get; set; } = false;
        public string? DomingoMananaInicio { get; set; }
        public string? DomingoMananaFin { get; set; }
        public string? DomingoTardeInicio { get; set; }
        public string? DomingoTardeFin { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Asignaciones de este horario a empleados.</summary>
        public virtual ICollection<HorarioEmpleado> HorariosEmpleado { get; set; }
            = new List<HorarioEmpleado>();
    }
}
