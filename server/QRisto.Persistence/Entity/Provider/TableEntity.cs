namespace QRisto.Persistence.Entity.Provider;

public class TableEntity : IEntity
{
    public string Name { get; set; }

    public int Seats { get; set; }

    public double LocationX { get; set; }

    public double LocationY { get; set; }

    public double Rotation { get; set; }

    public Guid ServiceId { get; set; }

    public ServiceEntity Service { get; set; }

    public virtual ICollection<ReservationEntity> Reservations { get; set; }

    public Guid Id { get; set; }
    
    public Guid? DeletedBy { get; set; }

    public DateTime CreatedDate { get; set; }
    
    public DateTime? DeletedDate { get; set; }

    public DateTime ModificationDate { get; set; }
}