using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands.Client;

public record class DeleteClientCommand(Guid ClientId) : IRequest<Result<string>>;