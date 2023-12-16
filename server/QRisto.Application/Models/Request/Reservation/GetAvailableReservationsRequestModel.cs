namespace QRisto.Application.Models.Request.Reservation;

public class GetAvailableReservationsRequestModel
{
    public Guid TableId { get; set; }

    public DateOnly Day { get; set; }
}