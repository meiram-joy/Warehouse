using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands;

public record CreateResourceCommand(ResourceInputDto Request) : IRequest<Result<ResourceOutputDto>>;