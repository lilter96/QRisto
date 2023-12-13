using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRisto.Application.Models.Request.Provider;
using QRisto.Application.Models.Response.Provider;
using QRisto.Application.Services.Provider;
using QRisto.Application.Utils;

namespace QRisto.Presentation.Controllers;

public class ProviderController : ControllerBase
{
    private readonly IProviderService _providerService;

    public ProviderController(IProviderService providerService)
    {
        _providerService = providerService;
    }

    [HttpPost]
    [Authorize]
    [Route("create", Name = "CreateProvider")]
    [ProducesResponseType(typeof(ProviderResponse), 201)]
    public async Task<IActionResult> Create(ProviderPostRequest providerPostRequest)
    {
        var result = await _providerService.CreateAsync(providerPostRequest);

        return result.Match<IActionResult, ProviderResponse>(
            onSuccess: x => StatusCode(StatusCodes.Status201Created, x),
            onFailure: BadRequest);
    }
}