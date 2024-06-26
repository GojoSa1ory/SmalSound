using KPCourseWork.Dto.PlaylistDto;
using KPCourseWork.Dto.TrackDto;

namespace KPCourseWork.Dto;

public class SearchResponseDto
{
    public List<GetUserDto>? User { get; set; }
    public List<GetTrackDto>? Tracks { get; set; }    
    public List<GetPlaylistDto>? Playlists { get; set; }    
}

