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
        var (existingUnit, nameExists) = await _unitRepository.GetForUpdateCheckAsync(request.Unit.Name, cancellationToken);
        
        if (nameExists)
            return Result.Failure<string>("В системе уже зарегистрирован Еденица измерения с таким наименованием");
        
        var unit = existingUnit.Update(request.Unit.Name);
        if (unit.IsFailure)
            return Result.Failure<string>("Что-то пошло не так");
        existingUnit.Active();
        
        await _unitRepository.UpdateAsync(existingUnit, cancellationToken);
        
        return Result.Success("Еденица успешно обновлен.");
    }
}