using Newtonsoft.Json;
using PreSchool.Application.Events;
using PreSchool.Application.Services.Emails;
using PreSchool.EmailTemplates;
using PreSchool.Application.Infastructures;
using PreSchool.EmailTemplates.ViewModels;

namespace PreSchool.Application.Services.Tickets.Events
{
    public class TicketConsumer : IEventConsumer<TicketOpenEvent> ,
                                   IEventConsumer<TicketReplyEvent>


    {
        private readonly IEmailService _emailService;
            private readonly IRazorViewToStringRenderer _renderer;
            private readonly ICurrentUserService _currentUserService;

        public TicketConsumer(
                IEmailService emailService,
                IRazorViewToStringRenderer renderer,
                  ICurrentUserService currentUserService


                )
        {
                _emailService = emailService;
                _renderer = renderer;
                _currentUserService = currentUserService;

        }


        public void HandleEvent(TicketOpenEvent eventMessage, EventSender sender, object parameter = null)
        {

            var emailVm = new TicketOpenEmailViewModel(
                          eventMessage.Ticket.FullName, 
                          eventMessage.Ticket.TicketNumber,
                          eventMessage.Ticket.CreatedOn,
                          eventMessage.Ticket.Message,
                          eventMessage.Ticket.Email
                          );

            _emailService.TicketOpen(emailVm);
        }

        public void HandleEvent(TicketReplyEvent eventMessage, EventSender sender, object parameter = null)
        {

            var ticketReplyEmailViewModel = new TicketReplyEmailViewModel(
                          eventMessage.Ticket.FullName,
                          eventMessage.Ticket.TicketNumber,
                          eventMessage.Ticket.CreatedOn,
                          eventMessage.Message,
                          eventMessage.Ticket.StatusId.ToNameString(),
                          eventMessage.Ticket.Email
                          );

            _emailService.TicketReply(ticketReplyEmailViewModel);
        }

    }


}
