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
        if (request.InboundDocumentId == null)
            throw new ArgumentNullException(nameof(request.InboundDocumentId));
        
        var existingDocument  = await _inboundDocumentRepository.GetByIdAsync(request.InboundDocumentId,cancellationToken);
        if (existingDocument  == null )
            return Result.Failure<string>("Документ поступления не найден");
        
        var inboundResources = await _inboundResourceRepository.GetByInboundDocumentIdAsync(request.InboundDocumentId,cancellationToken);
        if (inboundResources == null)
            return Result.Failure<string>("Ресурсы поступления не найдены");
        
        var balance = await _balanceRepository.GetByResourceIdAndUnitIdAsync(inboundResources!.ResourceId,inboundResources.UnitOfMeasurementId, cancellationToken);
        if (balance == null)
            return Result.Failure<string>("Баланс не найден");
        
        var result =  existingDocument!.RemoveItem(inboundResources!.ID);
        if (result.IsFailure)
            return Result.Failure<string>(result.Error);
        
        await _inboundDocumentRepository.DeleteAsync(existingDocument.ID, cancellationToken);
        await _inboundResourceRepository.DeleteAsync(inboundResources.ID, cancellationToken);
        await _balanceRepository.DeleteAsync(balance.ID, cancellationToken);
        
        return Result.Success("Документ поступления успешно удалён");
    }
}