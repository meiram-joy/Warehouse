using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands;

public record CreateUnitCommand(UnitOfMeasurementInputDto Request) : IRequest<Result<UnitOfMeasurementOutputDto>>;