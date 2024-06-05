using System.ComponentModel.DataAnnotations.Schema;

namespace KPCourseWork.Models;

[Table("playlist")]
public class PlaylistModel
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("imagePath")]
    public string? Image { get; set; }
    public List<TrackModel>? Tracks { get; set; }
    [Column("user_id")]
    public UserModel? User { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
}
