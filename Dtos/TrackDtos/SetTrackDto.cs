namespace CourseWork.Dtos;

public class SetTrackDto
{
    public int ID { get; set; }
    public IFormFile Track { get; set; }
    public string TrackName { get; set; }
    public int AuditionCount { get; set; }
    public string ArtistName { get; set; }
    public IFormFile Image { get; set; }
    public int UserId { get; set; }
    public UserModel? User { get; set; }
}
