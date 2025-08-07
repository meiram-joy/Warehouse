using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Domain.Currency.Enum;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Commands.Resource;

public class ArchiveOrActiveResourceCommandHandler : IRequestHandler<ArchiveOrActiveResourceCommand, Result<string>>
{
    private readonly IResourceRepository _resourceRepository;

    public ArchiveOrActiveResourceCommandHandler(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }


    public async Task<Result<string>> Handle(ArchiveOrActiveResourceCommand request, CancellationToken cancellationToken)
    {
        var resource =  await _resourceRepository.GetByIdAsync(request.ResourceId);
        if (resource is null)
            return Result.Failure<string>("Клиент не найден");

        if (request.Status == (int)EntityStatus.Active)
        {
            resource.Active();
        }
        else if (request.Status == (int)EntityStatus.Archived)
        {
            resource.Archive();
        }
        
        await _resourceRepository.UpdateAsync(resource, cancellationToken);
        
        return Result.Success("Клиент успешно архивирован");
    }
}