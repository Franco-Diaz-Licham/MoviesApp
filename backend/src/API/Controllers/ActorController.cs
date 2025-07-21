namespace backend.src.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class ActorController : ControllerBase
{
    private readonly IActorService _actorService;
    private readonly IMapper _mapper;

    public ActorController(IActorService actorService, IMapper mapper)
    {
        _actorService = actorService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ActorResponse>>> GetAllAsync()
    {
        var Actors = await _actorService.GetAllAsync();
        if (Actors.Count == 0) return NotFound(new ApiResponse(404));
        var result = _mapper.Map<List<ActorResponse>>(Actors);
        return Ok(new ApiResponse(200, result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActorResponse>> GetAsync(int id)
    {
        var genre = await _actorService.GetAsync(id);
        if (genre is null) return NotFound(new ApiResponse(404));
        var result = _mapper.Map<ActorResponse>(genre);
        return Ok(new ApiResponse(200, result));
    }

    [HttpPost]
    public async Task<ActionResult<ActorResponse>> CreateAsync([FromForm] ActorCreateRequest request)
    {
        var dto = _mapper.Map<ActorDTO>(request);
        var result = await _actorService.CreateAsync(dto);
        var output = _mapper.Map<ActorResponse>(result);
        return Accepted(new ApiResponse(201, output));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ActorResponse>> UpdateAsync(int id, [FromForm] ActorUpdateRequest request)
    {
        // check model exists
        var existingActor = await _actorService.GetAsyncCheck(id);
        if (existingActor is false) return NotFound(new ApiResponse(404));

        // make changes
        var dto = _mapper.Map<ActorDTO>(request);
        var result = await _actorService.UpdateAsync(dto);
        var output = _mapper.Map<ActorResponse>(result);
        return Accepted(new ApiResponse(202, output));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var result = await _actorService.DeleteAsync(id);
        if (result is false) return BadRequest(new ApiResponse(400));
        return NoContent();
    }
}
