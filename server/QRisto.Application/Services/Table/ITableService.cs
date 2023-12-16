using QRisto.Application.Models.Request.Table;
using QRisto.Application.Models.Response.Table;
using QRisto.Application.Utils;

namespace QRisto.Application.Services.Table;

public interface ITableService
{
    Task<Result<TableResponseModel>> CreateTableAsync(CreateTableRequestModel requestModel);
}