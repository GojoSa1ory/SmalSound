namespace KPCourseWork.Models;

public class SubscriptionModel
{
    public int Id { get; set; }
    public UserModel SubscribedTo { get; set; }
    public int SubscriberId { get; set; }
}