using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Queries.Client;

public class GetAllClientQueryHandler : IRequestHandler<GetAllClientQuery, IReadOnlyList<ClientOutputDto>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    
    public GetAllClientQueryHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
    }
    
    public async Task<IReadOnlyList<ClientOutputDto>> Handle(GetAllClientQuery request, CancellationToken cancellationToken)
    {
        var clients = await _clientRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<ClientOutputDto>>(clients);
    }
}