using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands;

public record CreateOutboundDocumentCommand( DateTime Date, string DepartmentName, List<OutboundDocumentDto> Items) : IRequest<Result>;