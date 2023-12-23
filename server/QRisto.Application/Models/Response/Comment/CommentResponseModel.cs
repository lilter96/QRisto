namespace QRisto.Application.Models.Response.Comment;

public class CommentResponseModel
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }

    public Guid ServiceId { get; set; }
    
    public int Rating { get; set; }
    
    public string Text { get; set; }
}