using AutoMapper;
using QRisto.Application.Models.Request.User;
using QRisto.Application.Models.Response.User;
using QRisto.Persistence.Entity;

namespace QRisto.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, LoginResponseModel>();
        
        CreateMap<LoginRequestModel, ApplicationUser>();
        CreateMap<LoginRequestModel, ApplicationUser>();
        
        CreateMap<RegisterRequestModel, ApplicationUser>();
    }
}