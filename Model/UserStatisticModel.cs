using System.ComponentModel.DataAnnotations.Schema;

namespace KPCourseWork.Models;

[Table("user_statistic")]
public class UserStatisticModel
{
    [Column("id")]
    public int Id { get; set; }
    [InverseProperty("Id")]
    [Column("user_id")]
    public int UserId { get; set; }
    public UserModel User { get; set; }
    [InverseProperty("Id")]
    [Column("artist_id")]
    public int ArtistId { get; set; }
    public UserModel Artist { get; set; }
    [Column("genre_id")]
    public int GenreId { get; set; }
    public GenreModel Genre { get; set; }
    [Column("track_id")]
    public int TrackId { get; set; }
    public TrackModel Track { get; set; }
}