using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands.Unit;

public record ArchiveOrActiveUnitCommand(Guid UnitId, int Status) : IRequest<Result<string>>;