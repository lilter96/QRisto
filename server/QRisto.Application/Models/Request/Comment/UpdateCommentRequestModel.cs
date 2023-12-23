namespace QRisto.Application.Models.Request.Comment;

public class UpdateCommentRequestModel : CreateCommentRequestModel
{
    public Guid CommentId { get; set; }
}