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
        var (existingClient,nameExists) = await _clientRepository.GetForCreateCheckAsync(request.Client.Name,request.Client.Address,cancellationToken);
        
        if (existingClient == null)
            return Result.Failure<string>("Клиент не найден");

        if (nameExists)
            return Result.Failure<string>("В системе уже зарегистрирован клиент с таким наименованием");
        
        var clientUpdate = Domain.Currency.Entities.Client.Update(request.Client.Name, request.Client.Address);
        clientUpdate.Active();
        
        await _clientRepository.UpdateAsync(clientUpdate, cancellationToken);
        
        return Result.Success("Клиент успешно обновлен.");
    }
}