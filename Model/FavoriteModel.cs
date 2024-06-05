using System.ComponentModel.DataAnnotations.Schema;

namespace KPCourseWork.Models;

[Table("favorite")]
public class FavoriteModel
{
    [Column("id")]
    public int Id { get; set; }
    [Column("tracks")]
    public List<TrackModel>? Tracks { get; set; }
    [Column("user_id")]
    public UserModel? User { get; set; }
}