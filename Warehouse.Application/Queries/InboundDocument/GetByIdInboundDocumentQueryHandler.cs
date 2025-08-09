using AutoMapper;
using MediatR;
using Warehouse.Application.DTOs;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Queries.InboundDocument;

public class GetByIdInboundDocumentQueryHandler :  IRequestHandler<GetByIdInboundDocumentQuery, IReadOnlyList<InboundDocumentOutputDto>>
{
    private readonly IInboundDocumentRepository _inboundDocumentRepository;
    private readonly IInboundResourceRepository _inboundResourceRepository;
    private readonly IMapper _mapper;

    public GetByIdInboundDocumentQueryHandler(IInboundDocumentRepository inboundDocumentRepository, IInboundResourceRepository inboundResourceRepository, IMapper mapper)
    {
        _inboundDocumentRepository = inboundDocumentRepository;
        _inboundResourceRepository = inboundResourceRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<InboundDocumentOutputDto>> Handle(GetByIdInboundDocumentQuery request, CancellationToken cancellationToken)
    {
        if (request.InboundDocumentId == Guid.Empty)
            throw new  ArgumentNullException(nameof(request.InboundDocumentId));
        
        var document = await _inboundDocumentRepository.GetByIdAsync(request.InboundDocumentId);
        if (document == null)
            throw new  InvalidOperationException($"Не был найден документ по id {request.InboundDocumentId}");
        var resources = await _inboundResourceRepository.GetByInboundDocumentIdAsync(document.ID);
        
        var dto = _mapper.Map<InboundDocumentOutputDto>(document);
        dto.Resources = _mapper.Map<List<InboundResourceOutputDto>>(resources);

        return new List<InboundDocumentOutputDto> { dto }  ;
    }
}