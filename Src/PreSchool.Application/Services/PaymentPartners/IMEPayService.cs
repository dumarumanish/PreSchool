using PreSchool.Application.Models.PaymentPartners;
using PreSchool.Application.Services.PaymentPartners.Models.Commands;
using PreSchool.Application.Services.PaymentPartners.Models.Dtos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.PaymentPartners
{
    public class IMEPayService : IIMEPayService
    {
        private readonly IMEPaySettings _imePaySettings;
        private readonly IHttpClientFactory _clientFactory;

        public IMEPayService(
            IHttpClientFactory clientFactory,
            IOptions<IMEPaySettings> esewaSettingOptions
            )
        {
            _imePaySettings = esewaSettingOptions.Value;
            _clientFactory = clientFactory;

        }

        public async Task<string> TransactionToken(IMEPayTransactionToken getToken)
        {

            HttpClient client = _clientFactory.CreateClient();

            var uri = _imePaySettings.BaseUrl + "api/Web/GetToken";
            var payload = new
            {
                MerchantCode = _imePaySettings.MerchantCode,
                Amount = getToken.Amount,
                RefId = getToken.RefId
            };


            var serializedPayload = JsonConvert.SerializeObject(payload);

            var content = new StringContent(serializedPayload, Encoding.UTF8,
                        "application/json");

            // Add authorization header
            var authenticationString = $"{_imePaySettings.Username}:{_imePaySettings.Password}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

            // Add Module
            client.DefaultRequestHeaders.Add("Module", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(_imePaySettings.MarchantModule)));

            HttpResponseMessage response = await client.PostAsync(
                uri, content);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadAsAsync<IMEPayTokenResponse>();

                if (tokenResponse != null)
                {
                    return tokenResponse.TokenId;
                }

            }

            return null;
        }

        public async Task<PaymentTransactionVerificationDto> VerifyPayment(VerifyIMEPayPayment verifyIMEPay)
        {

            // Check for valid token
            if (string.IsNullOrWhiteSpace(verifyIMEPay.TokenId))
                return new PaymentTransactionVerificationDto
                {
                    IsPaymentVerified = false,
                    Remark = "Invalid payment token"
                };



            HttpClient client = _clientFactory.CreateClient();

            var uri = _imePaySettings.BaseUrl + "api/Web/Confirm";
            var payload = new
            {
                MerchantCode = _imePaySettings.MerchantCode,
                Msisdn = verifyIMEPay.Msisdn,
                RefId = verifyIMEPay.RefId,
                TokenId = verifyIMEPay.TokenId,
                TransactionId = verifyIMEPay.TransactionId
            };

            var serializedPayload = JsonConvert.SerializeObject(payload);

            var content = new StringContent(serializedPayload, Encoding.UTF8,
                        "application/json");

            // Add authorization header
            var authenticationString = $"{_imePaySettings.Username}:{_imePaySettings.Password}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

            // Add Module
            client.DefaultRequestHeaders.Add("Module", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(_imePaySettings.MarchantModule)));

            HttpResponseMessage response = await client.PostAsync(
                uri, content);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadAsAsync<IMEPayVerifyResponse>();

                if (tokenResponse != null)
                {
                    return new PaymentTransactionVerificationDto
                    {
                        IsPaymentVerified = tokenResponse.ResponseCode == 0,
                        Remark = serializedPayload
                    };
                }

            }


            return new PaymentTransactionVerificationDto
            {
                IsPaymentVerified = false,
                Remark = "Cannot verify the payement"
            };
        }

    }
}
