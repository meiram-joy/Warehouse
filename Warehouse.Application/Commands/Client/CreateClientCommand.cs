using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands.Client;

public record CreateClientCommand(ClientInputDto Client): IRequest<Result<ClientOutputDto>>;