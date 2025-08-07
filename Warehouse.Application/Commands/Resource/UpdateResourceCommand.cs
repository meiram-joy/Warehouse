using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;

namespace Warehouse.Application.Commands.Resource;

public record UpdateResourceCommand(ResourceInputDto Resource) : IRequest<Result<string>>;