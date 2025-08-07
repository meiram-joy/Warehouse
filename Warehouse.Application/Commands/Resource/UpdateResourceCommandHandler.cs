using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Commands.Resource;

public class UpdateResourceCommandHandler : IRequestHandler<UpdateResourceCommand, Result<string>>
{
    private readonly IResourceRepository _resourceRepository;

    public UpdateResourceCommandHandler(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }

    public async Task<Result<string>> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
    {
        var existingClient = await _resourceRepository.GetByNameAsync(request.Resource.ResourceName,cancellationToken);
        
        if (existingClient != null)
            return Result.Failure<string>("Ресурс с таким именем уже существует.");
        
        var resourceUpdate = Domain.Currency.Entities.Resource.Update(request.Resource.ResourceName);
        resourceUpdate.Active();
        
        await _resourceRepository.UpdateAsync(resourceUpdate, cancellationToken);
        
        return Result.Success("Клиент успешно обновлен.");
    }
}