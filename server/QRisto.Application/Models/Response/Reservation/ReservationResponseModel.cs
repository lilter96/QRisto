using QRisto.Persistence.Entity.Provider;

namespace QRisto.Application.Models.Response.Reservation;

public class ReservationResponseModel
{
    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public ReservationStatus Status { get; set; }

    public string Table { get; set; }
}