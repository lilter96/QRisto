namespace QRisto.Persistence.Entity.Provider;

public class ReservationDetailsEntity : IEntity
{
    public string CustomerName { get; set; }

    public string CustomerPhone { get; set; }

    public string? CustomerEmail { get; set; }

    public Guid ReservationDetailsId { get; set; }
    public virtual ReservationEntity Reservation { get; set; }
    
    public Guid Id { get; set; }
    
    public Guid? DeletedBy { get; set; }

    public DateTime CreatedDate { get; set; }
    
    public DateTime? DeletedDate { get; set; }

    public DateTime ModificationDate { get; set; }
}