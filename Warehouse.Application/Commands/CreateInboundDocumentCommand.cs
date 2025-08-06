using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands;

public record CreateInboundDocumentCommand(string Number, DateTime Date, List<InboundDocumentDto> Items) : IRequest<Result>;