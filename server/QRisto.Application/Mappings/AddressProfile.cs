using AutoMapper;
using QRisto.Application.Models.Response.Address;
using QRisto.Persistence.Entity.Provider;

namespace QRisto.Application.Mappings;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
           CreateMap<AddressEntity, AddressGetResponse>();
    }
}