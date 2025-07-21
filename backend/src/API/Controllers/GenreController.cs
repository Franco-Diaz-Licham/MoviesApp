namespace backend.src.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;
    private readonly IMapper _mapper;

    public GenreController(IGenreService genreService, IMapper mapper)
    {
        _genreService = genreService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<GenreResponse>>> GetAllAsync()
    {
        var genres = await _genreService.GetAllAsync();
        if (genres.Count == 0) return NotFound(new ApiResponse(404));
        var result = _mapper.Map<List<GenreResponse>>(genres);
        return Ok(new ApiResponse(200, result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreResponse>> GetAsync(int id)
    {
        var genre = await _genreService.GetAsync(id);
        if (genre is null) return NotFound(new ApiResponse(404));
        var result = _mapper.Map<GenreResponse>(genre);
        return Ok(new ApiResponse(200, result));
    }

    [HttpPost]
    public async Task<ActionResult<GenreResponse>> CreateAsync([FromBody] GenreCreateRequest request)
    {
        var dto = _mapper.Map<GenreDTO>(request);
        var result = await _genreService.CreateAsync(dto);
        var output = _mapper.Map<GenreResponse>(result);
        return Created("", new ApiResponse(201, output));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GenreResponse>> UpdateAsync(int id, [FromBody] GenreUpdateRequest request)
    {
        var dto = _mapper.Map<GenreDTO>(request);
        var result = await _genreService.UpdateAsync(dto);
        var output = _mapper.Map<GenreResponse>(result);
        return Accepted(new ApiResponse(202, output));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var result = await _genreService.DeleteAsync(id);
        if (result is false) return BadRequest(new ApiResponse(400));
        return NoContent();
    }
}
