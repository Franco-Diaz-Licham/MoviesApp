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
    public async Task<ActionResult<List<MovieResponse>>> GetAllDetailsAsync()
    {
        var Movies = await _movieService.GetAllDetailsAsync();
        if (Movies.Count == 0) return NotFound(new ApiResponse(404));
        var result = _mapper.Map<List<MovieResponse>>(Movies);
        return Ok(new ApiResponse(200, result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieResponse>> GetAsync(int id)
    {
        var genre = await _movieService.GetAsync(id);
        if (genre is null) return NotFound(new ApiResponse(404));
        var result = _mapper.Map<MovieResponse>(genre);
        return Ok(new ApiResponse(200, result));
    }

    [HttpPost]
    public async Task<ActionResult<MovieResponse>> CreateAsync([FromForm] MovieCreateRequest request)
    {
        var dto = _mapper.Map<MovieCreateDTO>(request);
        var result = await _movieService.CreateAsync(dto);
        var output = _mapper.Map<MovieResponse>(result);
        return Accepted(new ApiResponse(201, output));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MovieResponse>> UpdateAsync(int id, [FromForm] MovieUpdateRequest request)
    {
        var dto = _mapper.Map<MovieUpdateDTO>(request);
        var result = await _movieService.UpdateAsync(dto);
        var output = _mapper.Map<MovieResponse>(result);
        return Accepted(new ApiResponse(202, output));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var result = await _movieService.DeleteAsync(id);
        if (result is false) return BadRequest(new ApiResponse(400));
        return NoContent();
    }
}
