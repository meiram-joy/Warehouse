using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;
using Warehouse.Domain.Currency.Interfaces;
using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Application.Commands.Client;

public class CreateClientHandler : IRequestHandler<CreateClientCommand, Result<ClientOutputDto>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    public CreateClientHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }
    public async Task<Result<ClientOutputDto>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var nameExists = await _clientRepository.CheckForCreateAsync(request.Client.Name,request.Client.Address,cancellationToken);
        
        if (nameExists)
            return Result.Failure<ClientOutputDto>("В системе уже зарегистрирован клиент с таким наименованием");
        
        var client = Domain.Currency.Entities.Client.Create(request.Client.Name, request.Client.Address);
        client.Active();
        
        await _clientRepository.AddAsync(client, cancellationToken);
        
        var clientOutput = _mapper.Map<ClientOutputDto>(client);
        return Result.Success(clientOutput);
    }
}