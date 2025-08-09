using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Queries.InboundDocument;

public record class GetByIdInboundDocumentQuery(Guid InboundDocumentId) : IRequest<IReadOnlyList<InboundDocumentOutputDto>>;