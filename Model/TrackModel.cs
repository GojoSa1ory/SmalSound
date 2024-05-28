namespace KPCourseWork.Models;

public class TrackModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TrackImage { get; set; }
    public string Track { get; set; }
    public UserModel User { get; set; }
    public GenreModel Genre {get; set;}
    public List<PlaylistModel>? Playlist { get; set; }
    public DateTime createdAt {get; set;}
    public DateTime updatedAt {get; set;}
}