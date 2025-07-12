namespace backend.src.API.Controllers;

[Route("api/[controller]")]
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
    public async Task<IActionResult> GetAllAsync()
    {
        var Theatres = await _theatreService.GetAllAsync();
        if (Theatres.Count == 0) return NotFound(new ApiResponse(404));
        var result = _mapper.Map<List<TheatreResponse>>(Theatres);
        return Ok(new ApiResponse(200, result));
    }

    [HttpPost]
    public async Task<ActionResult<TheatreResponse>> CreateAsync([FromBody] TheatreRequest request)
    {
        var dto = _mapper.Map<TheatreDTO>(request);
        var result = await _theatreService.CreateAsync(dto);
        var output = _mapper.Map<TheatreResponse>(result);
        return Accepted(new ApiResponse(202, output));
    }

    [HttpPut("id")]
    public async Task<ActionResult<TheatreResponse>> UpdateAsync([FromBody] TheatreRequest request)
    {
        var dto = _mapper.Map<TheatreDTO>(request);
        var result = await _theatreService.CreateAsync(dto);
        var output = _mapper.Map<TheatreResponse>(result);
        return Accepted(new ApiResponse(202, output));
    }
}
