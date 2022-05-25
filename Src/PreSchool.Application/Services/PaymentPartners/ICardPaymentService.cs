using PreSchool.Application.Services.PaymentPartners.Models.Commands;

namespace PreSchool.Application.Services.PaymentPartners
{
    public interface ICardPaymentService
    {
        string GetSignData(CardPaymentSignData data);
    }
}