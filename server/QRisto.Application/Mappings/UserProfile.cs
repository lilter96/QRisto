using AutoMapper;
using QRisto.Application.Models.Request.User;
using QRisto.Application.Models.Response.User;
using QRisto.Persistence.Entity;
using QRisto.Persistence.Entity.Auth;

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