namespace backend.src.Infrastructure.Persistence;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<ActorEntity> Actors { get; set; }
    public DbSet<GenreEntity> Genres { get; set; }
    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<TheatreEntity> Theatres { get; set; }
    public DbSet<MovieActorEntity> MovieActors { get; set; }
    public DbSet<MovieGenreEntity> MovieGenres { get; set; }
    public DbSet<MovieTheatreEntity> MovieTheatres { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // configure relations
        builder.Entity<MovieEntity>().HasMany(x => x.Genres).WithMany(x => x.Movies).UsingEntity<MovieGenreEntity>(
            x => x.HasOne(x => x.Genre).WithMany().HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.NoAction).IsRequired(),
            x => x.HasOne(x => x.Movie).WithMany().HasForeignKey(x => x.MovieId).OnDelete(DeleteBehavior.Cascade).IsRequired(),
            x => x.HasKey(x => new { x.MovieId, x.GenreId })
        );
        builder.Entity<MovieEntity>().HasMany(x => x.Actors).WithMany(x => x.Movies).UsingEntity<MovieActorEntity>(
            x => x.HasOne(x => x.Actor).WithMany().HasForeignKey(x => x.ActorId).OnDelete(DeleteBehavior.NoAction).IsRequired(),
            x => x.HasOne(x => x.Movie).WithMany().HasForeignKey(x => x.MovieId).OnDelete(DeleteBehavior.Cascade).IsRequired(),
            x => x.HasKey(x => new { x.MovieId, x.ActorId })
        );
        builder.Entity<MovieEntity>().HasMany(x => x.Theatres).WithMany(x => x.Movies).UsingEntity<MovieTheatreEntity>(
            x => x.HasOne(x => x.Theatre).WithMany().HasForeignKey(x => x.TheatreId).OnDelete(DeleteBehavior.NoAction).IsRequired(),
            x => x.HasOne(x => x.Movie).WithMany().HasForeignKey(x => x.MovieId).OnDelete(DeleteBehavior.Cascade).IsRequired(),
            x => x.HasKey(x => new { x.MovieId, x.TheatreId })
        );

        base.OnModelCreating(builder);
    }
}
