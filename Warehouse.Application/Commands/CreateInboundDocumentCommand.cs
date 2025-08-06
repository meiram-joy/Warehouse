using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands;

public record CreateInboundDocumentCommand(InboundDocumentInputDto Request) : IRequest<Result<InboundDocumentOutputDto>>;