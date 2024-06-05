using System.ComponentModel.DataAnnotations.Schema;

namespace KPCourseWork.Models;

[Table("user")]
public class UserModel {
    [Column("id")]
    public int Id {get; set;}
    [Column("name")]
    public string Name {get; set;}
    [Column("email")]
    public string Email { get; set; }
    [Column("password")]
    public string Password {get;set;}
    [Column("image_path")]
    public string? ProfilePicture {get; set;}
    [Column("role_id")]
    public RoleModel Role { get; set; }
    public List<TrackModel> Tracks {get; set; }
    public List<PlaylistModel>? Playlist { get; set; }
    public List<FavoriteModel>? Favorite { get; set; }
    [Column("created_at")]
    public DateTime createdAt {get; set;}
    [Column("updated_at")]
    public DateTime updatedAt {get; set;}
    public List<SubscriptionModel> Subscriptions { get; set; }
}
