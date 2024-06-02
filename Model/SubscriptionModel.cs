namespace KPCourseWork.Models;

public class SubscriptionModel
{
    public int Id { get; set; }
    public int SubscriberId { get; set; }
    public UserModel Subscriber { get; set; }

    public int SubscribedToId { get; set; }
    public UserModel SubscribedTo { get; set; }
}