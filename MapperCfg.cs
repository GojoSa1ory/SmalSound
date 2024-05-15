using AutoMapper;
using KPCourseWork.Dto.PlaylistDto;
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
        
        
        CreateMap<PlaylistModel, GetPlaylistDto>().ReverseMap();
        CreateMap<PlaylistModel, SetPlaylistDto>().ReverseMap();
    }
}