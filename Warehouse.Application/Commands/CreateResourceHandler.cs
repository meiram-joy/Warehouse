using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands;

public class CreateResourceHandler : IRequestHandler<CreateResourceCommand, Result<ResourceOutputDto>>
{
    public Task<Result<ResourceOutputDto>> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}