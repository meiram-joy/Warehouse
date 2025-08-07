using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Warehouse.Application.DTOs;
using Warehouse.Domain.Currency.Interfaces;

namespace Warehouse.Application.Commands.InboundDocument;

public class CreateInboundDocumentCommandHandler : IRequestHandler<CreateInboundDocumentCommand, Result<InboundDocumentOutputDto>>
{
    private readonly IInboundDocumentRepository _inboundDocumentRepository;
    private readonly IInboundResourceRepository _inboundResourceRepository;
    private readonly IBalanceRepository _balanceRepository;
    private readonly IMapper _mapper;

    public CreateInboundDocumentCommandHandler(IInboundResourceRepository inboundResourceRepository, IInboundDocumentRepository inboundDocumentRepository, IBalanceRepository balanceRepository, IMapper mapper)
    {
        _inboundResourceRepository = inboundResourceRepository;
        _inboundDocumentRepository = inboundDocumentRepository;
        _balanceRepository = balanceRepository;
        _mapper = mapper;
    }

    public async Task<Result<InboundDocumentOutputDto>> Handle(CreateInboundDocumentCommand request, CancellationToken cancellationToken)
    {
        var existingDocument  = await _inboundDocumentRepository.GetByDocumentNumberAsync(request.InboundDocument.DocumentNumber,cancellationToken);
        if (existingDocument  != null)
            return Result.Failure<InboundDocumentOutputDto>("В системе уже зарегистрирована накладная с таким номером");
        
        var inboundDocument = Domain.Currency.Aggregates.InboundDocument.Create(request.InboundDocument.DocumentNumber,request.InboundDocument.Date);

        foreach (var item in request.InboundDocument.Items)
        {
            var addItemResult  =  existingDocument .AddItem(item.ResourceId,item.UnitOfMeasurementId,item.Quantity);
            if (addItemResult .IsFailure)
                return Result.Failure<InboundDocumentOutputDto>(addItemResult .Error);
        }
        await _inboundDocumentRepository.AddAsync(inboundDocument,cancellationToken);
        
        foreach (var item in inboundDocument.Items)
        {
            var balance = Domain.Currency.Entities.Balance.Create(item.ResourceId,item.UnitOfMeasurementId,item.Quantity);
            if (balance.IsFailure)
                return Result.Failure<InboundDocumentOutputDto>(balance.Error);
            
            await _inboundResourceRepository.AddAsync(inboundDocument.ID, item, cancellationToken);
            await _balanceRepository.AddAsync(balance.Value, cancellationToken);
        }
        
        var outputDto = _mapper.Map<InboundDocumentOutputDto>(inboundDocument);
        return Result.Success(outputDto);
    }
}