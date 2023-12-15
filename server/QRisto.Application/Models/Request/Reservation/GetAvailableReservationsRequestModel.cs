namespace QRisto.Application.Models.Request.Reservation;

public class GetAvailableReservationsRequestModel
{
    public Guid ServiceId { get; set; }

    public DateOnly Day { get; set; }
}