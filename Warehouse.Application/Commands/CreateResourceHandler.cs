using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands;

public class CreateResourceHandler : IRequestHandler<CreateResourceCommand, Result>
{
    public Task<Result> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}