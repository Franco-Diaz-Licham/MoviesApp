namespace backend.src.API.Models.Request;

public class MovieUpdateRequest : MovieCreateRequest
{
    [Required] public int Id { get; set; }
}
