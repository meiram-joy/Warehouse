using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Queries.Resource;

public class GetAllResourceQueryHandler : IRequestHandler<GetAllResourceQuery, IReadOnlyList<ResourceOutputDto>>
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IMapper _mapper;

    public GetAllResourceQueryHandler(IResourceRepository resourceRepository, IMapper mapper)
    {
        _mapper = mapper;
        _resourceRepository = resourceRepository;
    }

    public async Task<IReadOnlyList<ResourceOutputDto>> Handle(GetAllResourceQuery request, CancellationToken cancellationToken)
    {
        var clients = await _resourceRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<ResourceOutputDto>>(clients);
    }
}