namespace QRisto.Persistence.Entity.Provider;

public class WorkingIntervalEntity : IEntity
{
    public Guid OperatingScheduleId { get; set; }

    public virtual OperatingScheduleEntity OperatingSchedule { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public IntervalType Type { get; set; }

    public Guid Id { get; set; }
    
    public Guid DeletedBy { get; set; }

    public DateTime CreatedDate { get; set; }
    
    public DateTime? DeletedDate { get; set; }

    public DateTime ModificationDate { get; set; }
}