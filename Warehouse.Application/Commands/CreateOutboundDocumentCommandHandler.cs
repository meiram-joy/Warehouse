using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands;

public class CreateOutboundDocumentCommandHandler : IRequestHandler<CreateOutboundDocumentCommand, Result>
{
    public Task<Result> Handle(CreateOutboundDocumentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}