using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands.Resource;

public record class DeleteResourceCommand(Guid ResourceId) : IRequest<Result<string>>;
