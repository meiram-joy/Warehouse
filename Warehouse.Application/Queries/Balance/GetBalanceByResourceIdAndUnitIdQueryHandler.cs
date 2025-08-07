using AutoMapper;
using MediatR;
using Warehouse.Application.DTOs;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Queries.Balance;

public class GetBalanceByResourceIdAndUnitIdQueryHandler : IRequestHandler<GetBalanceByResourceIdAndUnitIdQuery, BalanceOutputDto>
{
    private readonly IBalanceRepository _ibalanceRepository;
    private readonly IMapper _mapper;

    public GetBalanceByResourceIdAndUnitIdQueryHandler(IBalanceRepository ibalanceRepository, IMapper mapper)
    {
        _ibalanceRepository = ibalanceRepository;
        _mapper = mapper;
    }
    public async Task<BalanceOutputDto> Handle(GetBalanceByResourceIdAndUnitIdQuery request, CancellationToken cancellationToken)
    {
        var existingBalance = await _ibalanceRepository.GetByResourceIdAndUnitIdAsync(request.Balace.ResourceId, request.Balace.UnitOfMeasurementId);
        if (existingBalance == null)
            throw new KeyNotFoundException($"Balance not found for ResourceId: {request.Balace.ResourceId} and UnitOfMeasurementId: {request.Balace.UnitOfMeasurementId}");
        
        return _mapper.Map<BalanceOutputDto>(existingBalance);
    }
}