using Microsoft.AspNetCore.Mvc;
using QRisto.Application.Models.Request.OperatingSchedule;
using QRisto.Application.Models.Request.Service;
using QRisto.Application.Models.Response.Comment;
using QRisto.Application.Models.Response.Service;
using QRisto.Application.Services.Comment;
using QRisto.Application.Services.Service;
using QRisto.Application.Utils;

namespace QRisto.Presentation.Controllers;

[ApiController]
[Route("api/services")]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;
    private readonly ICommentService _commentService;
    
    public ServiceController(IServiceService serviceService, ICommentService commentService)
    {
        _serviceService = serviceService;
        _commentService = commentService;
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
    
    [HttpGet]
    [Route("{serviceId:guid}/average-rating")]
    public async Task<IActionResult> GetAverageRating(Guid serviceId)
    {
        var result = await _serviceService.GetAverageRatingAsync(serviceId);
        
        return result.Match<IActionResult, double>(
            x => new JsonResult(x),
            BadRequest);
    }
    
    [HttpGet]
    [Route("{serviceId:guid}/comments")]
    public async Task<IActionResult> GetServiceComments(
        [FromRoute] Guid serviceId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _commentService.GetServiceCommentsAsync(serviceId, pageNumber, pageSize);
        
        return result.Match<IActionResult, List<CommentResponseModel>>(
            comments => comments.Any() ? Ok(comments) : NotFound("Comments not found."),
            BadRequest);
    }
}