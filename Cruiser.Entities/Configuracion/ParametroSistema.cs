using System;
using System.Collections.Generic;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Parámetro de configuración del sistema editable desde la interfaz de administración.
    /// Permite ajustar comportamientos del sistema sin necesidad de modificar código ni
    /// redeployar la aplicación en Railway.
    ///
    /// Cada parámetro tiene un tipo de dato que controla la validación y el widget de edición.
    /// Los parámetros se agrupan por categoría funcional (Clave) para facilitar su gestión.
    /// Todos los cambios se auditan automáticamente en AuditoriaParametroSistema.
    ///
    /// SEED INICIAL: parámetros de seguridad, stock, facturación, email, RRHH y PWA.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Clave).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.Valor).IsRequired().HasMaxLength(2000);
    ///   builder.Property(x => x.Grupo).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.Descripcion).HasMaxLength(500);
    ///   builder.HasIndex(x => x.Clave).IsUnique();
    ///   builder.HasIndex(x => x.Grupo);
    /// </remarks>
    public class ParametroSistema : EntidadBase
    {
        /// <summary>
        /// Clave única del parámetro en formato SCREAMING_SNAKE_CASE.
        /// Es el identificador que usa el código para leer el parámetro.
        /// Ejemplo: "MAX_INTENTOS_LOGIN", "DIAS_VENCIMIENTO_FACTURA", "STOCK_MINIMO_DEFAULT".
        /// </summary>
        public string Clave { get; set; } = string.Empty;

        /// <summary>
        /// Valor actual del parámetro almacenado como string.
        /// Se deserializa al tipo correcto según el campo Tipo al leerlo.
        /// Ejemplo: "5", "true", "21.00", "Europe/Madrid", "[\"es-ES\",\"en-US\"]".
        /// </summary>
        public string Valor { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de dato del valor para validación y presentación en la UI.
        /// </summary>
        public TipoParametro Tipo { get; set; } = TipoParametro.String;

        /// <summary>
        /// Grupo funcional al que pertenece el parámetro.
        /// Ejemplos: "Seguridad", "Stock", "Facturación", "Email", "RRHH", "PWA", "Sistema".
        /// Los parámetros se muestran agrupados en la pantalla de administración.
        /// </summary>
        public string Grupo { get; set; } = string.Empty;

        /// <summary>
        /// Descripción clara del parámetro, su propósito y los valores válidos.
        /// Se muestra como tooltip/ayuda en el formulario de edición de la UI.
        /// </summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Indica si el parámetro puede ser editado desde la interfaz de administración.
        /// Los parámetros con EsEditable=false son de solo lectura (internos del sistema).
        /// </summary>
        public bool EsEditable { get; set; } = true;

        /// <summary>
        /// Indica si el parámetro es visible en la interfaz de administración.
        /// Los parámetros con EsVisible=false son internos y solo accesibles por código.
        /// </summary>
        public bool EsVisible { get; set; } = true;

        /// <summary>
        /// Posición de ordenación dentro del grupo para mostrar en la UI.
        /// Los parámetros con menor Orden aparecen primero en la lista del grupo.
        /// </summary>
        public int Orden { get; set; } = 0;

        /// <summary>
        /// Valor por defecto del parámetro. Permite restaurar el valor original
        /// desde la interfaz de administración con un botón "Restaurar por defecto".
        /// </summary>
        public string? ValorPorDefecto { get; set; }

        /// <summary>
        /// Expresión regular o rango de valores válidos para este parámetro.
        /// Se usa en la validación del formulario de edición en la UI.
        /// Ejemplo: "^[1-9][0-9]*$" para enteros positivos, "^[0-9]{1,3}$" para 0-999.
        /// </summary>
        public string? ReglaValidacion { get; set; }

        /// <summary>
        /// Indica si cambiar este parámetro requiere reiniciar la aplicación para tomar efecto.
        /// Se muestra una advertencia al usuario al guardar parámetros con RequiereReinicio=true.
        /// </summary>
        public bool RequiereReinicio { get; set; } = false;

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Historial de cambios auditados de este parámetro.</summary>
        public virtual ICollection<Auditoria.AuditoriaParametroSistema> HistorialAuditoria { get; set; }
            = new List<Auditoria.AuditoriaParametroSistema>();
    }
}
