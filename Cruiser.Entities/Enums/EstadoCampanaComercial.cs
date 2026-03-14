using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Estado del ciclo de vida de una campaña comercial de marketing o fidelización.
    /// </summary>
    public enum EstadoCampanaComercial
    {
        /// <summary>Campaña en preparación. Clientes aún no seleccionados ni emails enviados.</summary>
        Borrador = 1,
        /// <summary>Campaña programada para envío futuro. Configuración completada.</summary>
        Programada = 2,
        /// <summary>Campaña activa. Envíos en curso o realizados. Aceptando respuestas.</summary>
        Activa = 3,
        /// <summary>Campaña finalizada. Fecha fin superada. Métricas disponibles para análisis.</summary>
        Finalizada = 4,
        /// <summary>Campaña pausada manualmente antes de finalizar.</summary>
        Pausada = 5,
        /// <summary>Campaña cancelada antes de iniciarse.</summary>
        Cancelada = 6
    }
}
