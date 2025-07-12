namespace backend.src.Application.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<GenreEntity, GenreDTO>();
        CreateMap<GenreDTO, GenreResponse>();
        CreateMap<GenreRequest, GenreDTO>();
    }
}
