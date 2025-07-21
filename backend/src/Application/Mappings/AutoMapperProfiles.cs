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
        CreateMap<ActorDTO, ActorResponse>()
            .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Dob)));
        CreateMap<ActorCreateRequest, ActorDTO>();
        CreateMap<ActorUpdateRequest, ActorDTO>();

        // Photo
        CreateMap<PhotoEntity, PhotoDTO>().ReverseMap();
        CreateMap<PhotoDTO, PhotoResponse>();
        CreateMap<PhotoCreateRequest, PhotoDTO>();
        CreateMap<PhotoUpdateRequest, PhotoDTO>();

        // Theatres
        CreateMap<TheatreEntity, TheatreDTO>().ReverseMap();
        CreateMap<TheatreDTO, TheatreResponse>();
        CreateMap<TheatreCreateRequest, TheatreDTO>();
        CreateMap<TheatreUpdateRequest, TheatreDTO>();

        // Movies
        CreateMap<MovieEntity, MovieDTO>().ReverseMap();
        CreateMap<MovieDTO, MovieResponse>();
        CreateMap<MovieUpdateRequest, MovieUpdateDTO>();
        CreateMap<MovieCreateRequest, MovieCreateDTO>();
        CreateMap<MovieCreateDTO, MovieEntity>()
            .ForMember(dest => dest.Genres, opt => opt.Ignore())
            .ForMember(dest => dest.Actors, opt => opt.Ignore())
            .ForMember(dest => dest.Theatres, opt => opt.Ignore());
        CreateMap<MovieUpdateDTO, MovieEntity>()
            .ForMember(dest => dest.Genres, opt => opt.Ignore())
            .ForMember(dest => dest.Actors, opt => opt.Ignore())
            .ForMember(dest => dest.Theatres, opt => opt.Ignore());

        // Account
        CreateMap<UserDTO, UserEntity>().ReverseMap();
        CreateMap<UserRegisterDTO, UserEntity>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        CreateMap<UserDTO, UserResponse>();
        CreateMap<UserLoginRequest, UserLoginDTO>();
        CreateMap<UserRegisterRequest, UserRegisterDTO>();
        CreateMap<UserUpdateRequest, UserUpdateDTO>();
    }
}
