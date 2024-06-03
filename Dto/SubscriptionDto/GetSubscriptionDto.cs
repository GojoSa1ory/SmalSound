namespace KPCourseWork.Dtos;

public class GetSubscriptionDto
{
    public int Id { get; set; }
    public GetUserDto SubscribedTo { get; set; }
    public int SubscriberId { get; set; }
}