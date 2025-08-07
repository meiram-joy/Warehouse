using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Queries.Client;

public record class GetAllClientQuery() : IRequest<IReadOnlyList<ClientOutputDto>>;