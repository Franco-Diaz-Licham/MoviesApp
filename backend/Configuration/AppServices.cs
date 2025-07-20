namespace backend.Configuration;

public static class AppServices
{
    public static void AddAppServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IGenreService, GenreService>();
        builder.Services.AddScoped<IActorService, ActorService>();
        builder.Services.AddScoped<ITheatreService, TheatreService>();
        builder.Services.AddScoped<IMovieService, MovieService>();
        builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
        builder.Services.AddScoped<IPhotoService, PhotoService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
