namespace backend.src.Infrastructure.Persistence.Specifications;

public class MovieDetailsSpecs : BaseSpecification<MovieEntity>
{
    public MovieDetailsSpecs()
    {
        AddInclude(x => x.Actors);
        AddInclude(x => x.Genres);
        AddInclude(x => x.Theatres);
    }
}
