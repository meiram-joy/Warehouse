using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands.OutboundDocument;

public record CreateOutboundDocumentCommand(List<OutboundDocumentInputDto> Request) : IRequest<Result<OutboundDocumentInputDto>>;