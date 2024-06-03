namespace KPCourseWork.Models;

public class UserModel {
    public int Id {get; set;}
    public string Name {get; set;}
    public string Email { get; set; }
    public string Password {get;set;}
    public string? ProfilePicture {get; set;}
    public RoleModel Role { get; set; }
    public List<TrackModel> Tracks {get; set; }
    public List<PlaylistModel>? Playlist { get; set; }
    public List<FavoriteModel>? Favorite { get; set; }
    public DateTime createdAt {get; set;}
    public DateTime updatedAt {get; set;}
    public List<SubscriptionModel> Subscriptions { get; set; }
}
