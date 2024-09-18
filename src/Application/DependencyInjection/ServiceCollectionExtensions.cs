using Microsoft.Extensions.DependencyInjection;
using PromotionEngine.Application.Features.Promotions.Shared.Repositories;
using PromotionEngine.Application.Shared;

namespace PromotionEngine.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Get Promotion list
        services.AddTransient<IHandler<Features.Promotions.GetAll.V1.Request, Features.Promotions.GetAll.V1.Response>, Features.Promotions.GetAll.V1.Handler>();
        services.AddTransient<IHandler<Features.Promotions.GetAll.V2.Request, Features.Promotions.GetAll.V2.Response>, Features.Promotions.GetAll.V2.Handler>();

        // Get Promotion
        services.AddTransient<IHandler<Features.Promotions.Get.V1.Request, Features.Promotions.Get.V1.Response>, Features.Promotions.Get.V1.Handler>();

        // Shared
        services.AddTransient<IRepository, Repository>();

        return services;
    }
}
