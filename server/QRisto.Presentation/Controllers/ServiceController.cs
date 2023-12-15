using Microsoft.AspNetCore.Mvc;
using QRisto.Application.Models.Request.OperatingSchedule;
using QRisto.Application.Models.Request.Service;
using QRisto.Application.Models.Response.Service;
using QRisto.Application.Services.Service;
using QRisto.Application.Utils;

namespace QRisto.Presentation.Controllers;

[ApiController]
[Route("api/services")]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceRequestModel model)
    {
        var result = await _serviceService.CreateAsync(model);

        return result.Match<IActionResult, ServiceResponseModel>(
            service => StatusCode(StatusCodes.Status201Created, service),
            BadRequest);
    }

    [HttpPut]
    [Route("{serviceId}/schedule")]
    public async Task<IActionResult> UpdateSchedule(
        [FromRoute] Guid serviceId,
        [FromBody] AddWeeklyScheduleRequestModel model)
    {
        if (serviceId != model.ServiceId)
        {
            return BadRequest("Service ID in the route does not match the Service ID in the model.");
        }

        var result = await _serviceService.AddNewScheduleAsync(model);
        return result.Match<IActionResult>(
            () => Ok("Schedule added successfully."),
            BadRequest);
    }
}