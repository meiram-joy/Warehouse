using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands;

public record CreateClientCommand(string Name, string Address): IRequest<Result>;