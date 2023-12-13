using AutoMapper;
using QRisto.Application.Models.Request.User;
using QRisto.Persistence.Entity;

namespace QRisto.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<LoginRequestModel, ApplicationUser>(MemberList.Source);
        CreateMap<LoginRequestModel, ApplicationUser>(MemberList.Destination);
        CreateMap<RegisterRequestModel, ApplicationUser>(MemberList.Source);
    }
}