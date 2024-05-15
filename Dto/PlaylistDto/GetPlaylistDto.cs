using KPCourseWork.Dto.TrackDto;

namespace KPCourseWork.Dto.PlaylistDto;

public class GetPlaylistDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public List<GetTrackDto>? Tracks { get; set; }
    public GetUserDto User { get; set; }
}