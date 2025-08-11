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
        var (existingResource, nameExists) = await _resourceRepository.GetForUpdateCheckAsync(request.Resource.ResourceName, cancellationToken);

        if (nameExists)
            return Result.Failure<string>("В системе уже зарегистрирован ресурс с таким наименованием");
        
        var resource = existingResource.Update(request.Resource.ResourceName);
        if (resource.IsFailure)
            return Result.Failure<string>(resource.Error);
        
        existingResource.Active();
        
        await _resourceRepository.UpdateAsync(existingResource, cancellationToken);
        
        return Result.Success("Клиент успешно обновлен.");
    }
}