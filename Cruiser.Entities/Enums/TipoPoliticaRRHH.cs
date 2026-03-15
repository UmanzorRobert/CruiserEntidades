using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>Tipo de política de RRHH que establece las reglas de gestión del personal.</summary>
    public enum TipoPoliticaRRHH
    {
        /// <summary>Política de acumulación y disfrute de vacaciones anuales.</summary>
        Vacaciones = 1,
        /// <summary>Política de autorización y compensación de horas extra.</summary>
        HorasExtra = 2,
        /// <summary>Política de gestión y justificación de ausencias.</summary>
        Ausencias = 3,
        /// <summary>Política de cálculo y pago de comisiones por ventas y contratos.</summary>
        Comisiones = 4,
        /// <summary>Política disciplinaria: faltas, sanciones y procedimientos.</summary>
        Disciplinaria = 5,
        /// <summary>Política de teletrabajo y trabajo en remoto.</summary>
        Teletrabajo = 6,
        /// <summary>Política de uso de EPIs y prevención de riesgos laborales.</summary>
        PRL = 7
    }
}
