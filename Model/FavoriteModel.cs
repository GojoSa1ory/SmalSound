namespace KPCourseWork.Models;

public class FavoriteModel
{
    public int Id { get; set; }
    public List<TrackModel>? Tracks { get; set; }
    public UserModel? User { get; set; }
}