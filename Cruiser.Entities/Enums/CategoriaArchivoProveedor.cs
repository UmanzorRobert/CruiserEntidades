using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>Categoría del archivo adjunto a un proveedor.</summary>
    public enum CategoriaArchivoProveedor
    {
        /// <summary>Certificado de homologación del proveedor.</summary>
        Homologacion = 1,
        /// <summary>Certificado de calidad ISO 9001 u otros estándares.</summary>
        CertificadoISO = 2,
        /// <summary>Póliza de seguro de responsabilidad civil vigente.</summary>
        Seguro = 3,
        /// <summary>Ficha de datos de seguridad de un producto químico suministrado.</summary>
        FichaSeguridad = 4,
        /// <summary>Contrato de suministro o acuerdo comercial firmado.</summary>
        Contrato = 5,
        /// <summary>Catálogo de productos del proveedor.</summary>
        Catalogo = 6,
        /// <summary>Otros documentos del proveedor.</summary>
        Otros = 7
    }
}
