﻿namespace backend.src.Infrastructure.Persistence.Seed;

public static class SeedData
{
    private const string BASE_PATH = "src/Infrastructure/Persistence/Seed";
    private const string ACTORS = $"{BASE_PATH}/ActorsData.json";
    private const string GENRES = $"{BASE_PATH}/GenresData.json";
    private const string THEATRES = $"{BASE_PATH}/TheatresData.json";
    private const string MOVIES = $"{BASE_PATH}/MoviesData.json";
    private const string PHOTOS = $"{BASE_PATH}/PhotosData.json";
    private const string IMAGES = $"{BASE_PATH}/Images/";

    public static async Task SeedAsync(DataContext db, ICloudinaryService cloudinary)
    {
        await Photos(db, cloudinary);
        await Actors(db);
        await Genres(db);
        await Theatres(db);
        await Movies(db);
    }

    /// <summary>
    /// Migrates initial actors.
    /// </summary>
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

    /// <summary>
    /// Migrates initial genres.
    /// </summary>
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

    /// <summary>
    /// Migrates intial theatres.
    /// </summary>
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

    /// <summary>
    /// Migrates initial movies.
    /// </summary>
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

    /// <summary>
    /// Migrates initial photos.
    /// </summary>
    private static async Task Photos(DataContext db, ICloudinaryService cloudinary)
    {
        if (await db.Photos.AnyAsync()) return;

        // Load actors
        var data = await File.ReadAllTextAsync(PHOTOS);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var models = JsonSerializer.Deserialize<List<SeedPhotoModel>>(data, opt);
        if (models is null) return;

        // Load images
        var imagesDirectory = Path.Combine(IMAGES);
        var imageFiles = Directory.GetFiles(imagesDirectory, "*.jpg");

        // Upload to cloudinary and update photo model.
        for (int i = 0; i < imageFiles.Length; i++)
        {
            var fileName = imageFiles[i].Substring(0, IMAGES.Length - 1);
            byte[] bytes = await File.ReadAllBytesAsync(imageFiles[i]);
            var result = await cloudinary.UploadPhotoAsync(bytes, fileName);
            models[i].PublicUrl = result.SecureUrl.ToString();
            models[i].PublicId = result.PublicId.ToString();
        }

        await db.Photos.AddRangeAsync(models);
        await db.SaveChangesAsync();
    }

    private class SeedPhotoModel : PhotoEntity
    {
        public string FileName { get; set; } = default!;
    }
}

