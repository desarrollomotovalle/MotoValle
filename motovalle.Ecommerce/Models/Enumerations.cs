// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enumerations.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Enumerations
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models
{
    using System.ComponentModel.DataAnnotations;

    #region General Enums
    /// <summary>
    /// The Contact us type enumeration
    /// </summary>
    public enum ContactUsType
    {
        /// <summary>
        /// The quotation
        /// </summary>
        [Display(Name = "Cotización")]
        Quotation,

        /// <summary>
        /// The information
        /// </summary>
        [Display(Name = "Solicitud de información adicional de un vehículo o Maquinaria")]
        Infomation,

        /// <summary>
        /// The congratulation
        /// </summary>
        [Display(Name = "Felicitación")]
        Congratulation,

        /// <summary>
        /// The other
        /// </summary>
        [Display(Name = "Otro")]
        Other
    }

    /// <summary>
    /// Sort By enumeration
    /// </summary>
    public enum SortBy
    {
        /// <summary>
        /// The is featured
        /// </summary>
        [Display(Name = "Destacados")]
        IsFeatured,

        /// <summary>
        /// The minimum price
        /// </summary>
        [Display(Name = "Menor precio")]
        MinPrice,

        /// <summary>
        /// The maximum price
        /// </summary>
        [Display(Name = "Mayor precio")]
        MaxPrice
    }

    /// <summary>
    /// Records per Page
    /// </summary>
    public enum RecordsPerPage
    {
        /// <summary>
        /// The five
        /// </summary>
        [Display(Name = @"5/Página")]
        Five = 5,

        /// <summary>
        /// The ten
        /// </summary>
        [Display(Name = @"10/Página")]
        Ten = 10,

        /// <summary>
        /// The twenty
        /// </summary>
        [Display(Name = @"20/Página")]
        Twenty = 20,

        /// <summary>
        /// The thirty
        /// </summary>
        [Display(Name = @"30/Página")]
        Thirty = 30,

        /// <summary>
        /// The forty
        /// </summary>
        [Display(Name = @"40/Página")]
        Forty = 40
    }

    /// <summary>
    /// Bank enums
    /// </summary>
    public enum Banks
    {
        /// <summary>
        /// The west bank
        /// </summary>
        [Display(Name = "Banco de Occidente")]
        WestBank = 0,

        /// <summary>
        /// The santander bank
        /// </summary>
        [Display(Name = "Banco Santander")]
        SantanderBank = 1
    }

    /// <summary>
    /// Status Enum
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// The inactive
        /// </summary>
        [Display(Name = "Inactive")]
        Inactive = 0,

        /// <summary>
        /// The active
        /// </summary>
        [Display(Name = "Active")]
        Active = 1
    }

    /// <summary>
    /// Campaings Enum
    /// </summary>
    public enum Campaings
    {
        /// <summary>
        /// The eco sport
        /// </summary>
        [Display(Name = "Eco Sport")]
        EcoSport = 1,

        /// <summary>
        /// The eco sport2
        /// </summary>
        [Display(Name = "Eco Sport 2")]
        EcoSport2 = 2,

        /// <summary>
        /// The edge
        /// </summary>
        [Display(Name = "Edge")]
        Edge = 3,

        /// <summary>
        /// The escape
        /// </summary>
        [Display(Name = "Escape")]
        Escape = 4,

        /// <summary>
        /// The escape2
        /// </summary>
        [Display(Name = "Escape 2")]
        Escape2 = 5,

        /// <summary>
        /// The escape3
        /// </summary>
        [Display(Name = "Escape Hybrid")]
        Escape3 = 6,

        /// <summary>
        /// The explorer
        /// </summary>
        [Display(Name = "Explorer")]
        Explorer = 7,

        /// <summary>
        /// The fusion
        /// </summary>
        [Display(Name = "Fusion")]
        Fusion = 8,

        /// <summary>
        /// The ranger
        /// </summary>
        [Display(Name = "Ranger")]
        Ranger = 9,

        /// <summary>
        /// The carshow
        /// </summary>
        [Display(Name = "Carshow")]
        Carshow = 10,

        /// <summary>
        /// The ford
        /// </summary>
        [Display(Name = "Ford Julio")]
        FordJuly = 11,

        /// <summary>
        /// The motovalle limitless
        /// </summary>
        [Display(Name = "Motovalle Sin Límites")]
        MotovalleLimitless = 12,

        /// <summary>
        /// The mazda CDS
        /// </summary>
        [Display(Name = "Mazda - CDS")]
        MazdaCDS = 13,

        /// <summary>
        /// The ford hybrids
        /// </summary>
        [Display(Name = "Ford Híbridos")]
        FordHybrids = 14,

        /// <summary>
        /// The ford move calmly
        /// </summary>
        [Display(Name = "Movámonos con tranquilidad")]
        FordMoveCalmly = 15,

        /// <summary>
        /// The mazda santa fe
        /// </summary>
        [Display(Name = "Mazda - Santa Fé")]
        MazdaSantaFe = 16,

        /// <summary>
        /// The carshow6 meses de gracia
        /// </summary>
        [Display(Name = "Carshow - 6 Meses de Gracia")]
        Carshow6MesesDeGracia = 17,

        /// <summary>
        /// The ford plan financiero ford
        /// </summary>
        [Display(Name = "Ford - Plan Financiero Ford")]
        FordPlanFinancieroFord = 18,

        /// <summary>
        /// The ford movamonos con tranquilidad
        /// </summary>
        [Display(Name = "Ford - Movámonos Con Tranquilidad")]
        FordMovamonosConTranquilidad = 19,

        /// <summary>
        /// The ford centro comercial campanario
        /// </summary>
        [Display(Name = "Ford - Centro Comercial Campanario")]
        FordCentroComercialCampanario = 20,

        /// <summary>
        /// The ford hibridas septiembre2 020
        /// </summary>
        [Display(Name = "Ford - Híbridas Septiembre 2020")]
        FordHibridasSeptiembre2020 = 21,

        /// <summary>
        /// The ford fusion hibrida octubre 2020
        /// </summary>
        [Display(Name = "Ford - Fusion Hibrida Octubre 2020")]
        FordFusionHibridaOctubre2020 = 22,

        /// <summary>
        /// The ford fusion octubre 2020
        /// </summary>
        [Display(Name = "Ford - Fusion Octubre 2020")]
        FordFusionOctubre2020 = 23,

        /// <summary>
        /// The mazda experience
        /// </summary>
        [Display(Name = "Mazda - Motovalle Experience")]
        MazdaExperience = 24,

        /// <summary>
        /// The mazda bogota
        /// </summary>
        [Display(Name = "Mazda - Bogotá")]
        MazdaBogota = 25,

        /// <summary>
        /// The ford cali
        /// </summary>
        [Display(Name = "Ford - Cali")]
        FordCali = 26,

        /// <summary>
        /// The ford bogota
        /// </summary>
        [Display(Name = "Ford - Bogotá")]
        FordBogota = 27
    }

    /// <summary>
    /// ID Types
    /// </summary>
    public enum IDTypes
    {
        /// <summary>
        /// The cc
        /// </summary>
        [Display(Name = "Cédula de Ciudadanía")]
        CC = 1,

        /// <summary>
        /// The ce
        /// </summary>
        [Display(Name = "Cédula de extranjería")]
        CE = 2,

        /// <summary>
        /// The nit
        /// </summary>
        [Display(Name = "NIT")]
        NIT = 3,

        /// <summary>
        /// The other
        /// </summary>
        [Display(Name = "Otro")]
        Other = 4
    }

    /// <summary>
    /// Gateway Payments
    /// </summary>
    public enum GatewayPayments
    {
        /// <summary>
        /// The mercado pago
        /// </summary>
        [Display(Name = "Mercado Pagos - Tarjetas de crédito")]
        MP = 1,

        /// <summary>
        /// The wompi
        /// </summary>
        [Display(Name = "Wompi - Bancolombia")]
        Wompi = 2
    }

    /// <summary>
    /// Payment Ways Options
    /// </summary>
    public enum PaymentWaysOptions
    {
        /// <summary>
        /// The full pay
        /// </summary>
        [Display(Name = "Pago Completo")]
        FullPay = 1,

        /// <summary>
        /// The partial pay
        /// </summary>
        [Display(Name = "Pago Parcial")]
        PartialPay = 2,

        /// <summary>
        /// The funding request
        /// </summary>
        [Display(Name = "Solicitud de financiación")]
        FundingRequest = 3
    }

    /// <summary>
    /// Funding Request Institutions
    /// </summary>
    public enum FundingRequestInstitutions
    {
        /// <summary>
        /// The santander bank
        /// </summary>
        [Display(Name = "Banco Santander")]
        SantanderBank = 1

        ///// <summary>
        ///// The west bank
        ///// </summary>
        //[Display(Name = "Banco de Occidente")]
        //WestBank = 2,

        ///// <summary>
        ///// The motovalle
        ///// </summary>
        //[Display(Name = "Motovalle")]
        //Motovalle = 3
    }
    #endregion

    #region West Bank Enums
    /// <summary>
    /// Installments enum
    /// </summary>
    public enum FundingInstallments
    {
        /// <summary>
        /// The one
        /// </summary>
        [Display(Name = "1")]
        One = 1,

        /// <summary>
        /// The twelve
        /// </summary>
        [Display(Name = "12")]
        Twelve = 12,

        /// <summary>
        /// The twenty four
        /// </summary>
        [Display(Name = "24")]
        TwentyFour = 24,

        /// <summary>
        /// The thirty six
        /// </summary>
        [Display(Name = "36")]
        ThirtySix = 36,

        /// <summary>
        /// The forty eight
        /// </summary>
        [Display(Name = "48")]
        FortyEight = 48,

        /// <summary>
        /// The sixty
        /// </summary>
        [Display(Name = "60")]
        Sixty = 60,

        /// <summary>
        /// The seventy two
        /// </summary>
        [Display(Name = "72")]
        SeventyTwo = 72,

        /// <summary>
        /// The eighty
        /// </summary>
        [Display(Name = "80")]
        Eighty = 80
    }

    /// <summary>
    /// Profession enum
    /// </summary>
    public enum Profession
    {
        /// <summary>
        /// The lawyer
        /// </summary>
        [Display(Name = "Abogado")]
        Lawyer,

        /// <summary>
        /// The administrador
        /// </summary>
        Administrador,

        /// <summary>
        /// The architect
        /// </summary>
        [Display(Name = "Arquitecto")]
        Architect,

        /// <summary>
        /// The social communicator
        /// </summary>
        [Display(Name = "Comunicador social")]
        SocialCommunicator,

        /// <summary>
        /// The accountant
        /// </summary>
        [Display(Name = "Contador")]
        Accountant,

        /// <summary>
        /// The designer
        /// </summary>
        [Display(Name = "Diseñador")]
        Designer,

        /// <summary>
        /// The economist
        /// </summary>
        [Display(Name = "Economista")]
        Economist,

        /// <summary>
        /// The physiotherapist
        /// </summary>
        [Display(Name = "Fisoterapeuta")]
        Physiotherapist,

        /// <summary>
        /// The engineer
        /// </summary>
        [Display(Name = "Ingeniero")]
        Engineer,

        /// <summary>
        /// The doctor
        /// </summary>
        [Display(Name = "Médico")]
        Doctor,

        /// <summary>
        /// The marketing professional
        /// </summary>
        [Display(Name = "Profesional de mercadeo")]
        MarketingProfessional,

        /// <summary>
        /// The military
        /// </summary>
        [Display(Name = "Militar")]
        Military,

        /// <summary>
        /// The publicist
        /// </summary>
        [Display(Name = "Publicista")]
        Publicist,

        /// <summary>
        /// The other
        /// </summary>
        [Display(Name = "Otro")]
        Other
    }
    #endregion

    #region Santander Bank Enums
    /// <summary>
    /// Santander Bank Id Result
    /// </summary>
    public enum SantanderBankSemaforo
    {
        /// <summary>
        /// The problema tecnico
        /// </summary>
        [Display(Name = "Problema Técnico", Description = "Problema Técnico")]
        ProblemaTecnico = -1,

        /// <summary>
        /// The negado
        /// </summary>
        [Display(Name = "Negado", Description = "Negado")]
        Negado = 1,

        /// <summary>
        /// The pre aprobado
        /// </summary>
        [Display(Name = "Pre Aprobado", Description = "Pre Aprobado")]
        PreAprobado = 2,

        /// <summary>
        /// The aprobado con documentos
        /// </summary>
        [Display(Name = "Aprobado Con Documentos", Description = "Aprobado Con Documentos")]
        AprobadoConDocumentos = 3,

        /// <summary>
        /// The aprobado sin documentos
        /// </summary>
        [Display(Name = "Aprobado Sin Documentos", Description = "Aprobado Sin Documentos")]
        AprobadoSinDocumentos = 4,

        /// <summary>
        /// The in process
        /// </summary>
        [Display(Name = "En Proceso", Description = "En Proceso")]
        InProcess = 99
    }

    /// <summary>
    /// Santander Bank Doc Type
    /// </summary>
    public enum SantanderBankDocTypes
    {
        /// <summary>
        /// The cc
        /// </summary>
        [Display(Name = "Cédula de Ciudadanía", Description = "Cédula de Ciudadanía")]
        CC = 1,

        /// <summary>
        /// The ce
        /// </summary>
        [Display(Name = "Cédula de Extrangería", Description = "Cédula de Extrangería")]
        CE = 2,

        /// <summary>
        /// The nit
        /// </summary>
        [Display(Name = "NIT", Description = "NIT")]
        NIT = 3,

        /// <summary>
        /// The other
        /// </summary>
        [Display(Name = "Otro", Description = "Otro")]
        OTHER = 4
    }

    /// <summary>
    /// Santander Bank Installments enum
    /// </summary>
    public enum SantanderBankInstallments
    {
        /// <summary>
        /// The one
        /// </summary>
        [Display(Name = "12", Description = "12")]
        Twelve = 1,

        /// <summary>
        /// The twenty four
        /// </summary>
        [Display(Name = "24", Description = "24")]
        TwentyFour = 2,

        /// <summary>
        /// The thirty six
        /// </summary>
        [Display(Name = "36", Description = "36")]
        ThirtySix = 3,

        /// <summary>
        /// The forty eight
        /// </summary>
        [Display(Name = "48", Description = "48")]
        FortyEight = 4,

        /// <summary>
        /// The sixty
        /// </summary>
        [Display(Name = "60", Description = "60")]
        Sixty = 5,

        /// <summary>
        /// The seventy two
        /// </summary>
        [Display(Name = "72", Description = "72")]
        SeventyTwo = 6,

        /// <summary>
        /// The eighty
        /// </summary>
        [Display(Name = "84", Description = "84")]
        EightyFour = 7
    }

    /// <summary>
    /// Santander Bank Economic Activity Enum
    /// </summary>
    public enum SantanderBankEconomicActivity
    {
        /// <summary>
        /// The empleado pensionado
        /// </summary>
        [Display(Name = "Empleado - Pensionado", Description = "Empleado - Pensionado")]
        EmpleadoPensionado = 1,

        /// <summary>
        /// The independiente
        /// </summary>
        [Display(Name = "Independiente", Description = "Independiente")]
        Independiente = 2
    }

    /// <summary>
    /// Santander Bank Independent Activity Enum
    /// </summary>
    public enum SantanderBankIndependentActivity
    {
        /// <summary>
        /// The abogados litigantes
        /// </summary>
        [Display(Name = "Abogados Litigantes", Description = "Abogados Litigantes")]
        AbogadosLitigantes = 1,

        /// <summary>
        /// The agricultor
        /// </summary>
        [Display(Name = "Agricultor", Description = "Agricultor")]
        Agricultor = 2,

        /// <summary>
        /// The comerciante servicios con estabelicimiento
        /// </summary>
        [Display(Name = "Comerciante - Servicios Con Establecimiento", Description = "Comerciante - Servicios Con Establecimiento")]
        ComercianteServiciosConEstabelicimiento = 3,

        /// <summary>
        /// The comerciante servicios sin estabelicimiento
        /// </summary>
        [Display(Name = "Comerciante - Servicios Sin Establecimiento", Description = "Comerciante - Servicios Sin Establecimiento")]
        ComercianteServiciosSinEstabelicimiento = 4,

        /// <summary>
        /// The contratista
        /// </summary>
        [Display(Name = "Contratista", Description = "Contratista")]
        Contratista = 5,

        /// <summary>
        /// The ganadero
        /// </summary>
        [Display(Name = "Ganadero", Description = "Ganadero")]
        Ganadero = 6,

        /// <summary>
        /// The industrial manofactura
        /// </summary>
        [Display(Name = "Industrial - Manofactura", Description = "Industrial - Manofactura")]
        IndustrialManofactura = 7,

        /// <summary>
        /// The prestador servicios
        /// </summary>
        [Display(Name = "Prestador Servicios", Description = "Prestador Servicios")]
        PrestadorServicios = 9,

        /// <summary>
        /// The profesional independiente sin contrato de prestacion servicios
        /// </summary>
        [Display(Name = "Profesional Independiente Sin Contrato De Prestacion de Servicios", Description = "Prestador Servicios")]
        ProfesionalIndependienteSinContratoDePrestacionServicios = 10,

        /// <summary>
        /// The rentista de capital
        /// </summary>
        [Display(Name = "Rentista de Capital", Description = "Rentista de Capital")]
        RentistaDeCapital = 11,

        /// <summary>
        /// The taxista
        /// </summary>
        [Display(Name = "Taxista", Description = "Taxista")]
        Taxista = 12,

        /// <summary>
        /// The transportador
        /// </summary>
        [Display(Name = "Transportador", Description = "Transportador")]
        Transportador = 13,

        /// <summary>
        /// The venta de combustibles
        /// </summary>
        [Display(Name = "Venta De Combustibles", Description = "Venta De Combustibles")]
        VentaDeCombustibles = 14,

        /// <summary>
        /// The pensionado
        /// </summary>
        [Display(Name = "Pensionado", Description = "Pensionado")]
        Pensionado = 15,

        /// <summary>
        /// The empleado
        /// </summary>
        [Display(Name = "Empleado", Description = "Empleado")]
        Empleado = 16
    }
    #endregion

    #region Wompi Enums
    /// <summary>
    /// Wompi Enviroment
    /// </summary>
    public enum WompiEnviroment
    {
        /// <summary>
        /// The test
        /// </summary>
        [Display(Name = "Pruebas")]
        Test = 1,

        /// <summary>
        /// The product
        /// </summary>
        [Display(Name = "Producción")]
        Prod = 2
    }

    /// <summary>
    /// Wompi Status
    /// </summary>
    public enum WompiStatus
    {
        /// <summary>
        /// The pending
        /// </summary>
        [Display(Name = "Pendiente")]
        PENDING,

        /// <summary>
        /// The approved
        /// </summary>
        [Display(Name = "Aprobado")]
        APPROVED,

        /// <summary>
        /// The declined
        /// </summary>
        [Display(Name = "Rechazado")]
        DECLINED,

        /// <summary>
        /// The error
        /// </summary>
        [Display(Name = "Error")]
        ERROR,

        /// <summary>
        /// The voided
        /// </summary>
        [Display(Name = "Anulado")]
        VOIDED
    }

    /// <summary>
    /// Wompi Payment Method Type
    /// </summary>
    public enum WompiPaymentMethodType
    {
        /// <summary>
        /// The card
        /// </summary>
        [Display(Name = "Tarjeta")]
        CARD,

        /// <summary>
        /// The nequi
        /// </summary>
        [Display(Name = "NEQUI")]
        NEQUI,

        /// <summary>
        /// The pse
        /// </summary>
        [Display(Name = "PSE")]
        PSE,

        /// <summary>
        /// The bancolombia transfer
        /// </summary>
        [Display(Name = "Transferencia Bancolombia")]
        BANCOLOMBIA_TRANSFER,

        /// <summary>
        /// The bancolombia collect
        /// </summary>
        [Display(Name = "Recaudo Bancolombia")]
        BANCOLOMBIA_COLLECT
    }

    /// <summary>
    /// Currency type
    /// </summary>
    public enum WompiCurrencyType
    {
        /// <summary>
        /// The cop
        /// </summary>
        [Display(Name = "Pesos Colombianos")]
        COP = 1
    }
    #endregion
}
