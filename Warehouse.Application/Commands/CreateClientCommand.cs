using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands;

public record CreateClientCommand(ClientInputDto Request): IRequest<Result<ClientOutputDto>>;