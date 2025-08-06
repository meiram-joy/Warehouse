using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands;

public class CreateUnitHandler : IRequestHandler<CreateUnitCommand,Result>
{
    public Task<Result> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}