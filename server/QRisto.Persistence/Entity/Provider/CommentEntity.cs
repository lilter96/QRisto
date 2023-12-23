using QRisto.Persistence.Entity.Auth;

namespace QRisto.Persistence.Entity.Provider;

public class CommentEntity : IEntity
{
    public int Rating { get; set; }
    
    public string Text { get; set; }
    
    public Guid ServiceId { get; set; }
    
    public virtual ServiceEntity Service { get; set; }
    
    public Guid UserId { get; set; }
    
    public virtual ApplicationUser User { get; set; }
    
    public Guid Id { get; set; }
    
    public Guid? DeletedBy { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime? DeletedDate { get; set; }
 
    public DateTime ModificationDate { get; set; }
}