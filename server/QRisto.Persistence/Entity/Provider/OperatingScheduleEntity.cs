namespace QRisto.Persistence.Entity.Provider;

public class OperatingScheduleEntity : IEntity
{
    public ICollection<WorkingIntervalEntity> WorkingIntervals;

    public Guid ServiceId { get; set; }

    public virtual ServiceEntity Service { get; set; }

    public DateOnly Date { get; set; }

    public bool IsWorkingDay { get; set; }

    public string AdditionalInfo { get; set; }

    public Guid Id { get; set; }
    
    public Guid? DeletedBy { get; set; }

    public DateTime CreatedDate { get; set; }
    
    public DateTime? DeletedDate { get; set; }

    public DateTime ModificationDate { get; set; }
}