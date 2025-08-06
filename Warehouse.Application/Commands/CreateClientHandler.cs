using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands;

public class CreateClientHandler : IRequestHandler<CreateClientCommand, Result<ClientOutputDto>>
{
    public Task<Result<ClientOutputDto>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}