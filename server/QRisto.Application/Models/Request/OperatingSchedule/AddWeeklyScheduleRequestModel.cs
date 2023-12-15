namespace QRisto.Application.Models.Request.OperatingSchedule;

public class AddWeeklyScheduleRequestModel
{
    public Guid ServiceId { get; set; }

    public DateOnly StartDate { get; set; }

    public List<WeeklyScheduleModel> WeeklySchedules { get; set; }

    public List<SpecialDayScheduleModel> SpecialDays { get; set; }
}