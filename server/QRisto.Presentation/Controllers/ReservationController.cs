using Microsoft.AspNetCore.Mvc;
using QRisto.Application.Models.Request.Reservation;
using QRisto.Application.Models.Response.Reservation;
using QRisto.Application.Services.Reservation;
using QRisto.Application.Utils;

namespace QRisto.Presentation.Controllers;

[ApiController]
[Route("api/reservations")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }


    [HttpGet]
    [Route("available")]
    public async Task<IActionResult> GetAvailableReservations(
        [FromQuery] GetAvailableReservationsRequestModel requestModel)
    {
        var result =
            await _reservationService.GetAvailableReservationsAsync(requestModel);

        return result.Match<IActionResult, List<ReservationRangeResponseModel>>(
            Ok,
            BadRequest);
    }
}