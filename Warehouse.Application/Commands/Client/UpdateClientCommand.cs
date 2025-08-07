using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands.Client;

public record class UpdateClientCommand(ClientInputDto Client) : IRequest<Result<string>>;
