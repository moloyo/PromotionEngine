using PromotionEngine.Application.Features.Promotions.Shared;
using PromotionEngine.Application.Shared;
using System.ComponentModel.DataAnnotations;

namespace PromotionEngine.Application.Features.Promotions.Get.V1;

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

    /// <summary>
    /// Retrieves a specific promotion by country code and promotion ID.
    /// </summary>
    /// <param name="countryCode">The country code for which to retrieve the promotion.</param>
    /// <param name="languageCode">The language code to filter the display content of the promotion.</param>
    /// <param name="promotionId">The unique identifier of the promotion.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the promotion data if found, a 404 status if not found, 
    /// or a 500 status if an exception occurs during processing.
    /// </returns>
    [HttpGet("{countryCode}/promotions/{promotionId:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PromotionModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(OperationId = "Promotion", Description = "Get Promotion by ID")]
    public async Task<IActionResult> Get(
        [Required, RegularExpression("^[a-zA-Z]{2}$", ErrorMessage = "Country code must be a two-letter code.")][SwaggerParameter("ISO-3166 ALPHA-2")] string countryCode,
        [Required, RegularExpression("^[a-zA-Z]{2}$", ErrorMessage = "Language code must be a two-letter code.")][SwaggerParameter("ISO 639-1")] string languageCode,
        [Required] Guid promotionId,
        CancellationToken cancellationToken)
    {
        var request = new Request(countryCode, languageCode, promotionId);

        var response = await _handler.HandleAsync(request, cancellationToken);

        if (response.ExceptionOccurred)
            return HandleException(response.Exception!);

        if (response.NotFound)
            return NotFound();

        return Ok(response.Promotion);
    }
}
