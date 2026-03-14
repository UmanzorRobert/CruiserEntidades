using System;
using Cruiser.Entities.Base;
using Cruiser.Entities.Enums;

namespace Cruiser.Entities.Geografia
{
    /// <summary>
    /// Dirección postal completa reutilizable y polimórfica.
    /// Puede estar asociada a Clientes, Empleados, Proveedores o ser
    /// la dirección de prestación de un servicio de limpieza.
    ///
    /// El polimorfismo se implementa mediante campos TipoEntidadPropietaria y
    /// EntidadPropietariaId (referencia genérica sin FK explícita en BD).
    /// Esto permite que cualquier entidad del sistema tenga múltiples direcciones
    /// sin crear tablas de dirección específicas por entidad.
    ///
    /// Las coordenadas GPS (Latitud/Longitud) se capturan opcionalmente
    /// mediante geocodificación (Google Maps API) o entrada manual.
    /// Son usadas por Leaflet.js en el mapa de zonas de servicio y por
    /// el módulo de fichaje para validar la proximidad del empleado.
    /// </summary>
    /// <remarks>
    /// Fluent API:
    ///   builder.Property(x => x.Calle).IsRequired().HasMaxLength(300);
    ///   builder.Property(x => x.NumeroExterior).HasMaxLength(20);
    ///   builder.Property(x => x.NumeroInterior).HasMaxLength(20);
    ///   builder.Property(x => x.Alias).HasMaxLength(100);
    ///   builder.Property(x => x.Latitud).HasPrecision(9, 6);
    ///   builder.Property(x => x.Longitud).HasPrecision(9, 6);
    ///   builder.Property(x => x.TipoEntidadPropietaria).HasMaxLength(100);
    ///   builder.Property(x => x.InstruccionesAcceso).HasMaxLength(1000);
    ///   builder.HasIndex(x => new { x.TipoEntidadPropietaria, x.EntidadPropietariaId });
    ///   builder.HasIndex(x => new { x.EntidadPropietariaId, x.EsPrincipal });
    ///
    ///   Relaciones geográficas:
    ///   builder.HasOne(d => d.CodigoPostal).WithMany(cp => cp.Direcciones)
    ///          .HasForeignKey(d => d.CodigoPostalId).OnDelete(DeleteBehavior.Restrict);
    ///   builder.HasOne(d => d.Ciudad).WithMany()
    ///          .HasForeignKey(d => d.CiudadId).OnDelete(DeleteBehavior.Restrict);
    ///   builder.HasOne(d => d.Provincia).WithMany()
    ///          .HasForeignKey(d => d.ProvinciaId).OnDelete(DeleteBehavior.Restrict);
    ///   builder.HasOne(d => d.Pais).WithMany()
    ///          .HasForeignKey(d => d.PaisId).OnDelete(DeleteBehavior.Restrict);
    /// </remarks>
    public class DireccionCompleta : EntidadBase
    {
        // ── Polimorfismo (propietario de la dirección) ───────────────────────

        /// <summary>
        /// Nombre de la entidad propietaria de esta dirección.
        /// Ejemplos: "Cliente", "Empleado", "Proveedor", "Empresa".
        /// Junto con EntidadPropietariaId forma la referencia polimórfica.
        /// </summary>
        public string TipoEntidadPropietaria { get; set; } = string.Empty;

        /// <summary>
        /// Identificador GUID de la entidad específica propietaria de esta dirección.
        /// No se define FK explícita en BD (referencia polimórfica).
        /// Ejemplo: el Guid del Cliente concreto al que pertenece esta dirección.
        /// </summary>
        public Guid EntidadPropietariaId { get; set; }

        // ── Datos de la dirección ────────────────────────────────────────────

        /// <summary>
        /// Calle, avenida, plaza o vía de la dirección.
        /// Ejemplo: "Calle Gran Vía", "Avenida de la Constitución", "Plaza Mayor".
        /// </summary>
        public string Calle { get; set; } = string.Empty;

