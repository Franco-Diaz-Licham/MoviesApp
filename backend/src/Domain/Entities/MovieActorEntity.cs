namespace backend.src.Domain.Entities;

public class MovieActorEntity
{
    public int MovieId { get; set; }
    public MovieEntity? Movie { get; set; }
    public int ActorId { get; set; }
    public ActorEntity? Actor { get; set; }
}
