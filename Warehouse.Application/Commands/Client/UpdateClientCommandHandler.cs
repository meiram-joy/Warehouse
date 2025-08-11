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
        var (existingClient,nameExists) = await _clientRepository.CheckForUpdateAsync(request.Client.Name,request.Client.Address,cancellationToken);

        if (nameExists)
            return Result.Failure<string>("В системе уже зарегистрирован клиент с таким наименованием");
        
        var client = existingClient.Update(request.Client.Name, request.Client.Address);
        if (client.IsFailure)
            return Result.Failure<string>("Что-то пошло не так");
        
        existingClient.Active();
        
        await _clientRepository.UpdateAsync(existingClient, cancellationToken);
        
        return Result.Success("Клиент успешно обновлен.");
    }
}