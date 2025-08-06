using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands;

public class CreateInboundDocumentHandler : IRequestHandler<CreateInboundDocumentCommand, Result<InboundDocumentOutputDto>>
{
    public Task<Result<InboundDocumentOutputDto>> Handle(CreateInboundDocumentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}