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
        var (existingResource, nameExists) = await _resourceRepository.GetForCreateCheckAsync(request.Resource.ResourceName, cancellationToken);
        
        if (existingResource == null)
            return Result.Failure<string>("Ресурс не найден");

        if (nameExists)
            return Result.Failure<string>("В системе уже зарегистрирован ресурс с таким наименованием");
        
        var resourceUpdate = Domain.Currency.Entities.Resource.Update(request.Resource.ResourceName);
        resourceUpdate.Active();
        
        await _resourceRepository.UpdateAsync(resourceUpdate, cancellationToken);
        
        return Result.Success("Клиент успешно обновлен.");
    }
}