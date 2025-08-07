using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Queries.Unit;

public record GetAllUnitQuery() : IRequest<IReadOnlyList<UnitOfMeasurementOutputDto>>;