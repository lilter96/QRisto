namespace QRisto.Persistence.Entity.Provider;

public class TableEntity : IEntity
{
    public int Seats { get; set; }

    public double LocationX { get; set; }

    public double LocationY { get; set; }

    public double Rotation { get; set; }

    public Guid ServiceId { get; set; }
    public ServiceEntity Service { get; set; }

    public virtual ICollection<ReservationEntity> Reservations { get; set; }

    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModificationDate { get; set; }
}