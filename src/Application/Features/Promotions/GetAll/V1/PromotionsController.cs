using PromotionEngine.Application.Shared;
using System.ComponentModel.DataAnnotations;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

[ApiController]
[Route("v1")]
public class PromotionsController : FeatureControllerBase
{
    private readonly IHandler<Request, Response> _handler;

    public PromotionsController(
        IHandler<Request, Response> handler,
        ILogger<PromotionsController> logger) : base(logger)
    {
        _handler = handler;
    }

    [HttpGet("{countryCode}/promotions")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(OperationId = "PromotionsList", Description = "Get Promotions")]
    public async Task<IActionResult> Get(
        [Required, RegularExpression("^[a-zA-Z]{2}$", ErrorMessage = "Country code must be a two-letter code.")][SwaggerParameter("ISO-3166 ALPHA-2")] string countryCode,
        [Required, RegularExpression("^[a-zA-Z]{2}$", ErrorMessage = "Language code must be a two-letter code.")][SwaggerParameter("ISO 639-1")] string languageCode,
        CancellationToken cancellationToken)
    {
        var request = new Request(countryCode, languageCode);

        var response = await _handler.HandleAsync(request, cancellationToken);

        return response.ExceptionOccurred ? HandleException(response.Exception!) : Ok(response);
    }
}
