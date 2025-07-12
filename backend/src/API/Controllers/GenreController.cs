namespace backend.src.API.Controllers;

[Route("api/[controller]")]
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
    public async Task<IActionResult> GetAllAsync()
    {
        var genres = await _genreService.GetAllAsync();
        if (genres.Count == 0) return NotFound(new ApiResponse(404));
        var result = _mapper.Map<List<GenreResponse>>(genres);
        return Ok(new ApiResponse(200, result));
    }

    [HttpPost]
    public async Task<ActionResult<GenreResponse>> CreateAsync([FromBody] GenreRequest request)
    {
        var dto = _mapper.Map<GenreDTO>(request);           
        var result = await _genreService.CreateAsync(dto);
        var output = _mapper.Map<GenreResponse>(result);
        return Accepted(new ApiResponse(202, output));
    }

    [HttpPut("id")]
    public async Task<ActionResult<GenreResponse>> UpdateAsync([FromBody] GenreRequest request)
    {
        var dto = _mapper.Map<GenreDTO>(request);
        var result = await _genreService.CreateAsync(dto);
        var output = _mapper.Map<GenreResponse>(result);
        return Accepted(new ApiResponse(202, output));
    }
}
