using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands.InboundDocument;

public record class UpdateInboundDocumentCommand(InboundDocumentInputDto InboundDocument) : IRequest<Result<string>>;