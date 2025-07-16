namespace backend.src.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly IMapper _mapper;

    public MovieController(IMovieService movieService, IMapper mapper)
    {
        _movieService = movieService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var Movies = await _movieService.GetAllAsync();
        if (Movies.Count == 0) return NotFound(new ApiResponse(404));
        var result = _mapper.Map<List<MovieResponse>>(Movies);
        return Ok(new ApiResponse(200, result));
    }

    [HttpGet("/details")]
    public async Task<IActionResult> GetAllDetailsAsync()
    {
        var Movies = await _movieService.GetAllDetailsAsync();
        if (Movies.Count == 0) return NotFound(new ApiResponse(404));
        var result = _mapper.Map<List<MovieResponse>>(Movies);
        return Ok(new ApiResponse(200, result));
    }

    [HttpPost]
    public async Task<ActionResult<MovieResponse>> CreateAsync([FromForm] MovieRequest request)
    {
        var dto = _mapper.Map<MovieDTO>(request);
        var result = await _movieService.CreateAsync(dto);
        var output = _mapper.Map<MovieResponse>(result);
        return Accepted(new ApiResponse(202, output));
    }

    [HttpPut("id")]
    public async Task<ActionResult<MovieResponse>> UpdateAsync([FromForm] MovieRequest request)
    {
        var dto = _mapper.Map<MovieDTO>(request);
        var result = await _movieService.CreateAsync(dto);
        var output = _mapper.Map<MovieResponse>(result);
        return Accepted(new ApiResponse(202, output));
    }
}
