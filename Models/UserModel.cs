namespace CourseWork.Models;
using Microsoft.EntityFrameworkCore;

public class UserModel
{
    public int ID { set; get; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string ProfileImage { get; set; }
    public List<TrackModel>? TracksList { get; set; }
}
