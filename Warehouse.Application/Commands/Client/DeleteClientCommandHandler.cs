using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Commands.Client;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand,Result<string>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    
    public DeleteClientCommandHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<string>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var client =  await _clientRepository.GetByIdAsync(request.ClientId);
        if (client is null)
            return Result.Failure<string>("Клиент не найден");
        
        bool isUsed = await _clientRepository.IsClientUsedAsync(request.ClientId, cancellationToken);

        if (isUsed)
            return Result.Failure<string>("Клиент не может быть удален, так как используется в других местах");
        
        await _clientRepository.DeleteAsync(client.ID, cancellationToken);
        
        return Result.Success("Клиент был успешно удалён");
    }
}