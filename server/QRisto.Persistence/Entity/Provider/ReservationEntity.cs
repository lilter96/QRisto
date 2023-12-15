namespace QRisto.Persistence.Entity.Provider;

public class ReservationEntity : IEntity
{
    public Guid TableId { get; set; }

    public virtual TableEntity Table { get; set; }

    public DateTime ReservationTime { get; set; }

    public int DurationInMinutes { get; set; }

    public Guid? ReservationDetailsId { get; set; }

    public virtual ReservationDetailsEntity ReservationDetails { get; set; }

    public ReservationStatus Status { get; set; }

    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModificationDate { get; set; }
}