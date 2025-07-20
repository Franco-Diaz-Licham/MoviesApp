namespace backend.src.Domain.Entities;

public class TheatreEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public List<MovieEntity> Movies { get; set; } = new List<MovieEntity>();
}
