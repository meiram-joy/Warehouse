using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands.OutboundDocument;

public class CreateOutboundDocumentCommandHandler : IRequestHandler<CreateOutboundDocumentCommand, Result<OutboundDocumentInputDto>>
{
    public Task<Result<OutboundDocumentInputDto>> Handle(CreateOutboundDocumentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}