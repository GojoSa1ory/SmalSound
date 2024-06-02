using AutoMapper;
using KPCourseWork.Data;
using Microsoft.EntityFrameworkCore;

namespace KPCourseWork.Service.SubscriptionService;

public class SubscriptionService: ISubscriptionService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public SubscriptionService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<ServiceResponse<string>> Subscribe(int subscriberId, int subscribedToId)
    {
        ServiceResponse<string> response = new();

        try
        {
            var subscriber = await _context.Users.FindAsync(subscriberId);
            var subscribedTo = await _context.Users.FindAsync(subscribedToId);

            if (subscriber == null || subscribedTo == null)
            {
                throw new Exception("User not found");
            }

            var subscription = new SubscriptionModel
            {
                SubscriberId = subscriberId,
                SubscribedToId = subscribedToId
            };

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            response.Data = "Successfully subscribed";
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<string>> Unsubscribe(int subscriberId, int subscribedToId)
    {
        ServiceResponse<string> response = new();

        try
        {
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.SubscriberId == subscriberId && s.SubscribedToId == subscribedToId);

            if (subscription == null)
            {
                throw new Exception("Subscription not found");
            }

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();

            response.Data = "Successfully unsubscribed";
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }

        return response;
    }

    public Task<ServiceResponse<List<GetSubscriptionDto>>> GetSubscriptions(int userId)
    {
        throw new NotImplementedException();
    }

    // public async Task<ServiceResponse<List<GetUserDto>>> GetSubscribers(int userId)
    // {
    //     ServiceResponse<List<GetUserDto>> response = new();
    //
    //     try
    //     {
    //         var user = await _context.Users
    //             .Include(u => u.Subscribers)
    //             .ThenInclude(s => s.Subscriber)
    //             .FirstOrDefaultAsync(u => u.Id == userId);
    //
    //         if (user == null)
    //         {
    //             throw new Exception("User not found");
    //         }
    //
    //         var subscribers = user.Subscribers.Select(s => s.Subscriber).ToList();
    //         response.Data = _mapper.Map<List<GetUserDto>>(subscribers);
    //     }
    //     catch (Exception e)
    //     {
    //         response.Success = false;
    //         response.Message = e.Message;
    //     }
    //
    //     return response;
    // }

    // public async Task<ServiceResponse<List<GetUserDto>>> GetSubscriptions(int userId)
    // {
    //     ServiceResponse<List<GetUserDto>> response = new();
    //
    //     try
    //     {
    //         var user = await _context.Users
    //             .Include(u => u.Subscriptions)
    //             .ThenInclude(s => s.SubscribedTo)
    //             .FirstOrDefaultAsync(u => u.Id == userId);
    //
    //         if (user == null)
    //         {
    //             throw new Exception("User not found");
    //         }
    //
    //         var subscriptions = user.Subscriptions.Select(s => s.SubscribedTo).ToList();
    //         response.Data = _mapper.Map<List<GetUserDto>>(subscriptions);
    //     }
    //     catch (Exception e)
    //     {
    //         response.Success = false;
    //         response.Message = e.Message;
    //     }
    //
    //     return response;
    // }
}