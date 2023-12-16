using AutoMapper;
using QRisto.Application.Models.Response.Reservation;
using QRisto.Persistence.Entity.Provider;

namespace QRisto.Application.Mappings;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<ReservationEntity, ReservationResponseModel>()
            .ForMember(x => x.Table, y => y.MapFrom(d => d.Table.Name));
    }
}