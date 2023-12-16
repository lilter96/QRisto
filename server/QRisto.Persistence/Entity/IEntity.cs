namespace QRisto.Persistence.Entity;

public interface IEntity
{
    Guid Id { get; set; }
    
    Guid? DeletedBy { get; set; }

    DateTime CreatedDate { get; set; }

    DateTime? DeletedDate { get; set; }
    
    DateTime ModificationDate { get; set; }
}