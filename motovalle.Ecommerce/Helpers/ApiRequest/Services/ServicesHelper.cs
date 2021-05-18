// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServicesHelper.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Services Helper
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.ApiRequest.Services
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Models;
    using motovalle.Ecommerce.Models.DTO;
    using motovalle.Ecommerce.Models.DTO.Services.FundingRequests;
    using motovalle.Ecommerce.Models.DTO.Services.FundingRequests.SantanderBank;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Services Helper
    /// </summary>
    public class ServicesHelper : IServicesHelper
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The santander base URL
        /// </summary>
        private readonly string _santanderBaseUrl;

        /// <summary>
        /// The santander timeout
        /// </summary>
        private readonly int _santanderTimeout;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesHelper"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public ServicesHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._santanderBaseUrl = this._configuration.GetSection("SantanderBankSettings").GetValue<string>("UrlBase");
            this._santanderTimeout = this._configuration.GetSection("SantanderBankSettings").GetValue<int>("Timeout");
        }

        private FormUrlEncodedContent formContent { get; set; }

        /// <summary>
        /// Santanders the bank authentication.
        /// </summary>
        /// <returns>
        /// Santander Bank Auth Response
        /// </returns>
        public async Task<GenericResponse<SantanderBankAuthResponse>> SantanderBankAuth()
        {
            try
            {
                var client = new HttpClient()
                {
                    BaseAddress = new Uri(this._santanderBaseUrl),
                    Timeout = TimeSpan.FromMilliseconds(this._santanderTimeout)
                };

                //var credentials = new SantanderBankCredentials()
                //{
                //    Password = this._configuration.GetSection("SantanderBankSettings").GetValue<string>("Password"),
                //    Username = this._configuration.GetSection("SantanderBankSettings").GetValue<string>("Username"),
                //};

                string userPass = this._configuration.GetSection("SantanderBankSettings").GetValue<string>("UserPass");
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("UserPass", userPass);

                formContent = new FormUrlEncodedContent(param);
                //var result = await client.PostAsync($"/api/login/authenticateEncoded", formContent);
                var result = await client.PostAsync($"api/login/authenticateEncoded", formContent);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    var santanderBankFundingResponse = JsonConvert.DeserializeObject<SantanderBankAuthResponse>(response);
                    return new GenericResponse<SantanderBankAuthResponse>()
                    {
                        IsSuccess = true,
                        Message = "Ok",
                        Result = santanderBankFundingResponse
                    };
                }

                return new GenericResponse<SantanderBankAuthResponse>()
                {
                    IsSuccess = false,
                    Message = "BadRequest",
                    Result = new SantanderBankAuthResponse()
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<SantanderBankAuthResponse>()
                {
                    IsSuccess = false,
                    Message = $"{ex.Message} {ex?.InnerException?.Message ?? string.Empty}",
                    Result = new SantanderBankAuthResponse()
                };
            }
        }

        /// <summary>
        /// Santanders the funding request.
        /// </summary>
        /// <param name="santanderBankFundingRequest">The santander bank funding request.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// Santander Bank Funding Request Response
        /// </returns>
        public async Task<GenericResponse<SantanderBankFundingResponse>> SantanderFundingRequest(SantanderBankFundingRequest santanderBankFundingRequest, string token)
        {
            try
            {
                var client = new HttpClient()
                {
                    BaseAddress = new Uri(this._santanderBaseUrl),
                    Timeout = TimeSpan.FromMilliseconds(this._santanderTimeout)
                };

                ////Set Headers
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("User-Agent", "Tienda Motovalle");
                ////Set Token Type (Bearer)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
                //var result = await client.PostAsync($"api/viabilizacion/getViabxConcesionario", new StringContent(JsonConvert.SerializeObject(santanderBankFundingRequest), Encoding.UTF8, "application/json"));
                var result = await client.PostAsync($"api/viabilizacion/getViabxConcesionario", new StringContent(JsonConvert.SerializeObject(santanderBankFundingRequest), Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    var santanderBankFundingResponse = JsonConvert.DeserializeObject<SantanderBankFundingResponse>(response);
                    return new GenericResponse<SantanderBankFundingResponse>()
                    {
                        IsSuccess = true,
                        Message = "Ok",
                        Result = santanderBankFundingResponse
                    };
                }

                return new GenericResponse<SantanderBankFundingResponse>()
                {
                    IsSuccess = false,
                    Message = "BadRequest",
                    Result = new SantanderBankFundingResponse()
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<SantanderBankFundingResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = new SantanderBankFundingResponse()
                };
            }
        }

        /// <summary>
        /// Converts to fundingrequestgeneralresponse.
        /// </summary>
        /// <param name="santanderBankFundingResponse">The santander bank funding response.</param>
        /// <param name="reportId">The report identifier.</param>
        /// <returns>
        /// Funding Request General Response
        /// </returns>
        public FundingRequestGeneralResponse<SantanderBankFundingResponse> ToFundingRequestGeneralResponse(GenericResponse<SantanderBankFundingResponse> santanderBankFundingResponse, string reportId)
        {
            return santanderBankFundingResponse.Result.Semaforo switch
            {
                SantanderBankSemaforo.ProblemaTecnico => new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "Info", Message = $"Señor usuario se ha generado un <b>PROBLEMA TÉCNICO</b> al ejecutar la solicitud. Por favor comparta este identificador al administrador del sistema: <b>{reportId}</b>. Por favor intente más tarde.", Response = santanderBankFundingResponse.Result },
                SantanderBankSemaforo.Negado => new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "Info", Message = $"Señor usuario su solicitud no cumple con los requisitos y ha sido <b>NEGADA</b>. {santanderBankFundingResponse.Result.Titulo}", Response = santanderBankFundingResponse.Result },
                SantanderBankSemaforo.PreAprobado => new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "Info", Message = $"Señor usuario su solicitud es <b>PRE-APROBADA</b>, por favor verifique lo siguiente: {santanderBankFundingResponse.Result.Titulo}", Response = santanderBankFundingResponse.Result },
                SantanderBankSemaforo.AprobadoConDocumentos => new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "FundingRequestOk", Message = $"Señor usuario: {santanderBankFundingResponse.Result.Titulo}.", Response = santanderBankFundingResponse.Result },
                SantanderBankSemaforo.AprobadoSinDocumentos => new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "FundingRequestOk", Message = $"Señor usuario: {santanderBankFundingResponse.Result.Titulo}.", Response = santanderBankFundingResponse.Result },
                _ => new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "Error", Message = $"Error al realizar la solicitud de crédito, favor comuniquese con el administrador del sistema y compartele este identificador: <b>{reportId}</b>", Response = santanderBankFundingResponse.Result },
            };
        }
    }
}
