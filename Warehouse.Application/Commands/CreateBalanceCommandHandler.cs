using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;
using Warehouse.Domain.Currency.Entities;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Commands;

public class CreateBalanceCommandHandler : IRequestHandler<CreateBalanceCommand, Result<BalanceOutputDto>>
{
    private readonly IBalanceRepository _ibalanceRepository;
    private readonly IMapper _iMapper;
    public CreateBalanceCommandHandler(IBalanceRepository ibalanceRepository, IMapper iMapper)
    {
        _ibalanceRepository = ibalanceRepository;
        _iMapper = iMapper;
    }
    
    public async Task<Result<BalanceOutputDto>> Handle(CreateBalanceCommand request, CancellationToken cancellationToken)
    {
        var existingBalance = await _ibalanceRepository.GetByIdAsync(request.Item.ResourceId);//Todo Исправить искать по ResourceId и UnitOfMeasurementId
        if (existingBalance is not null )
        {
            existingBalance.Increase(request.Item.Quantity);
            await _ibalanceRepository.UpdateAsync(existingBalance);
            
            var resultDto = _iMapper.Map<BalanceOutputDto>(existingBalance);// Todo Надо будет добавить DI
            
            return Result.Success(resultDto);
        }
        var newBalance = Balance.Create(request.Item.ResourceId, request.Item.UnitOfMeasurementId, request.Item.Quantity);
        
        if (newBalance.IsFailure)
            throw new InvalidOperationException(newBalance.Error);
        
        await _ibalanceRepository.AddAsync(newBalance.Value, cancellationToken);
        
        var resultBalance = _iMapper.Map<BalanceOutputDto>(newBalance.Value);
        return Result.Success(resultBalance);
    }
}