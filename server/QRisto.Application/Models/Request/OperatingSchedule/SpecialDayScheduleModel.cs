namespace QRisto.Application.Models.Request.OperatingSchedule;

public class SpecialDayScheduleModel
{
    public DateOnly Date { get; set; }

    public bool IsClosed { get; set; }

    public List<TimeSlotModel> WorkingHours { get; set; }
}