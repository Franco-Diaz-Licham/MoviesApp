namespace backend.src.Infrastructure.Persistence.Specifications
{
    public class ActorDetailsSpecs : BaseSpecification<ActorEntity>
    {
        public ActorDetailsSpecs(int id) : base (x => x.Id == id)
        {
            AddInclude(x => x.Movies);
            AddInclude(x => x.Photo);
        }

        public ActorDetailsSpecs()
        {
            AddInclude(x => x.Movies);
            AddInclude(x => x.Photo);
        }
    }
}
