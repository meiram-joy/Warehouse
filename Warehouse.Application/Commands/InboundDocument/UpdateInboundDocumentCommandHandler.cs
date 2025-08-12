using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Commands.InboundDocument;

public class UpdateInboundDocumentCommandHandler : IRequestHandler<UpdateInboundDocumentCommand, Result<string>>
{
    private readonly IInboundDocumentRepository _inboundDocumentRepository;
    private readonly IInboundResourceRepository _inboundResourceRepository;
    private readonly IBalanceRepository _balanceRepository;

    public UpdateInboundDocumentCommandHandler(IInboundDocumentRepository inboundDocumentRepository, IInboundResourceRepository inboundResourceRepository, IBalanceRepository balanceRepository)
    {
        _inboundDocumentRepository = inboundDocumentRepository;
        _inboundResourceRepository = inboundResourceRepository;
        _balanceRepository = balanceRepository;
    }

    public async Task<Result<string>> Handle(UpdateInboundDocumentCommand request, CancellationToken cancellationToken)
    {
        var (documentToUpdate, numberExists) = await _inboundDocumentRepository
            .GetForUpdateCheckAsync(request.InboundDocument.Id, request.InboundDocument.DocumentNumber, cancellationToken);

        if (numberExists)
            return Result.Failure<string>("В системе уже зарегистрирована накладная с таким номером");
        
        documentToUpdate!.Update(request.InboundDocument.Id,request.InboundDocument.DocumentNumber,request.InboundDocument.Date);
        
        foreach (var item in request.InboundDocument.Items)
        {
            var addItemResult  =  documentToUpdate!.UpdateInboundResource(item.ResourceId,item.UnitOfMeasurementId,item.Quantity);
            if (addItemResult .IsFailure)
                return Result.Failure<string>(addItemResult .Error);
        }
        await _inboundDocumentRepository.UpdateAsync(documentToUpdate,cancellationToken);
        
        foreach (var item in documentToUpdate.Items)
        {
            var balance = await _balanceRepository.GetByResourceIdAndUnitIdAsync(item.ID,item.UnitOfMeasurementId, cancellationToken);
            if (balance == null)
                return Result.Failure<string>("Баланс для ресурса не найден");
            
            var updatedBalance =  balance.Update(item.ID,item.UnitOfMeasurementId,item.Quantity);
            if (updatedBalance.IsFailure)
                return Result.Failure<string>(updatedBalance.Error);
            
            await _inboundResourceRepository.UpdateAsync(documentToUpdate.ID, item, cancellationToken);
            await _balanceRepository.UpdateAsync(balance, cancellationToken);
        }
        return Result.Success("Накладная успешно обновлена");
    }
}