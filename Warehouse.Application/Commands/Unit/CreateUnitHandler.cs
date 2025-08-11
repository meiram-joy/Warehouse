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
        var nameExists = await _unitRepository.GetForCreateCheckAsync(request.Unit.Name, cancellationToken);
        
        if (nameExists)
            return Result.Failure<UnitOfMeasurementOutputDto>("В системе уже зарегистрирован Еденица измерения с таким наименованием");
        
        var unit = Domain.Currency.Entities.UnitOfMeasurement.Create(request.Unit.Name);
        unit.Active();
        
        await _unitRepository.AddAsync(unit, cancellationToken);
        
        var unitOutput = _mapper.Map<UnitOfMeasurementOutputDto>(unit);
        return Result.Success(unitOutput);
    }
}