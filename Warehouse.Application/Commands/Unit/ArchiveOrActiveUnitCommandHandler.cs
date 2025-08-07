using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Domain.Currency.Enum;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Commands.Unit;

public class ArchiveOrActiveUnitCommandHandler : IRequestHandler<ArchiveOrActiveUnitCommand, Result<string>>
{
    private readonly IMeasurementUnitRepository _unitRepository;

    public ArchiveOrActiveUnitCommandHandler(IMeasurementUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task<Result<string>> Handle(ArchiveOrActiveUnitCommand request, CancellationToken cancellationToken)
    {
        var unit =  await _unitRepository.GetByIdAsync(request.UnitId);
        if (unit is null)
            return Result.Failure<string>("Единица измерения не найдена");

        if (request.Status == (int)EntityStatus.Active)
        {
            unit.Active();
        }
        else if (request.Status == (int)EntityStatus.Archived)
        {
            unit.Archive();
        }
        
        await _unitRepository.UpdateAsync(unit, cancellationToken);
        
        return Result.Success("Клиент успешно архивирован");
    }
}