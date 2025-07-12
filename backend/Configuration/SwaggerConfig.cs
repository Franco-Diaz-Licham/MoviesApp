namespace backend.Configuration;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerService(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Movies App API v1" });
        });
        return services;
    }
}
