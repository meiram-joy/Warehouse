using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands;

public record CreateOutboundDocumentCommand(OutboundDocumentInputDto Request) : IRequest<Result<OutboundDocumentInputDto>>;