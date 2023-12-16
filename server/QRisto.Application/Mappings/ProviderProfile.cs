using AutoMapper;
using QRisto.Application.Models.Request.Provider;
using QRisto.Application.Models.Request.Reservation;
using QRisto.Application.Models.Response.Provider;
using QRisto.Persistence.Entity;
using QRisto.Persistence.Entity.Provider;

namespace QRisto.Application.Mappings;

public class ProviderProfile : Profile
{
    public ProviderProfile()
    {
        CreateMap<ProviderEntity, ProviderGetResponse>();
        CreateMap<ProviderGetListRequest, ProviderEntity>();
        
        CreateMap<ProviderEntity, ProviderDetailsGetResponse>();
        CreateMap<ProviderEntity, ProviderGetResponse>();
        CreateMap<ProviderPostRequest, ProviderEntity>();
    }
}