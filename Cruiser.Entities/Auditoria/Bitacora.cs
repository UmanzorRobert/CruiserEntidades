using Cruiser.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Auditoria
{
    /// <summary>
    /// Bitácora general de actividad del sistema. Registra todas las acciones relevantes
    /// realizadas por usuarios: accesos, operaciones CRUD, eventos de seguridad,
    /// operaciones GDPR, cambios de configuración, etc.
    ///
    /// Los campos ValoresAnteriores y ValoresNuevos se almacenan como JSONB en PostgreSQL
    /// para permitir búsquedas nativas con índices GIN sobre el contenido JSON.
    ///
    /// NO hereda de EntidadBase: es un registro append-only e inmutable por diseño.
    /// Auditarla generaría un bucle de auditoría sin fin.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.HasKey(x => x.Id);
    ///   builder.Property(x => x.NombreUsuario).HasMaxLength(256);
    ///   builder.Property(x => x.TipoAccion).IsRequired().HasMaxLength(100);
    ///   builder.Property(x => x.TablaAfectada).HasMaxLength(100);
    ///   builder.Property(x => x.RegistroId).HasMaxLength(36);
    ///   builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(2000);
    ///   builder.Property(x => x.ValoresAnteriores).HasColumnType("jsonb");
    ///   builder.Property(x => x.ValoresNuevos).HasColumnType("jsonb");
    ///   builder.Property(x => x.DireccionIP).HasMaxLength(45);
    ///   builder.Property(x => x.UserAgent).HasMaxLength(500);
    ///   builder.Property(x => x.Modulo).HasMaxLength(100);
    ///
    ///   Índices:
    ///   builder.HasIndex(x => x.UsuarioId);
    ///   builder.HasIndex(x => x.FechaAccion);
    ///   builder.HasIndex(x => x.Nivel);
    ///   builder.HasIndex(x => new { x.TablaAfectada, x.RegistroId });
    ///   builder.HasIndex(x => x.ValoresAnteriores).HasMethod("gin");  // índice GIN para búsqueda JSON
    ///   builder.HasIndex(x => x.ValoresNuevos).HasMethod("gin");
    /// </remarks>
    public class Bitacora
    {
        /// <summary>Identificador único del registro de bitácora.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Identificador del usuario que realizó la acción.
        /// Nulo para operaciones de sistema (Hangfire jobs, Service Worker, etc.).
        /// No se define FK explícita para preservar el historial si el usuario es eliminado/anonimizado.
        /// </summary>
        public Guid? UsuarioId { get; set; }

        /// <summary>
        /// Nombre de usuario en el momento de la acción.
        /// Se almacena denormalizado para que el historial sea legible aunque
        /// el usuario sea anonimizado posteriormente por GDPR.
        /// Puede contener "SISTEMA", "HANGFIRE" u otro identificador no personal.
        /// </summary>
        public string NombreUsuario { get; set; } = string.Empty;

        /// <summary>
        /// Tipo o categoría de la acción realizada.
        /// Ejemplos: "LOGIN", "LOGOUT", "CREATE_FACTURA", "DELETE_CLIENTE",
        /// "GDPR_ANONIMIZACION", "CAMBIO_CONTRASENA", "BLOQUEO_CUENTA".
        /// </summary>
        public string TipoAccion { get; set; } = string.Empty;

        /// <summary>
        /// Nombre de la tabla/entidad afectada por la acción.
        /// Nulo para acciones que no afectan directamente a una entidad (ej. LOGIN).
        /// </summary>
        public string? TablaAfectada { get; set; }

        /// <summary>
        /// Identificador del registro específico afectado.
        /// Nulo para acciones globales o de autenticación.
        /// </summary>
        public string? RegistroId { get; set; }

        /// <summary>
        /// Descripción legible de la acción realizada.
        /// Debe ser suficientemente descriptiva para entenderse sin contexto adicional.
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Estado completo del registro ANTES de la acción, serializado como JSON.
        /// Se almacena en columna JSONB de PostgreSQL para búsquedas nativas con GIN.
        /// Nulo para operaciones de solo lectura o inserciones nuevas.
        /// </summary>
        public string? ValoresAnteriores { get; set; }

        /// <summary>
        /// Estado completo del registro DESPUÉS de la acción, serializado como JSON.
        /// Se almacena en columna JSONB de PostgreSQL para búsquedas nativas con GIN.
        /// Nulo para operaciones de eliminación.
        /// </summary>
        public string? ValoresNuevos { get; set; }

        /// <summary>
        /// Dirección IP del cliente que originó la acción.
        /// Soporta formato IPv4 e IPv6 (máximo 45 caracteres).
        /// Puede ser IP de Railway/proxy en entorno de producción.
        /// </summary>
        public string? DireccionIP { get; set; }

        /// <summary>
        /// Cadena User-Agent del navegador o cliente HTTP que realizó la acción.
        /// Útil para identificar el tipo de dispositivo y detectar bots o accesos automatizados.
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// Módulo funcional del sistema desde el que se originó la acción.
        /// Ejemplos: "Autenticacion", "Inventario", "Facturacion", "GDPR", "Hangfire".
        /// Permite filtrar la bitácora por área funcional.
        /// </summary>
        public string? Modulo { get; set; }

        /// <summary>
        /// Nivel de severidad o importancia de la entrada en la bitácora.
        /// Permite filtrar por criticidad: seguridad, errores, GDPR, etc.
        /// </summary>
        public NivelBitacora Nivel { get; set; } = NivelBitacora.Informacion;

        /// <summary>
        /// Fecha y hora UTC exacta en que se produjo la acción.
        /// Se indexa para búsquedas y filtros por rango de fechas.
        /// </summary>
        public DateTime FechaAccion { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Identificador de correlación de la petición HTTP.
        /// Generado por ASP.NET Core o Serilog para trazar una operación completa
        /// a través de múltiples entradas de log relacionadas.
        /// </summary>
        public string? TraceId { get; set; }

        /// <summary>
        /// Duración en milisegundos de la operación registrada.
        /// Útil para detectar operaciones lentas o cuellos de botella.
        /// Nulo para eventos que no tienen duración medible (ej. LOGIN).
        /// </summary>
        public int? DuracionMs { get; set; }
    }
}
