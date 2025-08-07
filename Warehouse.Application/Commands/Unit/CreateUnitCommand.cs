using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands.Unit;

public record CreateUnitCommand(UnitOfMeasurementInputDto Unit) : IRequest<Result<UnitOfMeasurementOutputDto>>;