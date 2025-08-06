using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands;

public class CreateBalanceCommandHandler : IRequestHandler<CreateBalanceCommand, Result<BalanceOutputDto>>
{
    public Task<Result<BalanceOutputDto>> Handle(CreateBalanceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}