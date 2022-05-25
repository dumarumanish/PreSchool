using PreSchool.Application;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Models;
using PreSchool.Application.Services.PaymentPartners.Models.Commands;
using PreSchool.Application.Services.PaymentPartners.Models.Dtos;
using PreSchool.Application.Services.Payments;
using PreSchool.Application.Services.Payments.Models.Commands;
using PreSchool.Application.Services.Payments.Models.Dtos;
using PreSchool.Data.Enumerations;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {

        private readonly IPaymentService _paymentService;
        private readonly EmailSettings _emailSettings;

        public PaymentsController(IPaymentService paymentService, IOptions<EmailSettings> emailSettings)
        {
            _paymentService = paymentService;
            _emailSettings = emailSettings.Value;
        }

        /// <summary>
        /// Get Payment statuses
        /// </summary>
        /// <returns></returns>
        [HttpGet("Statuses")]
        public List<EnumValue> PaymentStatuses()
        {
            return _paymentService.PaymentStatuses();

        }
        #region Payment Mode

        [HttpPut("Modes/{id}")]
        [AuthorizeUser(Permissions.ManagePaymentMode)]
        public async Task<bool> UpdatePayment(int id, [FromForm] UpdatePaymentMode paymentMode)
        {
            if (paymentMode == null)
                throw new BadRequestException("Payment Mode is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != paymentMode.id)
                throw new BadRequestException("Id doesnot match");
            return await _paymentService.UpdatePaymentMode(paymentMode);

        }

        [HttpGet("Modes")]
        public async Task<List<PaymentModeDto>> GetPaymentModes()
        {
            return await _paymentService.PaymentsModes();

        }

        [HttpGet("Modes/{id}")]
        public async Task<PaymentModeDto> GetPaymentMode(int id)
        {
            return await _paymentService.PaymentMode(id);

        }

        #endregion

    }
}

