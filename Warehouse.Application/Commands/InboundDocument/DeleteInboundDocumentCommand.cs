using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands.InboundDocument;

public record DeleteInboundDocumentCommand(Guid InboundDocumentId) : IRequest<Result<string>>;