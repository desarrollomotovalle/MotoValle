// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonHelper.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Common Helper
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers
{
    using MercadoPago.DataStructures.Payment;
    using MercadoPago.Resources;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Helpers.ApiRequest;
    using motovalle.Ecommerce.Models;
    using motovalle.Ecommerce.Models.DTO.Services.FundingRequests.SantanderBank;
    using motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Events;
    using motovalle.Ecommerce.Models.ViewModels;
    using motovalle.Ecommerce.Models.ViewModels.Checkout.FundingRequest;
    using motovalle.Ecommerce.Models.ViewModels.Checkout.GatewayPayment.MercadoPagos;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Common Helper
    /// </summary>
    public static class CommonHelper
    {
        /// <summary>
        /// Gets the years.
        /// </summary>
        /// <returns>Select List task</returns>
        public static async Task<SelectList> GetYears(HttpClient httpClient, int makesId)
        {
            var makesApi = new MakesApi(httpClient);
            var productsAndMakeBaseTotal = await makesApi.GetProductAndMakeBase(makesId);
            return new SelectList(productsAndMakeBaseTotal.GroupBy(x => x.Year).OrderByDescending(x => x.Key).ToList(), "Key", "Key");
        }

        /// <summary>
        /// Converts to santanderbankfundingrequest.
        /// </summary>
        /// <param name="checkoutViewModel">The checkout view model.</param>
        /// <returns>Santander Bank Funding Request</returns>
        public static SantanderBankFundingRequest ToSantanderBankFundingRequest(CheckoutViewModel checkoutViewModel, IConfiguration configuration)
        {
            return new SantanderBankFundingRequest()
            {
                DatosBasicos = new SantanderDatosBasicos()
                {
                    AsociadoJuriscoop = false,
                    Celular = checkoutViewModel.PhoneNumber,
                    CorreoPersonal = checkoutViewModel.Email,
                    CuotaInicial = (int)checkoutViewModel.FundingRequestViewModel.InitialFee,
                    Nombre1 = checkoutViewModel.FullName,
                    NumeroDocumento = checkoutViewModel.IDNumber,
                    Plazo = checkoutViewModel.FundingRequestViewModel.FundingInstallments,
                    TipoDocumento = checkoutViewModel.FundingRequestViewModel.IDType,
                    ValorVehiculo = (int)checkoutViewModel.Total
                },
                DatosFinancieros = new SantanderDatosFinancieros()
                {
                    ActividadEconomica = checkoutViewModel.FundingRequestViewModel.SantanderBankEconomicActivity,
                    ActividadIndependiente = checkoutViewModel.FundingRequestViewModel.SantanderBankIndependentActivity,
                    IngresoMensual = checkoutViewModel.FundingRequestViewModel.MonthlyIncome
                },
                OtrosDatos = new SantanaderBankOtherData()
                {
                    ConcesionarioRadicacion = configuration.GetSection("SantanderBankSettings").GetValue<int>("ConcesionarioRadicacion"),
                    IdentificacionVendedor = configuration.GetSection("SantanderBankSettings").GetValue<int>("IdentificacionVendedor"),
                    UsuarioRadica = configuration.GetSection("SantanderBankSettings").GetValue<string>("UsuarioRadica")
                },
                IsJuriscoop = false
            };
        }

        /// <summary>
        /// Converts to santanderbankfundingrequest.
        /// </summary>
        /// <param name="creditOnlineNowViewModel">The credit online now view model.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>Santander Bank Funding Request</returns>
        public static SantanderBankFundingRequest ToSantanderBankFundingRequest(CreditOnlineNowViewModel creditOnlineNowViewModel, IConfiguration configuration)
        {
            return new SantanderBankFundingRequest()
            {
                DatosBasicos = new SantanderDatosBasicos()
                {
                    AsociadoJuriscoop = false,
                    Celular = creditOnlineNowViewModel.PhoneNumber,
                    CorreoPersonal = creditOnlineNowViewModel.EmailAddress,
                    CuotaInicial = creditOnlineNowViewModel.InitialFee,
                    Nombre1 = creditOnlineNowViewModel.FullName,
                    NumeroDocumento = creditOnlineNowViewModel.DocNumber,
                    Plazo = creditOnlineNowViewModel.Installments,
                    TipoDocumento = creditOnlineNowViewModel.DocType,
                    ValorVehiculo = creditOnlineNowViewModel.TotalAmount
                },
                DatosFinancieros = new SantanderDatosFinancieros()
                {
                    ActividadEconomica = creditOnlineNowViewModel.ActividadEconomica,
                    ActividadIndependiente = creditOnlineNowViewModel.ActividadIndependiente,
                    IngresoMensual = creditOnlineNowViewModel.MonthlyIncome
                },
                OtrosDatos = new SantanaderBankOtherData()
                {
                    ConcesionarioRadicacion = configuration.GetSection("SantanderBankSettings").GetValue<int>("ConcesionarioRadicacion"),
                    IdentificacionVendedor = configuration.GetSection("SantanderBankSettings").GetValue<int>("IdentificacionVendedor"),
                    UsuarioRadica = configuration.GetSection("SantanderBankSettings").GetValue<string>("UsuarioRadica")
                },
                IsJuriscoop = false
            };
        }

        /// <summary>
        /// Converts to santanderbankfundingrequest.
        /// </summary>
        /// <param name="checkoutFundingRequestSantaderBankViewModel">The checkout funding request santader bank view model.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>Santander Bank Funding Request</returns>
        public static SantanderBankFundingRequest ToSantanderBankFundingRequest(CheckoutFundingRequestSantaderBankViewModel checkoutFundingRequestSantaderBankViewModel, IConfiguration configuration)
        {
            return new SantanderBankFundingRequest()
            {
                DatosBasicos = new SantanderDatosBasicos()
                {
                    AsociadoJuriscoop = false,
                    Celular = checkoutFundingRequestSantaderBankViewModel.CheckoutData.CustomerData.PhoneNumber,
                    CorreoPersonal = checkoutFundingRequestSantaderBankViewModel.CheckoutData.CustomerData.Email,
                    CuotaInicial = checkoutFundingRequestSantaderBankViewModel.SantanderBankCreditOnline.InitialFee,
                    Nombre1 = checkoutFundingRequestSantaderBankViewModel.CheckoutData.CustomerData.FullName,
                    NumeroDocumento = checkoutFundingRequestSantaderBankViewModel.CheckoutData.CustomerData.IDNumber,
                    Plazo = checkoutFundingRequestSantaderBankViewModel.SantanderBankCreditOnline.Installments,
                    TipoDocumento = checkoutFundingRequestSantaderBankViewModel.SantanderBankCreditOnline.DocType,
                    ValorVehiculo = checkoutFundingRequestSantaderBankViewModel.SantanderBankCreditOnline.TotalAmount
                },
                DatosFinancieros = new SantanderDatosFinancieros()
                {
                    ActividadEconomica = checkoutFundingRequestSantaderBankViewModel.SantanderBankCreditOnline.ActividadEconomica,
                    ActividadIndependiente = checkoutFundingRequestSantaderBankViewModel.SantanderBankCreditOnline.ActividadIndependiente,
                    IngresoMensual = checkoutFundingRequestSantaderBankViewModel.SantanderBankCreditOnline.MonthlyIncome
                },
                OtrosDatos = new SantanaderBankOtherData()
                {
                    ConcesionarioRadicacion = configuration.GetSection("SantanderBankSettings").GetValue<int>("ConcesionarioRadicacion"),
                    IdentificacionVendedor = configuration.GetSection("SantanderBankSettings").GetValue<int>("IdentificacionVendedor"),
                    UsuarioRadica = configuration.GetSection("SantanderBankSettings").GetValue<string>("UsuarioRadica")
                },
                IsJuriscoop = false
            };
        }

        /// <summary>
        /// Saves the report on temporary path.
        /// </summary>
        /// <param name="bodyRequest">The report.</param>
        public static void SaveRequestReportOnTempPath(string reportId, string bodyRequest, string response, string requestType)
        {
            var partialPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "RequestReport");
            var fileName = "RequestReport.txt";
            var fullPath = Path.Combine(partialPath, fileName);

            if (!System.IO.Directory.Exists(partialPath))
            {
                System.IO.Directory.CreateDirectory(partialPath);
            }

            if (!File.Exists(fullPath))
            {
                File.Create(fullPath);
            }

            var content = @$"------------------------------------------------------------------------ {Environment.NewLine} Registred On: {DateTime.Now:dddd, dd MMMM yyyy HH:mm:ss}{Environment.NewLine} Report Id: {reportId} {Environment.NewLine} Request Type: {requestType} {Environment.NewLine} Request Body: {bodyRequest} {Environment.NewLine} Response: {response}------------------------------------------------------------------------ {Environment.NewLine} {Environment.NewLine}";
            using (var tw = new StreamWriter(fullPath, true))
            {
                tw.WriteLine(content);
                tw.Close();
            }
        }

        /// <summary>
        /// Converts to formatjson.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>Beauty Json</returns>
        public static string ToFormatJson(string json)
        {
            var INDENT_STRING = "    ";
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();
            for (var i = 0; i < json.Length; i++)
            {
                var ch = json[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = false;
                        var index = i;
                        while (index > 0 && json[--index] == '\\')
                        {
                            escaped = !escaped;
                        }

                        if (!escaped)
                        {
                            quoted = !quoted;
                        }

                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.Append(" ");
                        }

                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie">The ie.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (var i in ie)
            {
                action(i);
            }
        }

        /// <summary>
        /// Gets the santander bank semaforo message.
        /// </summary>
        /// <param name="santanderBankSemaforo">The santander bank semaforo.</param>
        /// <returns>Semaforo message</returns>
        public static string GetSantanderBankSemaforoMessage(SantanderBankSemaforo santanderBankSemaforo)
        {
            return santanderBankSemaforo switch
            {
                SantanderBankSemaforo.ProblemaTecnico => "Problema Técnico",
                SantanderBankSemaforo.Negado => "Negado",
                SantanderBankSemaforo.PreAprobado => "Pre - Aprobado",
                SantanderBankSemaforo.AprobadoConDocumentos => "Aprobado Con Documentos",
                SantanderBankSemaforo.AprobadoSinDocumentos => "Aprobado Sin Documentos",
                SantanderBankSemaforo.InProcess => "En Proceso",
                _ => string.Empty,
            };
        }

        /// <summary>
        /// Gets the santander bank independent activity message.
        /// </summary>
        /// <param name="santanderBankIndependentActivity">The santander bank independent activity.</param>
        /// <returns>Santander Bank Independent Activity Message</returns>
        public static string GetSantanderBankIndependentActivityMessage(SantanderBankIndependentActivity santanderBankIndependentActivity)
        {
            return santanderBankIndependentActivity switch
            {
                SantanderBankIndependentActivity.AbogadosLitigantes => "Abogados Litigantes",
                SantanderBankIndependentActivity.Agricultor => "Agricultor",
                SantanderBankIndependentActivity.ComercianteServiciosConEstabelicimiento => "Comerciante Servicios Con Estabelicimiento",
                SantanderBankIndependentActivity.ComercianteServiciosSinEstabelicimiento => "Comerciante Servicios Sin Estabelicimiento",
                SantanderBankIndependentActivity.Contratista => "Contratista",
                SantanderBankIndependentActivity.Ganadero => "Ganadero",
                SantanderBankIndependentActivity.IndustrialManofactura => "Industrial Manofactura",
                SantanderBankIndependentActivity.PrestadorServicios => "Prestador Servicios",
                SantanderBankIndependentActivity.ProfesionalIndependienteSinContratoDePrestacionServicios => "Profesional Independiente Sin Contrato De Prestacion Servicios",
                SantanderBankIndependentActivity.RentistaDeCapital => "Rentista De Capital",
                SantanderBankIndependentActivity.Taxista => "Taxista",
                SantanderBankIndependentActivity.Transportador => "Transportador",
                SantanderBankIndependentActivity.VentaDeCombustibles => "Venta De Combustibles",
                SantanderBankIndependentActivity.Pensionado => "Pensionado",
                SantanderBankIndependentActivity.Empleado => "Empleado",
                _ => string.Empty
            };
        }

        /// <summary>
        /// Gets the santander bank economic activity message.
        /// </summary>
        /// <param name="santanderBankEconomicActivity">The santander bank economic activity.</param>
        /// <returns>Santander Bank Economic Activity Message</returns>
        public static string GetSantanderBankEconomicActivityMessage(SantanderBankEconomicActivity santanderBankEconomicActivity)
        {
            return santanderBankEconomicActivity switch
            {
                SantanderBankEconomicActivity.EmpleadoPensionado => "Empleado - Pensionado",
                SantanderBankEconomicActivity.Independiente => "Independiente",
                _ => string.Empty
            };
        }

        /// <summary>
        /// Gets the santander bank installments message.
        /// </summary>
        /// <param name="santanderBankInstallments">The santander bank installments.</param>
        /// <returns>Santander Bank Installments String</returns>
        public static string GetSantanderBankInstallmentsMessage(SantanderBankInstallments santanderBankInstallments)
        {
            return santanderBankInstallments switch
            {
                SantanderBankInstallments.Twelve => "12",
                SantanderBankInstallments.TwentyFour => "24",
                SantanderBankInstallments.ThirtySix => "36",
                SantanderBankInstallments.FortyEight => "48",
                SantanderBankInstallments.Sixty => "60",
                SantanderBankInstallments.SeventyTwo => "72",
                SantanderBankInstallments.EightyFour => "84",
                _ => "0"
            };
        }

        /// <summary>
        /// Gets the name of the bank.
        /// </summary>
        /// <param name="bank">The santander bank economic activity.</param>
        /// <returns>Bank Name</returns>
        public static string GetBankName(Banks bank)
        {
            return bank switch
            {
                Banks.SantanderBank => "Banco Santander",
                Banks.WestBank => "Banco de Occidente",
                _ => string.Empty
            };
        }

        /// <summary>
        /// Computes the sha256 hash.
        /// </summary>
        /// <param name="rawData">The raw data.</param>
        /// <returns>Computed Hash</returns>
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using var sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            sha256Hash.Dispose();
            return builder.ToString();
        }

        /// <summary>
        /// Saves the wompi event notification.
        /// </summary>
        /// <param name="reportId">The report identifier.</param>
        /// <param name="wompiEvent">The wompi event.</param>
        /// <param name="hash">The hash.</param>
        public static void SaveWompiEventNotification(string reportId, WompiEventDTO wompiEvent, string hash)
        {
            var partialPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "RequestReport");
            const string fileName = "WompiEventNotification.txt";
            var fullPath = Path.Combine(partialPath, fileName);

            if (!System.IO.Directory.Exists(partialPath))
            {
                System.IO.Directory.CreateDirectory(partialPath);
            }

            if (!File.Exists(fullPath))
            {
                File.Create(fullPath);
            }

            var content = $"------------------------------------------------------------------------ {Environment.NewLine} Registred On: {DateTime.Now:dddd, dd MMMM yyyy HH:mm:ss}{Environment.NewLine} Report Id: {reportId} {Environment.NewLine}Event: {wompiEvent.Event}{Environment.NewLine}Sent At Local: {wompiEvent.SentAtLocal}{Environment.NewLine}Hash: {hash}{Environment.NewLine}Env: {wompiEvent.Environment}{Environment.NewLine}Transaction: {JsonConvert.SerializeObject(wompiEvent.Data.Transaction)}------------------------------------------------------------------------ {Environment.NewLine} {Environment.NewLine}";
            using var tw = new StreamWriter(fullPath, true);
            tw.WriteLine(content);
            tw.Close();
        }
    }
}