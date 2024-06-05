using System.ComponentModel.DataAnnotations.Schema;

namespace KPCourseWork.Models;

[Table("subscrription")]
public class SubscriptionModel
{
    [Column("id")]
    public int Id { get; set; }
    [Column("subscribed_to")]
    public UserModel SubscribedTo { get; set; }
    [Column("subscriber_id")]
    public int SubscriberId { get; set; }
}