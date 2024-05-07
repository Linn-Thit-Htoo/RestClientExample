using Microsoft.EntityFrameworkCore;
using RestClientExample.RestApi.Features.Blog;

namespace RestClientExample.RestApi;

public static class ModularService
{
    public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddBusinessLogicServices();
        services.AddDataAccessServices();
        services.AddCustomServices(builder);
        return services;
    }

    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services.AddScoped<BusinessLogic_Blog>();
        return services;
    }

    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services.AddScoped<DataAccess_Blog>();
        return services;
    }

    public static IServiceCollection AddCustomServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
        }, ServiceLifetime.Transient);
        return services;
    }
}
