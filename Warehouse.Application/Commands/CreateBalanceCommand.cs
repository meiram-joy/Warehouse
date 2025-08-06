using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands;

public record CreateBalanceCommand(BalanceInputDto Item) : IRequest<Result<BalanceOutputDto>>;