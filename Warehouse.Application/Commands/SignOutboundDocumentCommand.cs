using CSharpFunctionalExtensions;
using MediatR;

namespace Warehouse.Application.Commands;

public record SignOutboundDocumentCommand(Guid DocumentId) : IRequest<Result>;