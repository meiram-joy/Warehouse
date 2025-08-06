using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands;

public class CreateUnitHandler : IRequestHandler<CreateUnitCommand,Result<UnitOfMeasurementOutputDto>>
{
    public Task<Result<UnitOfMeasurementOutputDto>> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}