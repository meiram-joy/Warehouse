using AutoMapper;
using MediatR;
using Warehouse.Application.DTOs;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Queries.Balance;

public class GetAllBalanceQueryHandler : IRequestHandler<GetAllBalanceQuery, IReadOnlyList<BalanceOutputDto>>
{
    private readonly IBalanceRepository _ibalanceRepository;
    private readonly IMapper _mapper;
    
    public GetAllBalanceQueryHandler(IBalanceRepository ibalanceRepository, IMapper mapper)
    {
        _ibalanceRepository = ibalanceRepository;
        _mapper = mapper;
    }
    
    public async Task<IReadOnlyList<BalanceOutputDto>> Handle(GetAllBalanceQuery request, CancellationToken cancellationToken)
    {
        var existingBalance = await _ibalanceRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<BalanceOutputDto>>(existingBalance);//Todo замапить в DI
    }
}