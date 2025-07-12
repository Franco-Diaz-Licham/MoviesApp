namespace backend.Configuration
{
    public static class AppServices
    {
        public static void AddAppServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IGenreService, GenreService>();
        }
    }
}
