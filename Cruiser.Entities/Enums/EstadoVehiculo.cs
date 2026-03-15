using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>Estado operativo actual de un vehículo de la flota.</summary>
    public enum EstadoVehiculo
    {
        /// <summary>Vehículo operativo y disponible para asignación.</summary>
        Disponible = 1,
        /// <summary>Vehículo asignado a un equipo o ruta en curso.</summary>
        EnUso = 2,
        /// <summary>Vehículo en taller para mantenimiento preventivo o correctivo.</summary>
        EnMantenimiento = 3,
        /// <summary>Vehículo inmovilizado por avería grave. Fuera de servicio temporal.</summary>
        Averiado = 4,
        /// <summary>Vehículo dado de baja definitiva de la flota.</summary>
        DadoDeBaja = 5,
        /// <summary>Vehículo inmovilizado por sanción, embargo o retirada de circulación.</summary>
        Inmovilizado = 6
    }
}
