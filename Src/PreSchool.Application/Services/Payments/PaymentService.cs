using PreSchool.Application.Events;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Models;
using PreSchool.Application.Services.AppConfigurations;
using PreSchool.Application.Services.Files;
using PreSchool.Application.Services.PaymentPartners;
using PreSchool.Application.Services.PaymentPartners.Models.Commands;
using PreSchool.Application.Services.PaymentPartners.Models.Dtos;
using PreSchool.Application.Services.Payments.Models.Commands;
using PreSchool.Application.Services.Payments.Models.Dtos;
using PreSchool.Data.Entities.Payments;
using PreSchool.Data.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IFileService _fileService;
        private readonly IAppFeatureService _appFeatureService;
        private readonly IesewaService _esewaService;
        private readonly IKhaltiService _khaltiService;
        private readonly IIMEPayService _imePayService;
        private readonly ICardPaymentService _cardPaymentService;

        public PaymentService(
            IApplicationDbContext context,
            IDateTime dateTime,
            ICurrentUserService currentUserService,
            IEventPublisher eventPublisher,
            IFileService fileService,
            IAppFeatureService appFeatureService,
            IesewaService esewaService,
            IKhaltiService khaltiService,
            IIMEPayService imePayService,
            ICardPaymentService cardPaymentService

            )
        {
            _context = context;
            _dateTime = dateTime;
            _currentUserService = currentUserService;
            _eventPublisher = eventPublisher;
            _fileService = fileService;
            _appFeatureService = appFeatureService;
            _esewaService = esewaService;
            _khaltiService = khaltiService;
            _imePayService = imePayService;
            _cardPaymentService = cardPaymentService;
        }

        #region Payment Mode
        public List<EnumValue> PaymentStatuses()
        {
            return EnumHelper.GetEnumValues<PaymentStatusEnum>();
        }
        public Task<List<PaymentModeDto>> PaymentsModes()
        {
            return _context.PaymentModes
                .Include(m => m.Image)
                 .AsNoTracking()
                .OrderBy(i => i.DisplayOrder)
                .Select(d => new PaymentModeDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Image = d.Image == null ? null : d.Image.Path,
                    DisplayName = d.DisplayName,
                    DisplayOrder = d.DisplayOrder,
                    IsActive = d.IsActive
                })
                .ToListAsync();
        }
        public Task<PaymentModeDto> PaymentMode(int Id)
        {
            return _context.PaymentModes
                .Include(m => m.Image)
                 .AsNoTracking()
                 .Where(c => c.Id == Id)
                .Select(d => new PaymentModeDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Image = d.Image == null ? null : d.Image.Path,
                    DisplayName = d.DisplayName,
                    DisplayOrder = d.DisplayOrder,
                    IsActive = d.IsActive

                })
                .FirstOrDefaultAsync();
        }
        public async Task<bool> UpdatePaymentMode(UpdatePaymentMode paymentMode)
        {
            if (!_currentUserService.HavePermission(Permissions.ManagePaymentMode))
                throw new ForbiddenException();
            // Update
            var existing = _context.PaymentModes.FirstOrDefault(c => c.Id == paymentMode.id);
            if (existing == null)
                throw new NotFoundException("Invalid payment mode", "payment mode not found");


            int? fileId = null;

            // Save the image if the paymentMode image is not null
            if (paymentMode?.image != null)
            {
                // Check if the file is image
                if (!paymentMode.image.IsImage())
                    throw new BadRequestException("Only image file is supported");

                fileId = await _fileService.InsertFile(new Files.Models.InsertFileCommand { EntityName = nameof(Data.Entities.Payments.PaymentMode), File = paymentMode.image });

            }

            existing.Description = paymentMode.description;
            existing.DisplayName = paymentMode.displayName;
            existing.DisplayOrder = paymentMode.displayOrder;
            existing.IsActive = paymentMode.isActive;

            // Only update if the image is passed for update
            if (fileId > 0)
                existing.ImageId = fileId ?? 0;

            return (await _context.SaveChangesAsync()) > 0;

        }

        #endregion

    }
}
