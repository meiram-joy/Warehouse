using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Queries.Balance;

public record GetAllBalanceQuery(BalanceInputDto Balance) : IRequest<IReadOnlyList<BalanceOutputDto>>, IRequest<BalanceOutputDto>;