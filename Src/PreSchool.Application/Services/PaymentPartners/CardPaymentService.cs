using PreSchool.Application.Exceptions;
using PreSchool.Application.Models.PaymentPartners;
using PreSchool.Application.Services.PaymentPartners.Models.Commands;
using PreSchool.Application.Services.PaymentPartners.Models.Dtos;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace PreSchool.Application.Services.PaymentPartners
{
    public class CardPaymentService : ICardPaymentService
    {
        private readonly CardPaymentSettings _cardPaymentSettings;
        private readonly IHttpClientFactory _clientFactory;

        public CardPaymentService(
            IHttpClientFactory clientFactory,
            IOptions<CardPaymentSettings> cardPaymentSettingOptions
            )
        {
            _cardPaymentSettings = cardPaymentSettingOptions.Value;
            _clientFactory = clientFactory;

        }


        public string GetSignData(CardPaymentSignData data)
        {
            if (data == null || data.Data == null || data.Data.Count == 0)
                throw new BadRequestException("Form data is required");
            var dataToSign = buildDataToSign(data.Data.ToDictionary(d => d.FieldName, d => d.FieldValue));


            UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(_cardPaymentSettings.SecretKey);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(dataToSign);

            return Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));
        }




        #region Generate sign
        private static string buildDataToSign(IDictionary<string, string> paramsArray)
        {
            string[] signedFieldNames = paramsArray["signed_field_names"].Split(',');
            IList<string> dataToSign = new List<string>();

            foreach (string signedFieldName in signedFieldNames)
            {
                var value = string.Empty;
                _ = paramsArray.TryGetValue(signedFieldName, out value);
                dataToSign.Add(signedFieldName + "=" + value);
            }

            return commaSeparate(dataToSign);
        }

        private static string commaSeparate(IList<string> dataToSign)
        {
            return string.Join(",", dataToSign);
        }
        #endregion

    }
}
