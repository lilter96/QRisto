namespace QRisto.Persistence.Entity;

public class ProviderEntity
{
    public Guid Id { get; init; }
    
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string UNP { get; set; }
}