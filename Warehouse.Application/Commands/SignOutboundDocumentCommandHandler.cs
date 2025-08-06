using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands;

public class SignOutboundDocumentCommandHandler : IRequestHandler<SignOutboundDocumentCommand, Result>
{
    public Task<Result> Handle(SignOutboundDocumentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}