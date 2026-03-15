using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.RRHH
{
    /// <summary>
    /// Nómina mensual de un empleado. Almacena los conceptos salariales principales:
    /// salario bruto, deducciones (IRPF, Seguridad Social) y salario neto a percibir.
    ///
    /// La generación automática de nóminas se realiza mediante el job de Hangfire
    /// a inicio de cada mes para todos los empleados activos, en estado Borrador.
    ///
    /// Tras la revisión y cierre (Estado = Cerrada), se genera el PDF descargable
    /// con QuestPDF siguiendo el formato oficial de nómina español
    /// (Art. 29 ET y RD 2064/1995).
    ///
    /// INTEGRACIÓN CON COMISIONES: Si el empleado tiene una LiquidacionComision
    /// aprobada para el mismo período, su importe se incluye en los conceptos salariales.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Periodo).IsRequired().HasMaxLength(7);
    ///   builder.Property(x => x.SalarioBruto).HasPrecision(18, 2);
    ///   builder.Property(x => x.TotalDeducciones).HasPrecision(18, 2);
    ///   builder.Property(x => x.SalarioNeto).HasPrecision(18, 2);
    ///   builder.Property(x => x.IRPF).HasPrecision(5, 2);
    ///   builder.HasIndex(x => new { x.EmpleadoId, x.Periodo }).IsUnique();
    ///   builder.HasIndex(x => x.Estado);
    /// </remarks>
    public class NominaEmpleado : EntidadBase
    {
        /// <summary>FK hacia el empleado al que pertenece esta nómina.</summary>
        public Guid EmpleadoId { get; set; }

        /// <summary>Período de la nómina en formato "YYYY-MM". Ejemplo: "2026-03".</summary>
        public string Periodo { get; set; } = string.Empty;

        // ── Conceptos retributivos ────────────────────────────────────────────

        /// <summary>Salario base mensual según el contrato de trabajo.</summary>
        public decimal SalarioBase { get; set; }

        /// <summary>Plus de transporte, dietas u otros complementos salariales del período.</summary>
        public decimal ComplementosSalariales { get; set; } = 0m;

        /// <summary>Importe de horas extra realizadas en el período.</summary>
        public decimal ImporteHorasExtra { get; set; } = 0m;

        /// <summary>Comisiones aprobadas incluidas en esta nómina.</summary>
        public decimal ImporteComisiones { get; set; } = 0m;

        /// <summary>
        /// Salario bruto total: SalarioBase + Complementos + HorasExtra + Comisiones.
        /// </summary>
        public decimal SalarioBruto { get; set; }

        // ── Deducciones ──────────────────────────────────────────────────────

        /// <summary>
        /// Porcentaje de retención del IRPF aplicado al empleado.
        /// Calculado por el sistema según el salario anual y la situación familiar.
        /// </summary>
        public decimal IRPF { get; set; } = 0m;

        /// <summary>Importe retenido de IRPF: SalarioBruto × IRPF / 100.</summary>
        public decimal ImporteIRPF { get; set; } = 0m;

        /// <summary>Cuota del empleado a la Seguridad Social (contingencias comunes + desempleo + FP).</summary>
        public decimal CuotaSeguridadSocial { get; set; } = 0m;

        /// <summary>Otras deducciones (anticipos, préstamos, sanciones económicas).</summary>
        public decimal OtrasDeducciones { get; set; } = 0m;

        /// <summary>Total deducciones: ImporteIRPF + CuotaSeguridadSocial + OtrasDeducciones.</summary>
        public decimal TotalDeducciones { get; set; }

        /// <summary>Salario neto a percibir: SalarioBruto - TotalDeducciones.</summary>
        public decimal SalarioNeto { get; set; }

        // ── Trabajo realizado ────────────────────────────────────────────────

        /// <summary>Número de días laborables trabajados en el período.</summary>
        public int DiasLaborados { get; set; } = 0;

        /// <summary>Número de horas extra realizadas en el período.</summary>
        public decimal HorasExtras { get; set; } = 0m;

        /// <summary>Número de ausencias en días naturales del período.</summary>
        public int DiasAusencia { get; set; } = 0;

        // ── Estado y documento ───────────────────────────────────────────────

        /// <summary>Estado de la nómina: Borrador, Cerrada o Pagada.</summary>
        public EstadoNomina Estado { get; set; } = EstadoNomina.Borrador;

        /// <summary>Fecha de pago de la nómina al empleado.</summary>
        public DateOnly? FechaPago { get; set; }

        /// <summary>Ruta relativa del PDF de la nómina generado con QuestPDF.</summary>
        public string? RutaDocumento { get; set; }

        /// <summary>FK hacia el usuario de RRHH que cerró y validó la nómina.</summary>
        public Guid? CerradaPorId { get; set; }

        /// <summary>Notas sobre conceptos especiales o particularidades de esta nómina.</summary>
        public string? Notas { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Empleado al que pertenece esta nómina.</summary>
        public virtual Comercial.Empleado? Empleado { get; set; }
    }
}
