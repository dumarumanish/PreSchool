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
    public class KhaltiService : IKhaltiService
    {
        private readonly KhaltiSettings _khaltiSettings;
        private readonly IHttpClientFactory _clientFactory;

        public KhaltiService(
            IHttpClientFactory clientFactory,
            IOptions<KhaltiSettings> khaltiSettings
            )
        {
            _khaltiSettings = khaltiSettings.Value;
            _clientFactory = clientFactory;

        }


        public async Task<PaymentTransactionVerificationDto> VerifyKhaltiPayment(VerifyKhaltiPayment verifyKhalti)
        {

            // Check for valid token
            if (string.IsNullOrWhiteSpace(verifyKhalti.token))
                return new PaymentTransactionVerificationDto
                {
                    IsPaymentVerified = false,
                    Remark = "Invalid payment token"
                };


            HttpClient client = _clientFactory.CreateClient();

            var uri = @"https://khalti.com/api/v2/payment/verify/";
            var payload = new VerifyKhaltiPayment
            {
                // Amount should be in paisa
                amount = (int)(decimal.Round(verifyKhalti.amount, 2, MidpointRounding.AwayFromZero) * 100),
                token = verifyKhalti.token
            };

            var serializedPayload = JsonConvert.SerializeObject(payload);

            var content = new StringContent(serializedPayload, Encoding.UTF8,
                        "application/json");

            // Add authorization header
            client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Key", _khaltiSettings.SecretKey);

            HttpResponseMessage response = await client.PostAsync(
                uri, content);

            if (response.IsSuccessStatusCode)
            {

                var verificationResponse = await response.Content.ReadAsAsync<KhaltiVerificationResponse>();

                if (verificationResponse != null && verificationResponse.state != null && verificationResponse.state != null)
                {
                    // Check if Completed
                    if (verificationResponse.state.name == "Completed")
                    {
                        // Return pament verified
                        return new PaymentTransactionVerificationDto
                        {
                            IsPaymentVerified = true,
                            Remark = await response.Content.ReadAsStringAsync(),
                        };
                    }
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
