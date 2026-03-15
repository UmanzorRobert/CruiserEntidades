using System;
using System.Collections.Generic;
using System.Text;

namespace Cruiser.Entities.Enums
{
    /// <summary>
    /// Tipo de integración con un sistema externo.
    /// Determina el protocolo de comunicación, el formato de datos y
    /// las validaciones específicas aplicadas por IIntegracionService.
    /// </summary>
    public enum TipoIntegracion
    {
        /// <summary>
        /// Agencia Estatal de Administración Tributaria (AEAT).
        /// Envío de SII (Suministro Inmediato de Información) para VeriFactu.
        /// Endpoint: https://www7.aeat.es/wlpl/SSII-FACT/ws/fe/SiiFactFEV1SOAP
        /// </summary>
        AEAT = 1,
        /// <summary>
        /// Pasarela de pago Stripe para cobro online de facturas.
        /// REST API con autenticación Bearer Token.
        /// </summary>
        Stripe = 2,
        /// <summary>
        /// API de WhatsApp Business para notificaciones a clientes y empleados.
        /// Mensajes transaccionales: recordatorios de cita, confirmaciones de servicio.
        /// </summary>
        WhatsApp = 3,
        /// <summary>
        /// Google Maps Platform para geocodificación de direcciones y cálculo de rutas.
        /// API: Geocoding, Directions, Distance Matrix.
        /// </summary>
        GoogleMaps = 4,
        /// <summary>
        /// Sistema ERP externo del cliente para sincronización bidireccional de facturas y pedidos.
        /// Protocolo: REST o SOAP según el ERP destino.
        /// </summary>
        ERP = 5,
        /// <summary>
        /// Plataforma de envío masivo de SMS para notificaciones y recordatorios.
        /// Proveedores soportados: Twilio, Vonage, MásMóvil.
        /// </summary>
        SMS = 6,
        /// <summary>
        /// Pasarela de pago alternativa (Redsys, PayPal, Bizum).
        /// </summary>
        PasarelaPago = 7,
        /// <summary>
        /// Integración con software de contabilidad externo (Sage, Contaplus, A3).
        /// Exportación de asientos contables y datos fiscales.
        /// </summary>
        Contabilidad = 8,
        /// <summary>
        /// Sistema de Gestión de Recursos Humanos externo (A3Nom, Sage HR).
        /// Sincronización de nóminas y datos laborales.
        /// </summary>
        RRHH = 9,
        /// <summary>
        /// Plataforma de firma digital electrónica (AutoFirma, Signaturit, DocuSign).
        /// Para firmar contratos y documentos legales con certificado digital.
        /// </summary>
        FirmaDigital = 10,
        /// <summary>Otra integración no categorizada en los tipos anteriores.</summary>
        Otro = 99
    }
}
