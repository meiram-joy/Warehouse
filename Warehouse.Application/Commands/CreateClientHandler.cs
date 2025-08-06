using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands;

public class CreateClientHandler : IRequestHandler<CreateClientCommand, Result>
{
    public Task<Result> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}