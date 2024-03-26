namespace CourseWork.Dtos;

public class GetTrackDto
{
    public int ID { get; set; }
    public string Track { get; set; }
    public string TrackName { get; set; }
    public int AuditionCount { get; set; }
    public string ArtistName { get; set; }
    public string Image { get; set; }
    public int UserId { get; set; }
    public UserModel? User { get; set; }
}
