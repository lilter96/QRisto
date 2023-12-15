namespace QRisto.Application.Models.Request.OperatingSchedule;

public class WeeklyScheduleModel
{
    public DayOfWeek DayOfWeek { get; set; }

    public bool IsWorkingDay => DayOfWeek != DayOfWeek.Sunday && DayOfWeek != DayOfWeek.Saturday;
    public List<TimeSlotModel> WorkingHours { get; set; }
}