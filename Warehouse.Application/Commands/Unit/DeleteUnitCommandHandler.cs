using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Commands.Unit;

public class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand, Result<string>>
{
    private readonly IMeasurementUnitRepository _unitRepository;

    public DeleteUnitCommandHandler(IMeasurementUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task<Result<string>> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
    {
        var client =  await _unitRepository.GetByIdAsync(request.unitId);
        if (client is null)
            return Result.Failure<string>("Единица измерения не найдена");
        
        bool isUsed = await _unitRepository.IsClientUsedAsync(request.unitId, cancellationToken);

        if (isUsed)
            return Result.Failure<string>("Единица измерения не может быть удален, так как используется в других местах");
        
        await _unitRepository.DeleteAsync(client.ID, cancellationToken);
        
        return Result.Success("Единица измерения была успешно удалена");
    }
}