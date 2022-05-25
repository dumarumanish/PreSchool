using PreSchool.Application.Services.PaymentPartners.Models.Commands;
using PreSchool.Application.Services.PaymentPartners.Models.Dtos;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.PaymentPartners
{
    public interface IIMEPayService
    {
        Task<string> TransactionToken(IMEPayTransactionToken getToken);
        Task<PaymentTransactionVerificationDto> VerifyPayment(VerifyIMEPayPayment verifyIMEPay);
    }
}