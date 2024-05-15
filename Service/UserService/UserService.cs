using AutoMapper;
using KPCourseWork.Data;

namespace KPCourseWork.Service;

public class UserService: IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileUploadService _fileUploadService;

    public UserService (AppDbContext context, IMapper mapper, IFileUploadService fileUploadService)
    {
        _context = context;
        _mapper = mapper;
        _fileUploadService = fileUploadService;
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

    public async Task<ServiceResponse<GetUserDto>> UpdateUserInfo(UpdateUserDto newUser, int userId)
    {
        ServiceResponse<GetUserDto> response = new();

        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user is null) throw new Exception("User not found");
            
            user.Email = newUser.Email is not null ? newUser.Email : user.Email;
            user.Name = newUser.Name is not null ? newUser.Name : user.Name;
            
            // if (!BCrypt.Net.BCrypt.Verify(newUser.Password, user.Password))
            //     user.Password = newUser.Password;

            user.ProfilePicture = await _fileUploadService.UploadFile("UserImage", newUser.ProfilePicture);
            
            await _context.SaveChangesAsync();
            
            response.Data = _mapper.Map<GetUserDto>(user);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Success = false;
        }
        
        return response;
    }
}