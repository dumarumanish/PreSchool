using PreSchool.Application.Services.Payments.Models.Commands;
using PreSchool.Application.Services.Payments.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Payments
{
    public interface IPaymentService
    {
        Task<PaymentModeDto> PaymentMode(int Id);
        Task<List<PaymentModeDto>> PaymentsModes();
        List<EnumValue> PaymentStatuses();
        Task<bool> UpdatePaymentMode(UpdatePaymentMode paymentMode);
    }
}