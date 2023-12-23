using Microsoft.AspNetCore.Mvc;
using QRisto.Application.Models.Response.Comment;
using QRisto.Application.Services.Comment;
using QRisto.Application.Utils;

namespace QRisto.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly ICommentService _commentService;

    public UserController(ICommentService commentService)
    {
        _commentService = commentService;
    }
    
    [HttpGet]
    [Route("{userId:guid}/comments")]
    public async Task<IActionResult> GetUserComments(
        [FromRoute] Guid userId, 
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10)
    {
        var result = await _commentService.GetUserCommentsAsync(userId, pageNumber, pageSize);
        
        return result.Match<IActionResult, List<CommentResponseModel>>(
            comments => comments.Any() ? Ok(comments) : NotFound("Comments not found."),
            BadRequest);
    }
}