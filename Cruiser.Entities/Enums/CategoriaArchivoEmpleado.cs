using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>Categoría del archivo adjunto a un empleado.</summary>
    public enum CategoriaArchivoEmpleado
    {
        /// <summary>Contrato de trabajo firmado.</summary>
        Contrato = 1,
        /// <summary>Certificado de formación o titulación profesional.</summary>
        Certificado = 2,
        /// <summary>Nómina mensual.</summary>
        Nomina = 3,
        /// <summary>Documento de identificación (DNI, NIE, Pasaporte).</summary>
        Identificacion = 4,
        /// <summary>Partes de alta/baja médica por enfermedad.</summary>
        BajaMedica = 5,
        /// <summary>Documento relacionado con la Seguridad Social (vida laboral, TC2, etc.).</summary>
        SeguridadSocial = 6,
        /// <summary>Evaluación de riesgos laborales y reconocimientos médicos.</summary>
        PRL = 7,
        /// <summary>Otros documentos no clasificados en las categorías anteriores.</summary>
        Otros = 8
    }
}
