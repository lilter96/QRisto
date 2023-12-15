using AutoMapper;
using QRisto.Application.Models.Request.Service;
using QRisto.Application.Models.Response.Service;
using QRisto.Persistence.Entity.Provider;

namespace QRisto.Application.Mappings;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<CreateServiceRequestModel, ServiceEntity>();
        CreateMap<ServiceEntity, ServiceResponseModel>();
    }
}