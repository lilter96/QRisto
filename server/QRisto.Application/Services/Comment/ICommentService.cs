using QRisto.Application.Models.Request.Comment;
using QRisto.Application.Models.Response.Comment;
using QRisto.Application.Utils;

namespace QRisto.Application.Services.Comment;

public interface ICommentService
{
    public Task<Result<CommentResponseModel>> CreateAsync(CreateCommentRequestModel model);

    public Task<Result<CommentResponseModel>> UpdateAsync(UpdateCommentRequestModel model);

    public Task<Result> DeleteAsync(Guid serviceId);

    public Task<Result<List<CommentResponseModel>>> GetServiceCommentsAsync(Guid serviceId, int page, int pageSize);

    public Task<Result<List<CommentResponseModel>>> GetUserCommentsAsync(Guid userId, int page, int pageSize);

    public Task<Result<CommentResponseModel>> GetCommentAsync(Guid commentId);
}