using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands.Resource;

public record CreateResourceCommand(ResourceInputDto Resource) : IRequest<Result<ResourceOutputDto>>;