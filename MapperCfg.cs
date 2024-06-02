using AutoMapper;
using KPCourseWork.Dto;
using KPCourseWork.Dto.FavoriteDto;
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
        
        CreateMap<GenreModel, GetGenreDto>().ReverseMap();
        CreateMap<FavoriteModel, GetFavoriteDto>().ReverseMap();

        CreateMap<SubscriptionModel, GetSubscriptionDto>().ReverseMap();
        
        // CreateMap<SubscriptionModel, GetSubscriptionDto>()
        //     .ForMember(dest => dest.Subscriber, opt => opt.MapFrom(src => src.Subscriber))
        //     .ForMember(dest => dest.SubscribedTo, opt => opt.MapFrom(src => src.SubscribedTo));
    }
}