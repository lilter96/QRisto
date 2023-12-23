using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRisto.Application.Models.Request.Comment;
using QRisto.Application.Models.Response.Comment;
using QRisto.Application.Services.Comment;
using QRisto.Application.Utils;
using QRisto.Persistence.Entity.Auth;

namespace QRisto.Presentation.Controllers;

[Route("api/comments")]
[Authorize(Roles = ApplicationRoles.AllRoles)]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentRepository)
    {
        _commentService = commentRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequestModel model)
    {
        var result = await _commentService.CreateAsync(model);

        return result.Match<IActionResult, CommentResponseModel>(
            x => StatusCode(StatusCodes.Status201Created, x),
            BadRequest);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetComment(Guid id)
    {
        var result = await _commentService.GetCommentAsync(id);

        return result.Match<IActionResult, CommentResponseModel>(
            Ok,
            BadRequest);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateComment(Guid id, [FromBody] UpdateCommentRequestModel comment)
    {
        var result = await _commentService.CreateAsync(comment);

        return result.Match<IActionResult, CommentResponseModel>(
            Ok,
            BadRequest);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        var result = await _commentService.DeleteAsync(id);

        return result.Match<IActionResult>(
            Ok,
            BadRequest);
    }
}