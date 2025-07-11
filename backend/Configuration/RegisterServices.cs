namespace backend.Configuration;

public static class RegisterServices
{
    /// <summary>
    /// Method which registers all services used in the application to the DI container.
    /// </summary>
    /// <param name="builder"></param>
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.AddSerilog();
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<DataContext>(opt => opt.UseMySQL(builder.Configuration.GetConnectionString("Movie") ?? ""));
        builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        builder.Services.AddMemoryCache();
        builder.AddAppServices();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            builder.AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowAnyOrigin());
        });

        // model validation API response.
        builder.Services.Configure<ApiBehaviorOptions>(opt =>
        {
            opt.InvalidModelStateResponseFactory = actionContext =>
            {
                // make all validations errors into array
                var errors = actionContext.ModelState
                            .Where(e => e.Value?.Errors.Count > 0)
                            .SelectMany(x => x.Value?.Errors!)
                            .Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse(errors);
                return new BadRequestObjectResult(errorResponse);
            };
        });
    }
}
