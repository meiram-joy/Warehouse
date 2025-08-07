using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Commands.Client;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Result<string>>
{
    private readonly IClientRepository _clientRepository;

    public UpdateClientCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    
    public async Task<Result<string>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var existingClient = await _clientRepository.GetByNameAndAddressAsync(request.Client.Name,request.Client.Address,cancellationToken);
        
        if (existingClient != null)
            return Result.Failure<string>("Клиент с таким именем уже существует.");
        
        var clientUpdate = Domain.Currency.Entities.Client.Update(request.Client.Name, request.Client.Address);
        clientUpdate.Active();
        
        await _clientRepository.UpdateAsync(clientUpdate, cancellationToken);
        
        return Result.Success("Клиент успешно обновлен.");
    }
}