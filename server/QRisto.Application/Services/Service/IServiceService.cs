using QRisto.Application.Models.Request.OperatingSchedule;
using QRisto.Application.Models.Request.Service;
using QRisto.Application.Models.Response.Service;
using QRisto.Application.Utils;

namespace QRisto.Application.Services.Service;

public interface IServiceService
{
    Task<Result<ServiceResponseModel>> CreateAsync(CreateServiceRequestModel model);

    Task<Result> AddNewScheduleAsync(AddWeeklyScheduleRequestModel model);
}