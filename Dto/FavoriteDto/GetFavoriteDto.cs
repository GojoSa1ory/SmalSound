using KPCourseWork.Dto.TrackDto;

namespace KPCourseWork.Dto.FavoriteDto;

public class GetFavoriteDto
{
    public int Id { get; set; }
    public List<GetTrackDto> Tracks { get; set; }
    public GetUserDto User { get; set; }
}