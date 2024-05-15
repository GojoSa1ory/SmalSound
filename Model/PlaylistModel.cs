namespace KPCourseWork.Models;

public class PlaylistModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public List<TrackModel>? Tracks { get; set; }
    public UserModel User { get; set; }
}