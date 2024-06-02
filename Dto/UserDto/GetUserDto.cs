namespace KPCourseWork.Dtos;

public class GetUserDto {
    public int Id {get; set;}
    public string Name {get; set;}
    public string Email { get; set; }
    public string ProfilePicture {get; set;}
    public List<GetSubscriptionDto> Subscriptions { get; set; }
    public List<GetSubscriptionDto> Subscribers { get; set; }
    // public string ProfilePoster {get; set;}
}
