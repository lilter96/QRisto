using QRisto.Application.Models.Request.Provider;
using QRisto.Application.Models.Response.Provider;
using QRisto.Application.Utils;

namespace QRisto.Application.Services.Provider;

public interface IProviderService
{
    public Task<Result<ProviderResponse>> CreateAsync(ProviderPostRequest providerPostRequest);
}