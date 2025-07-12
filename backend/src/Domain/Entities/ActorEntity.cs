namespace backend.src.Domain.Entities;

public class ActorEntity : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly Dob { get; set; }
    public string? ImageUrl { get; set; }
    public string Biography { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public ICollection<MovieEntity> Movies { get; set; } = new List<MovieEntity>();
}
