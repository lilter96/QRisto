namespace QRisto.Application.Models.Response.Provider;

public class ProviderGetResponse
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }
    
    public string Image { get; init; }
    
    public string TaxId { get; init; }
}