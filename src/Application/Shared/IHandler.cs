namespace PromotionEngine.Application.Shared;

public interface IHandler<TRequest, TResponse> 
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}
