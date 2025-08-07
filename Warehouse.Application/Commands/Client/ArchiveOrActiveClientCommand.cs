using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Domain.Currency.Enum;

namespace Warehouse.Application.Commands.Client;

public record class ArchiveOrActiveClientCommand(Guid ClientId, int Status) : IRequest<Result<string>>;