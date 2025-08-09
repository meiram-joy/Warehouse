using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Commands.InboundDocument;

public class DeleteInboundDocumentCommandHandler : IRequestHandler<DeleteInboundDocumentCommand, Result<string>>
{
    private readonly IInboundDocumentRepository _inboundDocumentRepository;
    private readonly IInboundResourceRepository _inboundResourceRepository;
    private readonly IBalanceRepository _balanceRepository;

    public DeleteInboundDocumentCommandHandler(IInboundDocumentRepository inboundDocumentRepository, IInboundResourceRepository inboundResourceRepository, IBalanceRepository balanceRepository)
    {
        _inboundDocumentRepository = inboundDocumentRepository;
        _inboundResourceRepository = inboundResourceRepository;
        _balanceRepository = balanceRepository;
    }

    public async Task<Result<string>> Handle(DeleteInboundDocumentCommand request, CancellationToken cancellationToken)
    {
        if (request.InboundDocumentId == Guid.Empty)
            throw new ArgumentNullException(nameof(request.InboundDocumentId));
        
        var existingDocument  = await _inboundDocumentRepository.GetByIdAsync(request.InboundDocumentId,cancellationToken);
        if (existingDocument  == null )
            return Result.Failure<string>("Документ поступления не найден");
        
        var inboundResources = await _inboundResourceRepository.GetByInboundDocumentIdAsync(request.InboundDocumentId,cancellationToken);
        if (inboundResources == null || !inboundResources.Any())
            return Result.Failure<string>("Ресурсы поступления не найдены");
        
        foreach (var resource in inboundResources)
        {
            var balance = await _balanceRepository.GetByResourceIdAndUnitIdAsync(resource.ID,resource.UnitOfMeasurementId, cancellationToken);
            if (balance == null)
                return Result.Failure<string>("Баланс не найден");
        
            var result =  existingDocument!.RemoveItem(resource.ID);
            if (result.IsFailure)
                return Result.Failure<string>(result.Error);
            
            await _inboundResourceRepository.DeleteAsync(resource.ID, cancellationToken);
            await _balanceRepository.DeleteAsync(balance.ID, cancellationToken);
        }
        await _inboundDocumentRepository.DeleteAsync(existingDocument.ID, cancellationToken);
        
        return Result.Success("Документ поступления успешно удалён");
    }
}