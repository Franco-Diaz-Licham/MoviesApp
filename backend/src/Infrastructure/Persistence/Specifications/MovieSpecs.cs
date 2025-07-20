namespace backend.src.Infrastructure.Persistence.Specifications;

public class MovieSpecs : BaseSpecification<MovieEntity>
{
    public MovieSpecs(int id) : base(x => x.Id == id) => AddInclude(x => x.Photo);

    public MovieSpecs() => AddInclude(x => x.Photo);
}
