using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands;

public class CreateBalanceCommandHandler : IRequestHandler<CreateBalanceCommand, Result>
{
    public Task<Result> Handle(CreateBalanceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}