namespace KPCourseWork.Service;

public interface IUserService
{
    Task<ServiceResponse<GetUserDto>> GetProfile(int userId);
}