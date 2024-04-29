namespace KPCourseWork.Dto.TrackDto;

public class GetTrackDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TrackImage { get; set; }
    public string Track { get; set; }
    public GetUserDto User { get; set; } //Artist
}