using Cruiser.Entities.Base;

namespace Cruiser.Entities.Configuracion
{
    /// <summary>
    /// Plantilla HTML de email configurable desde la interfaz de administración.
    /// Soporta variables dinámicas con la sintaxis {{NombreVariable}} que se reemplazan
    /// en el momento del envío con los valores reales de la entidad correspondiente.
    ///
    /// Permite gestionar plantillas en múltiples idiomas (una por idioma y código).
    /// El sistema selecciona automáticamente la plantilla en el idioma del destinatario.
    ///
    /// SEED: templates para login, recuperación de contraseña, nueva factura,
    /// recordatorio de cobro, confirmación de orden de servicio, alertas de stock.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Codigo).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.Asunto).IsRequired().HasMaxLength(200);
    ///   builder.Property(x => x.Idioma).HasMaxLength(10).HasDefaultValue("es-ES");
    ///   builder.Property(x => x.Categoria).HasMaxLength(100);
    ///   builder.HasIndex(x => new { x.Codigo, x.Idioma }).IsUnique();
    /// </remarks>
    public class PlantillaEmail : EntidadBase
    {
        /// <summary>
        /// Código único de la plantilla en formato SCREAMING_SNAKE_CASE.
        /// Ejemplo: "RECUPERACION_PASSWORD", "NUEVA_FACTURA", "RECORDATORIO_COBRO".
        /// Es el identificador que usa el código para cargar la plantilla correcta.
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>
        /// Asunto del email. Puede contener variables dinámicas.
        /// Ejemplo: "Factura {{NumeroFactura}} de {{NombreEmpresa}} - Importe: {{ImporteTotal}}€".
        /// </summary>
        public string Asunto { get; set; } = string.Empty;

        /// <summary>
        /// Contenido HTML completo del email.
        /// Debe ser responsive y compatible con clientes de email comunes (Gmail, Outlook).
        /// Puede contener variables {{NombreVariable}} que se reemplazan al enviar.
        /// </summary>
        public string ContenidoHTML { get; set; } = string.Empty;

        /// <summary>
        /// Idioma de esta versión de la plantilla en formato BCP-47.
        /// Ejemplo: "es-ES", "en-US", "ca-ES".
        /// </summary>
        public string Idioma { get; set; } = "es-ES";

        /// <summary>
        /// Categoría funcional de la plantilla para agrupar en la interfaz de administración.
        /// Ejemplos: "Seguridad", "Facturación", "RRHH", "Notificaciones", "Comercial", "GDPR".
        /// </summary>
        public string? Categoria { get; set; }

        /// <summary>
        /// Lista de variables disponibles para esta plantilla, separadas por comas.
        /// Documentación para el administrador que edita la plantilla desde la UI.
        /// Ejemplo: "{{NombreCliente}}, {{NumeroFactura}}, {{ImporteTotal}}, {{FechaVencimiento}}".
        /// </summary>
        public string? VariablesDisponibles { get; set; }

        /// <summary>
        /// Indica si esta plantilla está activa y puede ser utilizada para enviar emails.
        /// Las plantillas inactivas no se usan aunque su código sea referenciado.
        /// </summary>
        public bool EsActiva { get; set; } = true;

        /// <summary>
        /// Descripción del propósito de la plantilla y cuándo se utiliza.
        /// Ayuda al administrador a entender el contexto antes de editarla.
        /// </summary>
        public string? Descripcion { get; set; }
    }
}
