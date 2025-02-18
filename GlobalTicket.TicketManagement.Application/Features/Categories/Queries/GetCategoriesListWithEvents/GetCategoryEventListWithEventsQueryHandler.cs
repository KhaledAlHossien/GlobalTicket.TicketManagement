using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GlobalTicket.TicketManagement.Application.Contracts.Persistence;
using GlobalTicket.TicketManagement.Application.Features.Events.Queries.GitEventsList;
using GlobalTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GlobalTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents
{
    public class GetCategoryEventListWithEventsQueryHandler : IRequestHandler<GetCategoryEventListWithEventsQuery, List<CategoryEventListVm>>
    {
       
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

       public GetCategoryEventListWithEventsQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
	}

        public async Task<List<CategoryEventListVm>> Handle(GetCategoryEventListWithEventsQuery request, CancellationToken cancellationToken)
        {
           var list = await _categoryRepository.GetCategoriesWithEvents(request.IncludeHistory);
            return _mapper.Map<List<CategoryEventListVm>>(list);
        }
    }
}
