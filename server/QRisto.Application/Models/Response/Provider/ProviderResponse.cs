namespace QRisto.Application.Models.Response.Provider;

public class ProviderResponse
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string UNP { get; set; }
}