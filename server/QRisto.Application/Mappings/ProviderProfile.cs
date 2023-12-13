using AutoMapper;
using QRisto.Application.Models.Request.Provider;
using QRisto.Application.Models.Response.Provider;
using QRisto.Persistence.Entity;

namespace QRisto.Application.Mappings;

public class ProviderProfile : Profile
{
    public ProviderProfile()
    {
        CreateMap<ProviderEntity, ProviderResponse>();
        CreateMap<ProviderPostRequest, ProviderEntity>();

    }
}