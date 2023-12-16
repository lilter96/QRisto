namespace QRisto.Persistence.Entity.Provider;

public class ServiceEntity : IEntity
{
    public string Code { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public Guid ProviderId { get; set; }

    public virtual ProviderEntity Provider { get; set; }

    public Guid? AddressId { get; set; }

    public virtual AddressEntity Address { get; set; }

    public virtual ICollection<TableEntity> Tables { get; set; }

    public Guid Id { get; set; }
    
    public Guid? DeletedBy { get; set; }

    public DateTime CreatedDate { get; set; }
    
    public DateTime? DeletedDate { get; set; }

    public DateTime ModificationDate { get; set; }
}