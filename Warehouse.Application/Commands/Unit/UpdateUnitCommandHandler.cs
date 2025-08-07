using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Commands.Unit;

public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, Result<string>>
{
    private readonly IMeasurementUnitRepository _unitRepository;

    public UpdateUnitCommandHandler(IMeasurementUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task<Result<string>> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        var existingUnit = await _unitRepository.GetByNameAsync(request.Unit.Name,cancellationToken);
        
        if (existingUnit != null)
            return Result.Failure<string>("Еденица с таким именем уже существует.");
        
        var unitUpdate = Domain.Currency.Entities.UnitOfMeasurement.Update(request.Unit.Name);
        unitUpdate.Active();
        
        await _unitRepository.UpdateAsync(unitUpdate, cancellationToken);
        
        return Result.Success("Еденица успешно обновлен.");
    }
}