using AutoMapper;
using KPCourseWork.Dto.TrackDto;

namespace KPCourseWork;

public class MapperCfg : Profile
{
    public MapperCfg()
    {
        CreateMap<UserModel, SetUserDto>().ReverseMap();
        CreateMap<UserModel, GetUserDto>().ReverseMap();
        
        CreateMap<TrackModel, GetTrackDto>().ReverseMap();
        CreateMap<TrackModel, SetTrackDto>().ReverseMap();
    }
}