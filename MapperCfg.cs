namespace CourseWork;

public class MapperCfg : Profile
{
    public MapperCfg()
    {
        //User maps
        CreateMap<UserModel, GetUserDto>();
        CreateMap<SetUserDto, UserModel>();

        //Track maps
        CreateMap<TrackModel, SetTrackDto>();
        CreateMap<SetTrackDto, TrackModel>();
        CreateMap<SetTrackDto, GetTrackDto>();

        CreateMap<TrackModel, GetTrackDto>();
        CreateMap<GetTrackDto, TrackModel>();



    }
}
