using Microsoft.AspNetCore.Mvc;
using QRisto.Application.Models.Request.Provider;
using QRisto.Application.Models.Response.Provider;
using QRisto.Application.Services.Provider;
using QRisto.Application.Utils;

namespace QRisto.Presentation.Controllers;

[Route("api/providers")]
public class ProviderController : ControllerBase
{
    private readonly IProviderService _providerService;

    public ProviderController(IProviderService providerService)
    {
        _providerService = providerService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProviderGetResponse), 201)]
    public async Task<IActionResult> Create([FromBody] ProviderPostRequest providerPostRequest)
    {
        var result = await _providerService.CreateAsync(providerPostRequest);

        return result.Match<IActionResult, ProviderGetResponse>(
            x => StatusCode(
                StatusCodes.Status201Created,
                x),
            BadRequest);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(List<ProviderGetResponse>), 200)]
    public async Task<IActionResult> GetList([FromQuery] ProviderGetListRequest providerPostRequest)
    {
        var result = await _providerService.GetListAsync(providerPostRequest);

        return result.Match<IActionResult, List<ProviderGetResponse>>(
            x => StatusCode(
                StatusCodes.Status200OK,
                x),
            BadRequest);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(List<ProviderDetailsGetResponse>), 200)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await _providerService.GetByIdWithAddressAsync(id);

        return result.Match<IActionResult, ProviderDetailsGetResponse>(
            x => StatusCode(
                StatusCodes.Status200OK,
                x),
            BadRequest);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _providerService.DeleteAsync(id);

        return result.Match<IActionResult>(Ok, BadRequest);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProviderGetResponse), 200)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProviderPostRequest providerPostRequest)
    {
        var result = await _providerService.UpdateAsync(id, providerPostRequest);

        return result.Match<IActionResult, ProviderGetResponse>(
            x => StatusCode(
                StatusCodes.Status200OK,
                x),
            BadRequest);
    }
}