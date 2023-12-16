using QRisto.Application.Models.Request.Provider;
using QRisto.Application.Models.Response.Provider;
using QRisto.Application.Utils;

namespace QRisto.Application.Services.Provider;

public interface IProviderService
{
    public Task<Result<ProviderGetResponse>> CreateAsync(ProviderPostRequest providerPostRequest);
    
    public Task<Result<List<ProviderGetResponse>>> GetListAsync(ProviderGetListRequest providerPostRequest);
    
    public Task<Result<ProviderDetailsGetResponse>> GetByIdWithAddressAsync(Guid id);
    
    public Task<Result> DeleteAsync(Guid id);

    public Task<Result<ProviderGetResponse>> UpdateAsync(Guid id, ProviderPostRequest providerPostRequest);
}