using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands;

public record CreateUnitCommand(string UnitName) : IRequest<Result>;