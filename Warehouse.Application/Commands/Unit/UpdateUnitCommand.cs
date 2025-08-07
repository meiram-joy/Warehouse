using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands.Unit;

public record class UpdateUnitCommand(UnitOfMeasurementInputDto Unit) : IRequest<Result<string>>;