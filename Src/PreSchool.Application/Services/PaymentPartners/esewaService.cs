using PreSchool.Application.Models.PaymentPartners;
using PreSchool.Application.Services.PaymentPartners.Models.Commands;
using PreSchool.Application.Services.PaymentPartners.Models.Dtos;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.PaymentPartners
{
    public class esewaService : IesewaService
    {
        private readonly esewaSettings _esewaSettings;
        private readonly IHttpClientFactory _clientFactory;

        public esewaService(
            IHttpClientFactory clientFactory,
            IOptions<esewaSettings> esewaSettingOptions
            )
        {
            _esewaSettings = esewaSettingOptions.Value;
            _clientFactory = clientFactory;

        }

        public async Task<PaymentTransactionVerificationDto> VerifyesewaPayment(VerifyEsewaPayment verifyEsewa)
        {

            var client = _clientFactory.CreateClient();


            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("amt", decimal.Round(verifyEsewa.Amount, 2, MidpointRounding.AwayFromZero).ToString() ),
                new KeyValuePair<string, string>("pid", verifyEsewa.ProductCode),
                new KeyValuePair<string, string>("rid", verifyEsewa.ReferenceCode),
                new KeyValuePair<string, string>("scd",  _esewaSettings.MerchantCode)
            });

            HttpResponseMessage response = await client.PostAsync(
                _esewaSettings.esewaPaymentVerificationUrl, formContent);

            if (response.IsSuccessStatusCode)
            {

                var verificationResponse = await response.Content.ReadAsStringAsync();

                if (verificationResponse.Contains("Success"))
                {

                    // Return pament verified
                    return new PaymentTransactionVerificationDto
                    {
                        IsPaymentVerified = true,
                        Remark = verificationResponse
                    };
                }

                return new PaymentTransactionVerificationDto
                {
                    IsPaymentVerified = false,
                    Remark = verificationResponse
                };

            }

            return new PaymentTransactionVerificationDto
            {
                IsPaymentVerified = false,
                Remark = "Cannot verify the payement"
            };
        }

    }
}
