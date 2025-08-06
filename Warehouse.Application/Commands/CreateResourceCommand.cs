using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands;

public record CreateResourceCommand(string resourceName) : IRequest<Result>;