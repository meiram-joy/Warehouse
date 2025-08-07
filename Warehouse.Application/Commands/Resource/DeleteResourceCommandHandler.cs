using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Commands.Resource;

public class DeleteResourceCommandHandler : IRequestHandler<DeleteResourceCommand, Result<string>>
{
    private readonly IResourceRepository _resourceRepository;

    public DeleteResourceCommandHandler(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }

    public async Task<Result<string>> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
    {
        var resource =  await _resourceRepository.GetByIdAsync(request.ResourceId, cancellationToken);
        if (resource is null)
            return Result.Failure<string>("Ресурс не найден");
        
        bool isUsed = await _resourceRepository.IsClientUsedAsync(request.ResourceId, cancellationToken);
        
        if (isUsed)
            return Result.Failure<string>("Ресурс используется клиентами и не может быть удален");
        
        await _resourceRepository.DeleteAsync(resource.ID, cancellationToken);
        
        return Result.Success("Ресурс был успешно удалён");
    }
}