﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GlobalTicket.TicketManagement.Application.Contracts.Infrastructure;
using GlobalTicket.TicketManagement.Application.Contracts.Persistence;
using GlobalTicket.TicketManagement.Application.Models.Mail;
using GlobalTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GlobalTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CreateEventCommandHandler(IMapper mapper, IEventRepository eventRepository,IEmailService emailService )
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _emailService = emailService;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var @event = _mapper.Map<Event>(request);

            var validator = new CreateEventCommandValidator(_eventRepository);

            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            @event = await _eventRepository.AddAsync(@event);


            // Sending email to notification address
            var email = new Email() 
            { 
                To = "hossink131@gmail.com",
                Body = $"A new event was created {request}",
                Subject = "A new event was created" 
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {

            }

            return @event.EventId;
        }
    }

}
