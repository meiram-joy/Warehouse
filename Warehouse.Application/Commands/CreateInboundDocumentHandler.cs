using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands;

public class CreateInboundDocumentHandler : IRequestHandler<CreateInboundDocumentCommand, Result>
{
    public Task<Result> Handle(CreateInboundDocumentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}