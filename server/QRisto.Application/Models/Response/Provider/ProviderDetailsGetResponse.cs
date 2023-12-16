using QRisto.Application.Models.Response.Address;

namespace QRisto.Application.Models.Response.Provider;

public class ProviderDetailsGetResponse : ProviderGetResponse
{
    public AddressGetResponse Address { get; set; }
}