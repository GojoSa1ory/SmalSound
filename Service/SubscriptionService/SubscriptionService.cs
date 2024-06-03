using AutoMapper;
using KPCourseWork.Data;
using Microsoft.EntityFrameworkCore;

namespace KPCourseWork.Service.SubscriptionService;

public class SubscriptionService : ISubscriptionService
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

            var checkSub = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.SubscriberId == subscriberId && s.SubscribedTo.Id == subscribedToId);
            

            if (checkSub is not null)
            {
                throw new Exception("U already is subscriber");
            }

            if (subscriber == null || subscribedTo == null)
            {
                throw new Exception("User not found");
            }

            var subscription = new SubscriptionModel
            {
                SubscriberId = subscriberId,
                SubscribedTo = subscribedTo
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
            var subscribedTo = await _context.Users.FindAsync(subscribedToId);
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.SubscriberId == subscriberId && s.SubscribedTo == subscribedTo);

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

    public async Task<ServiceResponse<List<GetSubscriptionDto>>> GetSubscriptions(int userId)
    {
        ServiceResponse<List<GetSubscriptionDto>> response = new();

        try
        {
            var sub = _context.Subscriptions
                .Include(s => s.SubscribedTo)
                .Where(s => s.SubscriberId == userId).ToList();

            if (sub is null) throw new Exception("Sub not found");

            response.Data = sub.Select(s => _mapper.Map<GetSubscriptionDto>(s)).ToList();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> CheckSubscription(int userId, int subscribedToId)
    {
        ServiceResponse<bool> response = new();

        try
        {
            var checkSub = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.SubscriberId == userId && s.SubscribedTo.Id == subscribedToId);

            if (checkSub is null)
            {
                response.Data = false;
            }
            else
            {
                response.Data = true;
            }

        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<int>> GetSubscribersCount(int userId)
    {
        ServiceResponse<int> response = new();
    
        try
        {
            var user = await _context.Users
                .Include(u => u.Subscriptions)
                .FirstOrDefaultAsync(u => u.Id == userId);

            int count = user.Subscriptions.Count;
    
            if (user == null)
            {
                throw new Exception("User not found");
            }
            
            response.Data = count;
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }
    
        return response;
    }

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