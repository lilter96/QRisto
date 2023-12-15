using AutoMapper;
using QRisto.Application.Errors;
using QRisto.Application.Models.Request.Provider;
using QRisto.Application.Models.Response.Provider;
using QRisto.Application.Utils;
using QRisto.Persistence.Entity.Provider;
using QRisto.Persistence.Repositories.Implementations;

namespace QRisto.Application.Services.Provider;

public class ProviderService : IProviderService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public ProviderService(IMapper mapper, UnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ProviderResponse>> CreateAsync(ProviderPostRequest providerPostRequest)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var providerEntity = _mapper.Map<ProviderEntity>(providerPostRequest);
            providerEntity = await _unitOfWork.ProviderRepository.InsertAsync(providerEntity);
            await _unitOfWork.ProviderRepository.SaveAsync();

            var model = _mapper.Map<ProviderResponse>(providerEntity);
            await _unitOfWork.CommitTransactionAsync();
            return Result<ProviderResponse>.Success(model);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result<ProviderResponse>.Failure(
                ProviderErrors.UnableCreateProvider,
                ex.ToString());
        }
    }
}