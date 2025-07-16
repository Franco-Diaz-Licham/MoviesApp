namespace backend.src.Domain.Entities;

public class ActorEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public DateTime Dob { get; set; }
    public int PhotoId { get; set; }
    public PhotoEntity? Photo { get; set; }
    public string Biography { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public ICollection<MovieEntity> Movies { get; set; } = new List<MovieEntity>();
}
