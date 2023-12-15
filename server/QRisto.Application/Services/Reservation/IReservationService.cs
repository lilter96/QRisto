using QRisto.Application.Models.Request.Reservation;
using QRisto.Application.Models.Response.Reservation;
using QRisto.Application.Utils;

namespace QRisto.Application.Services.Reservation;

public interface IReservationService
{
    Task<Result<List<ReservationRangeResponseModel>>> GetAvailableReservationsAsync(
        GetAvailableReservationsRequestModel model);
}