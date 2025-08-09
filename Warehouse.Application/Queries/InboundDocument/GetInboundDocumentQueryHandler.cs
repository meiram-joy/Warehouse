using AutoMapper;
using MediatR;
using Warehouse.Application.DTOs;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Queries.InboundDocument;

public class GetInboundDocumentQueryHandler : IRequestHandler<GetInboundDocumentQuery, IReadOnlyList<InboundDocumentOutputDto>>
{
    private readonly IInboundDocumentRepository _inboundDocumentRepository;
    private readonly IInboundResourceRepository _inboundResourceRepository;
    private readonly IMapper _mapper;

    public GetInboundDocumentQueryHandler(IInboundDocumentRepository inboundDocumentRepository, IInboundResourceRepository inboundResourceRepository, IMapper mapper)
    {
        _inboundDocumentRepository = inboundDocumentRepository;
        _inboundResourceRepository = inboundResourceRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<InboundDocumentOutputDto>> Handle(GetInboundDocumentQuery request, CancellationToken cancellationToken)
    {
        var inboundDocuments = await _inboundDocumentRepository.GetAllAsync();
        var inboundResources = await _inboundResourceRepository.GetAllAsync();
        
        var documentDtos = _mapper.Map<List<InboundDocumentOutputDto>>(inboundDocuments);

        foreach (var docDto in documentDtos)
        {
            var relatedResources = inboundResources
                .Where(r => r.DocumentId == docDto.Id)
                .ToList();

            docDto.Items = _mapper.Map<List<InboundResourceOutputDto>>(relatedResources);
        }

        return documentDtos;
        
    }
}