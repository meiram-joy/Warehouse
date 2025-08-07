using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Domain.Currency.Enum;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Commands.Client;

public class ArchiveOrActiveClientCommandHandler : IRequestHandler<ArchiveOrActiveClientCommand, Result<string>>
{
    private readonly IClientRepository _clientRepository;

    public ArchiveOrActiveClientCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Result<string>> Handle(ArchiveOrActiveClientCommand request, CancellationToken cancellationToken)
    {
        var client =  await _clientRepository.GetByIdAsync(request.ClientId);
        if (client is null)
            return Result.Failure<string>("Клиент не найден");

        if (request.Status == (int)EntityStatus.Active)
        {
            client.Active();
        }
        else if (request.Status == (int)EntityStatus.Archived)
        {
            client.Archive();
        }
        
        await _clientRepository.UpdateAsync(client, cancellationToken);
        
        return Result.Success("Клиент успешно архивирован");
    }
}