using AutoMapper;
using QRisto.Application.Models.Request.OperatingSchedule;
using QRisto.Application.Models.Request.Service;
using QRisto.Application.Models.Response.Service;
using QRisto.Application.Utils;
using QRisto.Persistence.Entity.Provider;
using QRisto.Persistence.Repositories.Implementations;

namespace QRisto.Application.Services.Service;

public class ServiceService : IServiceService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public ServiceService(
        UnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<ServiceResponseModel>> CreateAsync(CreateServiceRequestModel model)
    {
        try
        {
            var service = _mapper.Map<ServiceEntity>(model);

            var createdService = await _unitOfWork.ServiceRepository.InsertAsync(service);
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<ServiceResponseModel>(createdService);
            return Result<ServiceResponseModel>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<ServiceResponseModel>.Failure(ex.ToString());
        }
    }

    public async Task<Result> AddNewScheduleAsync(AddWeeklyScheduleRequestModel model)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var endDate = model.StartDate.AddYears(1);

            for (var date = model.StartDate; date < endDate; date = date.AddDays(1))
            {
                var isSpecialDay = model.SpecialDays.FirstOrDefault(s => s.Date == date);

                if (isSpecialDay != null)
                {
                    continue;
                }

                var weeklySchedule = model.WeeklySchedules.FirstOrDefault(w => w.DayOfWeek == date.DayOfWeek);

                var operatingSchedule = new OperatingScheduleEntity
                {
                    ServiceId = model.ServiceId,
                    Date = date,
                    IsWorkingDay = weeklySchedule?.IsWorkingDay ?? false
                };

                if (operatingSchedule.IsWorkingDay)
                {
                    var workingHours = weeklySchedule?.WorkingHours;
                    operatingSchedule.WorkingIntervals = workingHours?
                        .Select(
                            hours => new WorkingIntervalEntity
                            {
                                StartTime = hours.StartTime,
                                EndTime = hours.EndTime,
                                Type = IntervalType.Work
                            }).ToList();
                }

                await _unitOfWork.OperatingScheduleRepository.InsertAsync(operatingSchedule);
            }

            foreach (var specialDay in model.SpecialDays)
            {
                var specialDaySchedule = new OperatingScheduleEntity
                {
                    ServiceId = model.ServiceId,
                    Date = specialDay.Date,
                    IsWorkingDay = !specialDay.IsClosed
                };

                if (!specialDay.IsClosed)
                {
                    specialDaySchedule.WorkingIntervals = specialDay.WorkingHours
                        .Select(
                            hours => new WorkingIntervalEntity
                            {
                                StartTime = hours.StartTime,
                                EndTime = hours.EndTime,
                                Type = IntervalType.Work
                            }).ToList();
                }

                await _unitOfWork.OperatingScheduleRepository.InsertAsync(specialDaySchedule);
            }

            await _unitOfWork.CommitTransactionAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result.Failure(ex.ToString());
        }
    }
}