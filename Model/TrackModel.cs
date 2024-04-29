namespace KPCourseWork.Models;

public class TrackModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TrackImage { get; set; }
    public string Track { get; set; }
    public UserModel User { get; set; } //Artist
}