using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands.InboundDocument;

public record CreateInboundDocumentCommand(InboundDocumentInputDto InboundDocument) : IRequest<Result<InboundDocumentOutputDto>>;