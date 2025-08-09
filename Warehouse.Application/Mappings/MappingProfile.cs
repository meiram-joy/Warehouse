using AutoMapper;
using Warehouse.Application.DTOs;
using Warehouse.Domain.Currency.Aggregates;
using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<InboundDocument, InboundDocumentOutputDto>()
            .ForMember(dest => dest.Items, opt => opt.Ignore()); 
        CreateMap<InboundResource, InboundResourceOutputDto>();
    }

}