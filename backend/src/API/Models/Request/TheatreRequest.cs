namespace backend.src.API.Models.Request;

public class TheatreRequest
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Address { get; set; } = string.Empty;
    public int? Longitude { get; set; }
    public int? Latitude { get; set; }
}
