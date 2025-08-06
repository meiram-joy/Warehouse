using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands;

public  record CreateBalanceCommand(DateTime Date, string DepartmentName, List<BalanceDto> Items) : IRequest<Result>;