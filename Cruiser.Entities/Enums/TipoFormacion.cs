using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de formación o certificación laboral del empleado.
    /// Las formaciones obligatorias (EsObligatoria=true) bloquean la asignación
    /// del empleado a órdenes de servicio si están caducadas.
    /// </summary>
    public enum TipoFormacion
    {
        /// <summary>Prevención de Riesgos Laborales (PRL). Obligatoria por ley.</summary>
        PRL = 1,
        /// <summary>Manipulación de productos químicos y biocidas. Carné oficial requerido.</summary>
        ProductosQuimicos = 2,
        /// <summary>Formación en uso y mantenimiento de maquinaria de limpieza.</summary>
        Maquinaria = 3,
        /// <summary>Primeros auxilios y RCP. Recomendable para personal de campo.</summary>
        PrimerosAuxilios = 4,
        /// <summary>Protección de datos (GDPR/RGPD). Obligatoria por política interna.</summary>
        ProteccionDatos = 5,
        /// <summary>Carné de conducir o autorización para conducir vehículos de empresa.</summary>
        Conduccion = 6,
        /// <summary>Técnicas de limpieza especializadas (salas blancas, hospitales, industria).</summary>
        LimpiezaEspecializada = 7,
        /// <summary>Formación interna de la empresa (procedimientos, protocolos, valores).</summary>
        FormacionInterna = 8,
        /// <summary>Titulación universitaria o FP reglada relacionada con el puesto.</summary>
        TitulacionReglada = 9,
        /// <summary>Otro tipo de formación o certificado no categorizado.</summary>
        Otros = 10
    }
}
