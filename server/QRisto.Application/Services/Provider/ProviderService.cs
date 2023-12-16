using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public ProviderService(IMapper mapper, UnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<ProviderGetResponse>> CreateAsync(ProviderPostRequest providerPostRequest)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var providerEntity = _mapper.Map<ProviderEntity>(providerPostRequest);
            providerEntity = await _unitOfWork.ProviderRepository.InsertAsync(providerEntity);

            var model = _mapper.Map<ProviderGetResponse>(providerEntity);
            await _unitOfWork.CommitTransactionAsync();
            return Result<ProviderGetResponse>.Success(model);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result<ProviderGetResponse>.Failure(
                ProviderErrors.UnableCreateProvider,
                ex.ToString());
        }
    }

    public async Task<Result<List<ProviderGetResponse>>> GetListAsync(ProviderGetListRequest providerPostRequest)
    {
        try
        {
            var providers = await _unitOfWork.ProviderRepository.GetListAsync();
            
            var model = _mapper.Map<List<ProviderGetResponse>>(providers);
            return Result<List<ProviderGetResponse>>.Success(model);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result<List<ProviderGetResponse>>.Failure(
                ProviderErrors.UnableGetProviders,
                ex.ToString());
        }
    }
    
    public async Task<Result<ProviderDetailsGetResponse>> GetByIdWithAddressAsync(Guid id)
    {
        try
        {
            var providerEntity = await _unitOfWork.ProviderRepository.GetByIdWithAddressAsync(id);
            
            var model = _mapper.Map<ProviderDetailsGetResponse>(providerEntity);
            return Result<ProviderDetailsGetResponse>.Success(model);
        }
        catch (Exception ex)
        {
            return Result<ProviderDetailsGetResponse>.Failure(
                ProviderErrors.UnableGetProvider,
                ex.ToString());
        }
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var userId = new Guid(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            
            await _unitOfWork.ProviderRepository.DeleteAsync(id, userId);
            await _unitOfWork.CommitTransactionAsync();
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result.Failure(
                ProviderErrors.UnableDeleteProvider,
                ex.ToString());
        }
    }

    public async Task<Result<ProviderGetResponse>> UpdateAsync(Guid id, ProviderPostRequest providerPostRequest)
    {
        try
        {
            var providerEntity = _mapper.Map<ProviderEntity>(providerPostRequest);
            providerEntity.Id = id;
            
            _unitOfWork.ProviderRepository.Update(providerEntity);
            
            await _unitOfWork.SaveChangesAsync();
            var model = _mapper.Map<ProviderGetResponse>(providerEntity);
            return Result<ProviderGetResponse>.Success(model);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result<ProviderGetResponse>.Failure(
                ProviderErrors.UnableUpdateProvider,
                ex.ToString());
        }
    }
}