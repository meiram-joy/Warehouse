using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Queries.Balance;

public record GetBalanceByResourceIdAndUnitIdQuery(BalanceInputDto Balace) : IRequest<BalanceOutputDto>;