namespace backend.src.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class TheatreController : ControllerBase
{
    private readonly ITheatreService _theatreService;
    private readonly IMapper _mapper;

    public TheatreController(ITheatreService theatreService, IMapper mapper)
    {
        _theatreService = theatreService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<TheatreResponse>>> GetAllAsync()
    {
        var Theatres = await _theatreService.GetAllAsync();
        if (Theatres.Count == 0) return NotFound(new ApiResponse(404));
        var result = _mapper.Map<List<TheatreResponse>>(Theatres);
        return Ok(new ApiResponse(200, result));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<TheatreResponse>> GetAsync(int id)
    {
        var genre = await _theatreService.GetAsync(id);
        if (genre is null) return NotFound(new ApiResponse(404));
        var result = _mapper.Map<TheatreResponse>(genre);
        return Ok(new ApiResponse(200, result));
    }

    [HttpPost]
    public async Task<ActionResult<TheatreResponse>> CreateAsync([FromBody] TheatreCreateRequest request)
    {
        var dto = _mapper.Map<TheatreDTO>(request);
        var result = await _theatreService.CreateAsync(dto);
        var output = _mapper.Map<TheatreResponse>(result);
        return Accepted(new ApiResponse(201, output));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TheatreResponse>> UpdateAsync(int id, [FromBody] TheatreUpdateRequest request)
    {
        var dto = _mapper.Map<TheatreDTO>(request);
        var result = await _theatreService.UpdateAsync(dto);
        var output = _mapper.Map<TheatreResponse>(result);
        return Accepted(new ApiResponse(202, output));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var result = await _theatreService.DeleteAsync(id);
        if (result is false) return BadRequest(new ApiResponse(400));
        return NoContent();
    }
}
