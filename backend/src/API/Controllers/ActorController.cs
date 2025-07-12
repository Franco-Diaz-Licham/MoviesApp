namespace backend.src.API.Controllers;

[Route("api/[controller]")]
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
    public async Task<IActionResult> GetAllAsync()
    {
        var Actors = await _actorService.GetAllAsync();
        if (Actors.Count == 0) return NotFound(new ApiResponse(404));
        var result = _mapper.Map<List<ActorResponse>>(Actors);
        return Ok(new ApiResponse(200, result));
    }

    [HttpPost]
    public async Task<ActionResult<ActorResponse>> CreateAsync([FromBody] ActorRequest request)
    {
        var dto = _mapper.Map<ActorDTO>(request);
        var result = await _actorService.CreateAsync(dto);
        var output = _mapper.Map<ActorResponse>(result);
        return Accepted(new ApiResponse(202, output));
    }

    [HttpPut("id")]
    public async Task<ActionResult<ActorResponse>> UpdateAsync([FromBody] ActorRequest request)
    {
        var dto = _mapper.Map<ActorDTO>(request);
        var result = await _actorService.CreateAsync(dto);
        var output = _mapper.Map<ActorResponse>(result);
        return Accepted(new ApiResponse(202, output));
    }
}
