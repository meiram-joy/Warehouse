using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands.Resource;

public record ArchiveOrActiveResourceCommand(Guid ResourceId, int Status) : IRequest<Result<string>>;