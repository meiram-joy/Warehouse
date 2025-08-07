using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Commands.Resource;

public class CreateResourceHandler : IRequestHandler<CreateResourceCommand, Result<ResourceOutputDto>>
{
    private readonly IMapper _mapper;
    private readonly IResourceRepository _resourceRepository;

    public CreateResourceHandler(IMapper mapper, IResourceRepository resourceRepository)
    {
        _mapper = mapper;
        _resourceRepository = resourceRepository;
    }

    public async Task<Result<ResourceOutputDto>> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        var existingResource = await _resourceRepository.GetByNameAsync(request.Resource.ResourceName);
        if (existingResource != null)
            return Result.Failure<ResourceOutputDto>("Ресурс с таким именем уже существует.");
        
        var resource = Domain.Currency.Entities.Resource.Create(request.Resource.ResourceName);
        resource.Active();
        
        await _resourceRepository.AddAsync(resource, cancellationToken);
        
        var resourceResult = _mapper.Map<ResourceOutputDto>(resource);
        
        return Result.Success(resourceResult);
    }
}