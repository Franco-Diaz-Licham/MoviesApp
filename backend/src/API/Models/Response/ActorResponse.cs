namespace backend.src.API.Models.Response;

public class ActorResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly Dob { get; set; }
    public PhotoResponse? Photo { get; set; }
    public string Biography { get; set; } = string.Empty;
}
