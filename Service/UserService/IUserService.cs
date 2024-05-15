namespace KPCourseWork.Service;

public interface IUserService
{
    Task<ServiceResponse<GetUserDto>> GetProfile(int userId);
    Task<ServiceResponse<GetUserDto>> UpdateUserInfo(UpdateUserDto newUser,int userId);
}