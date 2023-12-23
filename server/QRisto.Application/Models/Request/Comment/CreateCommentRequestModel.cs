namespace QRisto.Application.Models.Request.Comment;

public class CreateCommentRequestModel
{
    public Guid UserId { get; set; }
    
    public Guid ServiceId { get; set; }
    
    public int Rating { get; set; }
    
    public string Text { get; set; }
}