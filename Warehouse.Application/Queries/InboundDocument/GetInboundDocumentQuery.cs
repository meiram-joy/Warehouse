using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Queries.InboundDocument;

public record GetInboundDocumentQuery() : IRequest<IReadOnlyList<InboundDocumentOutputDto>>;