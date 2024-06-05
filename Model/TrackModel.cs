using System.ComponentModel.DataAnnotations.Schema;

namespace KPCourseWork.Models;

[Table("track")]
public class TrackModel
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("image_path")]
    public string TrackImage { get; set; }
    [Column("track_path")]
    public string Track { get; set; }
    [Column("user_id")]
    public UserModel User { get; set; }
    [Column("genre_id")]
    public GenreModel Genre {get; set;}
    public List<PlaylistModel>? Playlist { get; set; }
    public List<FavoriteModel>? Favorite { get; set; }
    [Column("created_at")]
    public DateTime createdAt {get; set;}
    [Column("updated_at")]
    public DateTime updatedAt {get; set;}
    [Column("listen_count")]
    public int ListenCount {get; set;}
}
