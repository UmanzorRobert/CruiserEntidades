using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>Tipo de incidencia registrada durante o después de un servicio de limpieza.</summary>
    public enum TipoIncidenciaServicio
    {
        /// <summary>Daño accidental a bienes del cliente durante el servicio.</summary>
        DanioInstalaciones = 1,
        /// <summary>Accidente laboral del empleado durante el servicio.</summary>
        AccidenteLaboral = 2,
        /// <summary>Problema de acceso a las instalaciones del cliente.</summary>
        ProblemaAcceso = 3,
        /// <summary>Reclamación del cliente por calidad insuficiente del servicio.</summary>
        ReclamacionCliente = 4,
        /// <summary>Robo o pérdida de material de trabajo durante el servicio.</summary>
        RoboMaterial = 5,
        /// <summary>Incidencia con el vehículo de empresa durante el desplazamiento.</summary>
        IncidenciaVehiculo = 6,
        /// <summary>Conducta inadecuada del empleado comunicada por el cliente.</summary>
        ConductaEmpleado = 7,
        /// <summary>Otro tipo de incidencia no categorizada.</summary>
        Otro = 8
    }
}
