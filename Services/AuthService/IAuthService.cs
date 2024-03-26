namespace CourseWork.Services;

public interface IAuthService
{
    Task<ServiceResponse<GetUserDto>> Register(SetUserDto user);
    Task<ServiceResponse<GetUserDto>> Login(SetUserDto user);
}
