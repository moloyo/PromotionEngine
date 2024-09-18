using System.Diagnostics;
using System.Net;

namespace PromotionEngine.Application.Shared;

public abstract class FeatureControllerBase : ControllerBase
{
    protected readonly ILogger _logger;

    public FeatureControllerBase(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected IActionResult HandleException(Exception ex)
    {
        return Problem(ex, (int)HttpStatusCode.InternalServerError, LogLevel.Error);
    }

    protected IActionResult Problem(Exception ex, int statusCode, LogLevel logLevel = LogLevel.Information)
    {
        if (logLevel != LogLevel.None)
        {
            _logger.Log(logLevel, ex, ex.Message);
        }

        var problem = new ProblemDetails()
        {
            Type = $"https://httpstatuses.com/{statusCode}",
            Title = ex.Message,
#if DEBUG
            Detail = ex.ToString(),
#endif
            Status = statusCode,
            Instance = Request.Path,
            Extensions = { { "traceId", Activity.Current?.TraceId.ToString() } }
        };

        return StatusCode(problem.Status.Value, problem);
    }

    protected ObjectResult Problem(IDictionary<string, string[]> errors,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        var problem = new ValidationProblemDetails(errors)
        {
            Type = $"https://httpstatuses.com/{(int)statusCode}",
            Title = "One or more validation errors occurred.",
            Status = (int)statusCode,
            Instance = Request?.Path,
            Extensions = { { "traceId", Activity.Current?.TraceId.ToString() } }
        };

        return StatusCode(problem.Status.Value, problem);
    }
}
