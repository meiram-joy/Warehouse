using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands.Unit;

public record DeleteUnitCommand(Guid unitId) : IRequest<Result<string>>;