namespace QRisto.Application.Models.Request.Reservation;

public class ReservationRequestModel
{
    public Guid TableId { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }
}