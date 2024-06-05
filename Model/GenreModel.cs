using System.ComponentModel.DataAnnotations.Schema;

namespace KPCourseWork.Models;

[Table("genre")]
public class GenreModel
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    public List<TrackModel>? Tracks { get; set; }
}