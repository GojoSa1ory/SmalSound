using AutoMapper;
using KPCourseWork.Data;

namespace KPCourseWork.Service;

public class UserService: IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserService (AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<GetUserDto>> GetProfile(int userId)
    {
        ServiceResponse<GetUserDto> response = new();

        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user is null) throw new Exception("User not found");

            response.Data = _mapper.Map<GetUserDto>(user);
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }

        return response;
    }
}