        /// <summary>
        /// Número exterior del inmueble en la calle.
        /// Puede incluir bis, duplicado, etc. Ejemplo: "15", "23 Bis", "S/N".
        /// </summary>
        public string? NumeroExterior { get; set; }

        /// <summary>
        /// Número o letra del piso, portal, escalera o departamento interior.
        /// Ejemplo: "3ºB", "Local 4", "Nave 12", "Portal A, 2ºC".
        /// </summary>
        public string? NumeroInterior { get; set; }

        /// <summary>
        /// Información adicional de la dirección para facilitar la localización.
        /// Ejemplo: "Edificio Torre Norte", "Junto al Corte Inglés", "Km 15 carretera N-IV".
        /// </summary>
        public string? InformacionAdicional { get; set; }

        // ── Referencias geográficas ──────────────────────────────────────────

        /// <summary>
        /// FK hacia el código postal de la dirección.
        /// Determina la ciudad, provincia y país mediante la jerarquía geográfica.
        /// </summary>
        public Guid? CodigoPostalId { get; set; }

        /// <summary>
        /// FK directa hacia la ciudad, independiente del código postal.
        /// Permite búsquedas y filtros eficientes por ciudad sin join a CodigoPostal.
        /// </summary>
        public Guid CiudadId { get; set; }

        /// <summary>
        /// FK directa hacia la provincia, independiente del código postal.
        /// Permite búsquedas y filtros eficientes por provincia.
        /// </summary>
        public Guid ProvinciaId { get; set; }

        /// <summary>
        /// FK directa hacia el país, independiente del código postal.
        /// Permite búsquedas y filtros eficientes por país.
        /// </summary>
        public Guid PaisId { get; set; }

        // ── Coordenadas GPS ──────────────────────────────────────────────────

        /// <summary>
        /// Latitud exacta de la dirección en grados decimales WGS84.
        /// Obtenida mediante geocodificación de la dirección postal (Google Maps API)
        /// o introducida manualmente. Usada por Leaflet.js y el módulo de fichaje GPS.
        /// </summary>
        public decimal? Latitud { get; set; }

        /// <summary>
        /// Longitud exacta de la dirección en grados decimales WGS84.
        /// </summary>
        public decimal? Longitud { get; set; }

        /// <summary>
        /// Precisión en metros de las coordenadas GPS almacenadas.
        /// Permite conocer la fiabilidad de las coordenadas para el módulo de fichaje.
        /// </summary>
        public decimal? PrecisionMetros { get; set; }

        // ── Clasificación y uso ──────────────────────────────────────────────

        /// <summary>
        /// Tipo o propósito de esta dirección para la entidad propietaria.
        /// </summary>
        public TipoDireccion TipoDireccion { get; set; } = TipoDireccion.Fiscal;

        /// <summary>
        /// Indica si esta es la dirección principal de la entidad propietaria.
        /// Solo puede haber una dirección principal por entidad y tipo.
        /// </summary>
        public bool EsPrincipal { get; set; } = false;

        /// <summary>
        /// Nombre descriptivo o alias de la dirección para identificarla rápidamente.
        /// Ejemplo: "Sede principal", "Almacén Sevilla", "Casa del cliente".
        /// </summary>
        public string? Alias { get; set; }

        /// <summary>
        /// Instrucciones específicas de acceso al inmueble para los empleados en campo.
        /// Ejemplo: "Llamar al interfono 3B", "Acceso por puerta trasera", "Código portón: 1234".
        /// </summary>
        public string? InstruccionesAcceso { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────

        /// <summary>Código postal de esta dirección con sus parámetros logísticos.</summary>
        public virtual CodigoPostal? CodigoPostal { get; set; }

        /// <summary>Ciudad de esta dirección.</summary>
        public virtual Ciudad? Ciudad { get; set; }

        /// <summary>Provincia de esta dirección.</summary>
        public virtual Provincia? Provincia { get; set; }

        /// <summary>País de esta dirección.</summary>
        public virtual Pais? Pais { get; set; }
    }
}
