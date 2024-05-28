namespace KPCourseWork.Dto.TrackDto;

public class SetTrackDto
{
    public string Name { get; set; }
    public IFormFile TrackImage { get; set; }
    public int GenreId { get; set; }
    public IFormFile Track { get; set; }
}