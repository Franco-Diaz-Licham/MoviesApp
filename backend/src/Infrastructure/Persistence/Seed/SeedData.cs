namespace backend.src.Infrastructure.Persistence.Seed;

public static class SeedData
{
    private const string BASE_PATH = "src/Infrastructure/Persistence/Seed";
    private const string ACTORS = $"{BASE_PATH}/ActorsData.json";
    private const string GENRES = $"{BASE_PATH}/GenresData.json";
    private const string THEATRES = $"{BASE_PATH}/TheatresData.json";
    private const string MOVIES = $"{BASE_PATH}/MoviesData.json";

    public static async Task SeedAsync(DataContext db)
    {
        await Actors(db);
        await Genres(db);
        await Theatres(db);
        await Movies(db);
    }

    private static async Task Actors(DataContext db)
    {
        if (await db.Actors.AnyAsync()) return;
        var data = await File.ReadAllTextAsync(ACTORS);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var models = JsonSerializer.Deserialize<List<ActorEntity>>(data, opt);
        if (models is null) return;
        await db.Actors.AddRangeAsync(models);
        await db.SaveChangesAsync();
    }

    private static async Task Genres(DataContext db)
    {
        if (await db.Genres.AnyAsync()) return;
        var data = await File.ReadAllTextAsync(GENRES);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var models = JsonSerializer.Deserialize<List<GenreEntity>>(data, opt);
        if (models is null) return;
        await db.Genres.AddRangeAsync(models);
        await db.SaveChangesAsync();
    }

    private static async Task Theatres(DataContext db)
    {
        if (await db.Theatres.AnyAsync()) return;
        var data = await File.ReadAllTextAsync(THEATRES);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var models = JsonSerializer.Deserialize<List<TheatreEntity>>(data, opt);
        if (models is null) return;
        await db.Theatres.AddRangeAsync(models);
        await db.SaveChangesAsync();
    }

    private static async Task Movies(DataContext db)
    {
        if (await db.Movies.AnyAsync()) return;
        var data = await File.ReadAllTextAsync(MOVIES);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var models = JsonSerializer.Deserialize<List<MovieEntity>>(data, opt);
        if (models is null) return;
        await db.Movies.AddRangeAsync(models);
        await db.SaveChangesAsync();
    }
}
