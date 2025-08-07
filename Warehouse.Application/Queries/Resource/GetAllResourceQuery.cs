using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Queries.Resource;

public record class GetAllResourceQuery() : IRequest<IReadOnlyList<ResourceOutputDto>>;