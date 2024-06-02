namespace KPCourseWork.Service.SubscriptionService;

public interface ISubscriptionService
{
    Task<ServiceResponse<string>> Subscribe(int subscriberId, int subscribedToId);
    Task<ServiceResponse<string>> Unsubscribe(int subscriberId, int subscribedToId);
    Task<ServiceResponse<List<GetSubscriptionDto>>> GetSubscriptions(int userId);
}