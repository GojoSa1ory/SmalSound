namespace KPCourseWork.Models;

public class GenreModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<TrackModel>? Tracks { get; set; }
}