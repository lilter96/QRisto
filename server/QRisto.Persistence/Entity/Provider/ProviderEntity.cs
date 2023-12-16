namespace QRisto.Persistence.Entity.Provider;

public class ProviderEntity : IEntity
{
    public Guid Id { get; set; }
    
    public Guid? DeletedBy { get; set; }

    public Guid? Owner { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime? DeletedDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public string Name { get; set; }
    
    public string TaxId { get; set; }
    
    public string Image { get; set; }
    
    public virtual ICollection<ServiceEntity> Services { get; set; }
    
    public virtual AddressEntity Address { get; set; }
}