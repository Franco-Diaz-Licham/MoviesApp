namespace backend.src.API.Models.Request;

public class TheatreCreateRequest
{
    [Required] public string Name { get; set; } = default!;
    [Required, MinLength(3)] public string Address { get; set; } = default!;
    [Required] public decimal Longitude { get; set; }
    [Required] public decimal Latitude { get; set; }
}
