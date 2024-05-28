namespace KPCourseWork.Dto.TrackDto;

public class GetTrackDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TrackImage { get; set; }
    public string Track { get; set; }
    public GetGenreDto? Genre { get; set; }
    public GetUserDto User { get; set; } 
    public DateTime createdAt {get; set;}
    public DateTime updatedAt {get; set;}
}