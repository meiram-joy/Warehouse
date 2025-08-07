using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Commands.Unit;

public class CreateUnitHandler : IRequestHandler<CreateUnitCommand,Result<UnitOfMeasurementOutputDto>>
{
    private readonly IMapper _mapper;
    private readonly IMeasurementUnitRepository _unitRepository;

    public CreateUnitHandler(IMeasurementUnitRepository unitRepository, IMapper mapper)
    {
        _unitRepository = unitRepository;
        _mapper = mapper;
    }

    public async Task<Result<UnitOfMeasurementOutputDto>> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        var existingUnit = await _unitRepository.GetByNameAsync(request.Unit.Name);
        if (existingUnit != null)
            return Result.Failure<UnitOfMeasurementOutputDto>("Еденица с таким именем уже существует.");
        
        var unit = Domain.Currency.Entities.UnitOfMeasurement.Create(request.Unit.Name);
        unit.Active();
        
        await _unitRepository.AddAsync(unit, cancellationToken);
        
        var unitOutput = _mapper.Map<UnitOfMeasurementOutputDto>(unit);
        return Result.Success(unitOutput);
    }
}