namespace backend.src.Application.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // Genre
        CreateMap<GenreEntity, GenreDTO>().ReverseMap();
        CreateMap<GenreDTO, GenreResponse>();
        CreateMap<GenreCreateRequest, GenreDTO>();
        CreateMap<GenreUpdateRequest, GenreDTO>();

        // Actor
        CreateMap<ActorEntity, ActorDTO>().ReverseMap();
        CreateMap<ActorDTO, ActorResponse>().ForMember(dest => dest.Dob, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Dob)));
        CreateMap<ActorCreateRequest, ActorDTO>();
        CreateMap<ActorUpdateRequest, ActorDTO>();

        // Photo
        CreateMap<PhotoEntity, PhotoDTO>().ReverseMap();
        CreateMap<PhotoDTO, PhotoResponse>();
        CreateMap<PhotoCreateRequest, PhotoDTO>();
        CreateMap<PhotoUpdateRequest, PhotoDTO>();

        // Movies
        CreateMap<MovieEntity, MovieDTO>().ReverseMap();
        CreateMap<MovieDTO, MovieResponse>();
        CreateMap<MovieDetailsDTO, MovieDetailsResponse>();
        CreateMap<MovieCreateRequest, MovieDTO>();
        CreateMap<MovieUpdateRequest, MovieDTO>();

        // Theatres
        CreateMap<TheatreEntity, TheatreDTO>().ReverseMap();
        CreateMap<TheatreDTO, TheatreResponse>();
        CreateMap<TheatreCreateRequest, TheatreDTO>();
        CreateMap<TheatreUpdateRequest, TheatreDTO>();
    }
}
