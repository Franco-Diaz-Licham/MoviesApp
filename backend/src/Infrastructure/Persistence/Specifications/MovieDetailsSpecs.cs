namespace backend.src.Infrastructure.Persistence.Specifications;

public class MovieDetailsSpecs : BaseSpecification<MovieEntity>
{
    public MovieDetailsSpecs(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Photo);
        AddInclude(x => x.Actors);
        AddInclude(x => x.Genres);
        AddInclude(x => x.Theatres);
    }

    public MovieDetailsSpecs()
    {
        AddInclude(x => x.Photo);
        AddInclude(x => x.Actors);
        AddInclude(x => x.Genres);
        AddInclude(x => x.Theatres);
    }
}
