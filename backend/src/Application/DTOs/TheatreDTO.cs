namespace backend.src.Application.DTOs;

public class TheatreDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int? Longitude { get; set; }
    public int? Latitude { get; set; }
}
