using AutoMapper;
using MediatR;
using Warehouse.Application.DTOs;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Queries.Unit;

public class GetAllUnitQueryHandler : IRequestHandler<GetAllUnitQuery,IReadOnlyList<UnitOfMeasurementOutputDto>>
{
    private readonly IMeasurementUnitRepository _unitRepository;
    private readonly IMapper _mapper;
    
    public GetAllUnitQueryHandler(IMeasurementUnitRepository unitRepository, IMapper mapper)
    {
        _unitRepository = unitRepository;
        _mapper = mapper;
    }
    
    public async Task<IReadOnlyList<UnitOfMeasurementOutputDto>> Handle(GetAllUnitQuery request, CancellationToken cancellationToken)
    {
        var unit = await _unitRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<UnitOfMeasurementOutputDto>>(unit);
    }
}